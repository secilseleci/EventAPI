using Core.DTOs.Invitation;
using Core.Entities;
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
        Task<IResult> SendInvitationAsync(Guid organizerId, Guid eventId, List<Guid> userIds, CancellationToken cancellationToken);
        Task<IDataResult<IEnumerable<InvitationDto>>> GetReceivedInvitationsAsync(Guid userId, CancellationToken cancellationToken);
        Task<IDataResult<IEnumerable<InvitationDto>>> GetSentInvitationsAsync(Guid userId, CancellationToken cancellationToken);
        Task<IDataResult<InvitationDto?>> GetSingleInvitationAsync(Guid receiverId, Guid eventId, CancellationToken cancellationToken);
        Task<IResult> ParticipateInvitationAsync(Guid receiverId, Guid eventId, CancellationToken cancellationToken);

    }
}
