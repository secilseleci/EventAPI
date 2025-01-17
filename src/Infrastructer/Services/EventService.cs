using AutoMapper;
using Core.DTOs;
using Core.DTOs.Event;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Utilities.Constants;
using Core.Utilities.Results;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Infrastructure.Services
{
    public class EventService(
        IEventRepository _eventRepository,
        IMapper _mapper,
        IUserService _userService) : IEventService
    {
        #region Create
        public async Task<IResult> CreateEventAsync(CreateEventDto createEventDto, Guid userId, CancellationToken cancellationToken)
        {
             var isValidUser=await _userService.IsUserValidAsync(userId,cancellationToken);

            if (isValidUser == false)  
                return new ErrorResult(Messages.UserNotFound);

            var eventEntity=_mapper.Map<Event>(createEventDto);
            if (eventEntity == null)  
                return new ErrorResult(Messages.CreateEventError); 
            
            eventEntity.OrganizerId = userId;
            
            var createResult = await _eventRepository.CreateAsync(eventEntity);
            return createResult>0
                ? new SuccessResult(Messages.CreateEventSuccess)
                : new ErrorResult(Messages.CreateEventError);
        }
        #endregion

        #region Delete
        public async Task<IResult> DeleteEventAsync(Guid eventId,Guid userId, CancellationToken cancellationToken)
        {
            var eventEntity=await _eventRepository.GetByIdAsync(eventId);
            
            if(eventEntity == null) 
                return new ErrorResult(Messages.EventNotFound);

            if (eventEntity.OrganizerId != userId)
                return new ErrorResult(Messages.UnauthorizedAccess);

            var deleteResult=await _eventRepository.DeleteAsync(eventId);
            return deleteResult > 0
                ? new SuccessResult(Messages.DeleteEventSuccess)
                : new ErrorResult(Messages.DeleteEventError);
        }
        #endregion

        #region Update
        public async Task<IResult> UpdateEventAsync(UpdateEventDto updateEventDto,Guid userId, CancellationToken cancellationToken)
        {
            var eventEntity=await _eventRepository.GetByIdAsync(updateEventDto.Id);
            if(eventEntity == null)
                return new ErrorResult(Messages.EventNotFound);

            if (eventEntity.OrganizerId != userId)
                return new ErrorResult(Messages.UnauthorizedAccess);

            CompleteUpdate(eventEntity, updateEventDto);
            
            var updateResult = await _eventRepository.UpdateAsync(eventEntity);
            return updateResult > 0
                ? new SuccessResult(Messages.UpdateEventSuccess)
                : new ErrorResult(Messages.UpdateEventError);
        }
        #endregion


        #region Read
        public async Task<IDataResult<IEnumerable<ViewEventDto>>> GetAllEventsAsync(Expression<Func<Event, bool>> predicate, CancellationToken cancellationToken)
        {
            var eventList = await _eventRepository.GetAllAsync(predicate);
            return eventList is not null && eventList.Any()
               ? new SuccessDataResult<IEnumerable<ViewEventDto>>(_mapper.Map<IEnumerable<ViewEventDto>>(eventList))
               : new ErrorDataResult<IEnumerable<ViewEventDto>>(Messages.EmptyEventList);
        }

        public async Task<IDataResult<ViewEventDto>> GetEventByIdAsync(Guid eventId, CancellationToken cancellationToken)
        {
            var eventEntity= await _eventRepository.GetByIdAsync(eventId);
            return eventEntity is not null
                ? new SuccessDataResult<ViewEventDto> (_mapper.Map<ViewEventDto>(eventEntity))
                : new ErrorDataResult<ViewEventDto>(Messages.EventNotFound);
        }
        public async Task<IDataResult<PaginationDto<ViewEventDto>>> GetAllEventsWithPaginationAsync(
        int page, int pageSize, CancellationToken cancellationToken)
        {
            var eventsWithPagination = await _eventRepository.GetAllEventsWithPaginationAsync(page, pageSize);
            if (eventsWithPagination == null || eventsWithPagination.Data == null || !eventsWithPagination.Data.Any())
            {
                return new ErrorDataResult<PaginationDto<ViewEventDto>>(Messages.EmptyEventList);
            }
            
            return new SuccessDataResult<PaginationDto<ViewEventDto>>(
        new PaginationDto<ViewEventDto>
        {
            Data = _mapper.Map<IEnumerable<ViewEventDto>>(eventsWithPagination.Data),
            CurrentPage = eventsWithPagination.CurrentPage,
            TotalPages = eventsWithPagination.TotalPages,
            PageSize = eventsWithPagination.PageSize,
            TotalCount = eventsWithPagination.TotalCount
        });

        }

        public async Task<IDataResult<ViewEventWithParticipantsDto>> GetEventWithParticipantsAsync(Guid eventId, CancellationToken cancellationToken)
        {
            var eventEntity = await _eventRepository.GetEventWithParticipantsAsync(eventId);
            if (eventEntity is null)
                return new ErrorDataResult<ViewEventWithParticipantsDto>(Messages.EventNotFound);

            return !eventEntity.Participants.Any()
                ? new ErrorDataResult<ViewEventWithParticipantsDto>(Messages.EmptyParticipantList)
                : new SuccessDataResult<ViewEventWithParticipantsDto>(_mapper.Map<ViewEventWithParticipantsDto>(eventEntity));
        }
        public async Task<IDataResult<IEnumerable<ViewEventDto>>> GetEventListByDateRangeAsync(DateRangeDto dateRangeDto, CancellationToken cancellationToken)
        {
            var validationContext = new ValidationContext(dateRangeDto);
            Validator.ValidateObject(dateRangeDto, validationContext, validateAllProperties: true);

            var eventList = await _eventRepository.GetAllAsync(e => e.StartDate <= dateRangeDto.EndDate && e.EndDate >= dateRangeDto.StartDate);

            return eventList is not null && eventList.Any()
                ? new SuccessDataResult<IEnumerable<ViewEventDto>>(_mapper.Map<IEnumerable<ViewEventDto>>(eventList), Messages.EventsRetrievedSuccessfully)
                : new ErrorDataResult<IEnumerable<ViewEventDto>>(Messages.EmptyEventList);
        }

        public async Task<IDataResult<IEnumerable<ViewEventDto>>> GetOrganizedEventListForUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var userExists=await _userService.IsUserValidAsync(userId, cancellationToken);
            if(userExists==false)
                return new ErrorDataResult<IEnumerable<ViewEventDto>>(Messages.UserNotFound);
           
            
            var eventList = await _eventRepository.GetAllAsync(e => e.OrganizerId == userId);
            return eventList is not null && eventList.Any()
               ? new SuccessDataResult<IEnumerable<ViewEventDto>>(_mapper.Map<IEnumerable<ViewEventDto>>(eventList), Messages.EventsRetrievedSuccessfully)
               : new ErrorDataResult<IEnumerable<ViewEventDto>>(Messages.EmptyEventList);
        }
        public async Task<IDataResult<IEnumerable<ViewEventDto>>> GetParticipatedEventListForUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var userExists = await _userService.IsUserValidAsync(userId, cancellationToken);
            if (userExists == false)
                return new ErrorDataResult<IEnumerable<ViewEventDto>>(Messages.UserNotFound);

            var eventList = await _eventRepository.GetAllAsync(e => e.Participants.Any(p => p.UserId == userId));
            return eventList is not null && eventList.Any()
               ? new SuccessDataResult<IEnumerable<ViewEventDto>>(_mapper.Map<IEnumerable<ViewEventDto>>(eventList), Messages.EventsRetrievedSuccessfully)
               : new ErrorDataResult<IEnumerable<ViewEventDto>>(Messages.EmptyEventList);

        }
        public async Task<IDataResult<int>> GetParticipantCountForEventAsync(Guid eventId, CancellationToken cancellationToken)
        {
            var eventExists = await _eventRepository.GetByIdAsync(eventId);
            if (eventExists==null)
                return new ErrorDataResult<int>(Messages.EventNotFound);

            var participantCount = await _eventRepository.GetParticipantCountAsync(eventId);

            return new SuccessDataResult<int>(
        participantCount,
        participantCount > 0
            ? Messages.ParticipantCountRetrievedSuccessfully  
            : Messages.ParticipantNotFound  
    );
        }

        #endregion


        #region Helper Methods 
         private static void CompleteUpdate(Event eventEntity, UpdateEventDto updateEventDto)
        {
            eventEntity.EventName = updateEventDto.EventName;
            eventEntity.EventDescription = updateEventDto.EventDescription;
            eventEntity.StartDate = updateEventDto.StartDate;
            eventEntity.EndDate = updateEventDto.EndDate;
            eventEntity.Location = updateEventDto.Location;
            eventEntity.Timezone = updateEventDto.Timezone;
        }
       

        #endregion




    }
}
