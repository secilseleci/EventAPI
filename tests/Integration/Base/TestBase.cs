using Core.Interfaces.Services;
using Integration.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Integration.Base
{
    [Collection("Database collection")]//ortak kaynak-çakışmaların önlenmesi
    public class TestBase
    {
        public ApplicationFixture ApplicationFixture { get; }

        public IEventService EventService
            =>ApplicationFixture.Services.GetRequiredService<IEventService>();

        public IInvitationService InvitationService
           => ApplicationFixture.Services.GetRequiredService<IInvitationService>();

        public IUserService UserService
           => ApplicationFixture.Services.GetRequiredService<IUserService>();

        protected TestBase(ApplicationFixture  applicationFixture)
        {
            ApplicationFixture =  applicationFixture;
        }
    }
}
