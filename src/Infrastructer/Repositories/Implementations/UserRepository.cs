using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Implementations
{
    public class UserRepository(EventApiDbContext context) : BaseRepository<User>(context), IUserRepository
    {
    }
}
