using AutoMapper;
using Core.Entities;
using Core.Interfaces.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Utilities.Constants;
using Core.Utilities.Results;
using Infrastructure.Repositories.Implementations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class InvitationService(
        IInvitationRepository _invitationRepository,
        IMapper _mapper,
        IUserService _userService,
        IEventRepository _eventRepository) : IInvitationService
    {
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

            // Kullanıcıların geçerliliğini kontrol et
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
    }
}