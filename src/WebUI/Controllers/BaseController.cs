using Core.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Core.Utilities.Results;
using IResult = Core.Utilities.Results.IResult;

namespace WebUI.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult HandleResponse(IResult result)
        {
            if (result.Success)
            {
                return Ok(result);
            }

            if (
                result.Message == Messages.EmptyEventList ||
                result.Message == Messages.EmptyInvitationList ||
                result.Message == Messages.EmptyParticipantList ||
                result.Message == Messages.EventNotFound ||
                result.Message == Messages.InvitationNotFound ||
                result.Message == Messages.UserNotFound ||
                result.Message == Messages.ParticipantNotFound)
            {
                return NotFound(result);
            }

            return BadRequest(result);
        }

    }
}
