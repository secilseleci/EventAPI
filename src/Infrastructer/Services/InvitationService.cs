using AutoMapper;
using Core.DTOs.Invitation;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Utilities.Constants;
using Core.Utilities.Results;

namespace Infrastructure.Services
{
    public class InvitationService(
        IInvitationRepository _invitationRepository,
        IMapper _mapper,
        IUserService _userService,
        IEventRepository _eventRepository) : IInvitationService
    {

        public async Task<IDataResult<IEnumerable<InvitationDto>>> GetReceivedInvitationsAsync(Guid userId, CancellationToken cancellationToken)
        {
            var isValidUser=await _userService.IsUserValidAsync(userId, cancellationToken);
            if (isValidUser == false)
                return new ErrorDataResult<IEnumerable<InvitationDto>>(Messages.UserNotFound);
            var invitationList=await _invitationRepository.GetInvitationsWithDetailsAsync(i=>i.ReceiverId == userId);

            return invitationList is not null && invitationList.Any()
              ? new SuccessDataResult<IEnumerable<InvitationDto>>(_mapper.Map<IEnumerable<InvitationDto>>(invitationList))
              : new ErrorDataResult<IEnumerable<InvitationDto>>(Messages.EmptyInvitationList);
        }

        public async Task<IDataResult<IEnumerable<InvitationDto>>> GetSentInvitationsAsync(Guid userId, CancellationToken cancellationToken)
        {
            var isValidUser = await _userService.IsUserValidAsync(userId, cancellationToken);
            if (isValidUser == false)
                return new ErrorDataResult<IEnumerable<InvitationDto>>(Messages.UserNotFound);
            var invitationList = await _invitationRepository.GetInvitationsWithDetailsAsync(i => i.OrganizerId == userId); ;

            return invitationList is not null && invitationList.Any()
              ? new SuccessDataResult<IEnumerable<InvitationDto>>(_mapper.Map<IEnumerable<InvitationDto>>(invitationList))
              : new ErrorDataResult<IEnumerable<InvitationDto>>(Messages.EmptyInvitationList);
        }

        public async Task<IDataResult<InvitationDto?>> GetSingleInvitationAsync(Guid receiverId, Guid eventId, CancellationToken cancellationToken)
        {
            var isValidUser = await _userService.IsUserValidAsync(receiverId, cancellationToken);
            if (isValidUser == false)
                return new ErrorDataResult<InvitationDto?>(Messages.UserNotFound);

            var invitationEntity = await _invitationRepository.GetInvitationByEventAndReceiverAsync(eventId, receiverId);
            return invitationEntity is null
               ? new ErrorDataResult<InvitationDto?>(Messages.InvitationNotFound)
                : new SuccessDataResult<InvitationDto?>(_mapper.Map<InvitationDto>(invitationEntity));


        }

             
        public async Task<IResult> SendInvitationAsync(Guid organizerId, Guid eventId, List<Guid> userIds, CancellationToken cancellationToken)
        {
            // Kullanıcı doğrulaması
            var isValidUser = await _userService.IsUserValidAsync(organizerId, cancellationToken);
            if (isValidUser == false)
                return new ErrorResult(Messages.UserNotFound);

            // Event doğrulaması
            var eventEntity = await _eventRepository.GetByIdAsync(eventId);
            if (eventEntity == null)
                return new ErrorResult(Messages.EventNotFound);

            // Yetki kontrolü
            if (eventEntity.OrganizerId != organizerId)
                return new ErrorResult(Messages.UnauthorizedAccess);

            // Kullanıcıların geçerliliğini kontrol  
            var validUserIds = new List<Guid>();
            foreach (var userId in userIds)
            {
                var isUserValid = await _userService.IsUserValidAsync(userId, cancellationToken);
                if (isUserValid)
                {
                    validUserIds.Add(userId);
                }
            }

            if (!validUserIds.Any())
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            // Daha önce davetiye gönderilmiş kullanıcıları filtrele
            var alreadyInvitedUsers = await _invitationRepository
                .GetAllAsync(i => i.EventId == eventId && validUserIds.Contains(i.ReceiverId));

            var newUserIds = validUserIds
                .Except(alreadyInvitedUsers.Select(i => i.ReceiverId))
                .ToList();

            if (!newUserIds.Any())
            {
                return new ErrorResult(Messages.AllUsersAlreadyInvited);
            }

            // Yeni davetiyeler oluştur ve kaydet
            foreach (var userId in newUserIds)
            {
                var invitation = new Invitation
                {
                    OrganizerId = organizerId,
                    ReceiverId = userId,
                    EventId = eventId,
                    IsAccepted = false,
                    Message = "You have been invited to the event."
                };

                await _invitationRepository.CreateAsync(invitation);
            }

            return new SuccessResult(Messages.InvitationsSentSuccessfully);
        }
    

        public async Task<IResult> ParticipateInvitationAsync(Guid receiverId, Guid eventId, CancellationToken cancellationToken)
        {
            var isValidUser = await _userService.IsUserValidAsync(receiverId, cancellationToken);

            if (isValidUser == false)
                return new ErrorResult(Messages.UserNotFound);

            var invitation = await _invitationRepository.GetInvitationByEventAndReceiverAsync(eventId, receiverId);
            if (invitation == null)
                return new ErrorResult(Messages.InvitationNotFound);

            // Sadece gerekli alanları güncelle
            invitation.IsAccepted = true;

            // Güncellenmiş entity'yi veritabanına kaydet
            var updateResult = await _invitationRepository.UpdateAsync(invitation);

            return updateResult > 0
                ? new SuccessResult(Messages.InvitationAcceptedSuccessfully)
                : new ErrorResult(Messages.UpdateInvitationError);
        }
    }
}