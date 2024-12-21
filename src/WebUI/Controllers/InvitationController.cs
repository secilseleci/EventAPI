using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationController(IInvitationService _invitationService) : BaseController
    {
        [HttpGet("received-invitations")]
        public async Task<IActionResult> GetReceivedInvitationsAsync([FromQuery] Guid userId)
        {
            var invitationResult = await _invitationService.GetReceivedInvitationsAsync(userId, default);

            return HandleResponse(invitationResult);
        }

        [HttpGet("sent-invitations")]
        public async Task<IActionResult> GetSentInvitations([FromQuery] Guid userId)
        {
            var invitationResult = await _invitationService.GetSentInvitationsAsync(userId, default);

            return HandleResponse(invitationResult);
        }

        [HttpGet("invitation")]
        public async Task<IActionResult> GetSingleInvitation([FromQuery] Guid receiverId, [FromQuery] Guid eventId)
        {

            var invitationResult = await _invitationService.GetSingleInvitationAsync(receiverId, eventId, default);

            return HandleResponse(invitationResult);
        }

        [HttpPost("participate")]
        public async Task<IActionResult> ParticipateInvitation([FromQuery] Guid receiverId, [FromQuery] Guid eventId)
        {
            var invitationResult = await _invitationService.ParticipateInvitationAsync(receiverId, eventId, default);
            return HandleResponse(invitationResult);
        }

        [HttpPost("sent-invitation")]
        public async Task<IActionResult> SendInvitation([FromQuery] Guid organizerId, [FromQuery] Guid eventId, [FromQuery] List<Guid> userIds)
        {
        var invitationResult = await _invitationService.SendInvitationAsync(organizerId, eventId, userIds, default);
            return HandleResponse(invitationResult);
        }
}
}
