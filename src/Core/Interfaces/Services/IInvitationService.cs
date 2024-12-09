using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IInvitationService
    {
       Task<IResult> SendInvitationAsync(Guid organizerId, Guid eventId, List<Guid> userIds,CancellationToken cancellationToken)

    }
}
