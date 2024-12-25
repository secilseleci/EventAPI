using Core.DTOs.Event;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]

    [ApiController]

    public class EventsController(IEventService _eventService) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventDto createEventDto, Guid userId)
        {
            var createEventResult = await _eventService.CreateEventAsync(createEventDto, userId, default);
            return HandleResponse(createEventResult);
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEvent(Guid eventId, Guid userId)
        {
            var deleteEventResult = await _eventService.DeleteEventAsync(eventId, userId, default);
            return HandleResponse(deleteEventResult);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvent(UpdateEventDto updateEventDto, Guid userId)
        {
            var updateEventResult = await _eventService.UpdateEventAsync(updateEventDto, userId, default);
            return HandleResponse(updateEventResult);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            var eventResult = await _eventService.GetEventByIdAsync(id, default);
            return HandleResponse(eventResult);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllEvents()
        {
            var eventsResult = await _eventService.GetAllEventsAsync(e => true, default);
            return HandleResponse(eventsResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventsWithPagination([FromQuery] int page, [FromQuery] int pageSize)
        {
            var eventResult = await _eventService.GetAllEventsWithPaginationAsync(page, pageSize, default);
            return HandleResponse(eventResult);
        }

        [HttpGet("with-participants/{id}")]
        public async Task<IActionResult> GetEventWithParticipants([FromRoute] Guid id)
        {
            var eventResult = await _eventService.GetEventWithParticipantsAsync(id, default);
            return HandleResponse(eventResult);
        }

        [HttpGet("organized-events/{userId}")]
        public async Task<IActionResult> GetOrganizedEventListForUser([FromRoute] Guid userId)
        {
            var eventResult=await _eventService.GetOrganizedEventListForUserAsync(userId, default);
            return HandleResponse(eventResult);
        }
        
        [HttpGet("participated-events/{userId}")]
        public async Task<IActionResult> GetParticipatedEventListForUser([FromRoute] Guid userId)
        {
            var eventResult = await _eventService.GetParticipatedEventListForUserAsync(userId, default);
            return HandleResponse(eventResult);
        }

        [HttpGet("date-events")]
        public async Task<IActionResult> GetEventListByDateRange( [FromQuery] DateTimeOffset startDate, [FromQuery] DateTimeOffset endDate)
        {
            var eventResult = await _eventService.GetEventListByDateRangeAsync( startDate,endDate, default);
            return HandleResponse(eventResult);
        }

        [HttpGet("participants/count/{eventId}")]
        public async Task<IActionResult> GetParticipantCountForEvent([FromRoute] Guid eventId)
        {
            var countResult=await _eventService.GetParticipantCountForEventAsync(eventId, default);
            return HandleResponse(countResult);
        }
            
            }
}
