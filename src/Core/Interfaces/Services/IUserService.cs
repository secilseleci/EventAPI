using Core.Entities;
using Core.Utilities.Results;

namespace Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> IsUserValidAsync(Guid userId,CancellationToken cancellation);
    }
}
