 
namespace Integration.ServiceTests.UserServiceTests
{
    public class UserServiceTests : TestBase
    {

        public UserServiceTests(ApplicationFixture applicationFixture) : base(applicationFixture)
        {
        }

        [Fact]
        public async Task RegisterUser_Should_Succeed_When_ValidData_Provided()
        {
            // Arrange
            var user = await this.RegisterAndGetRandomUserAsync();

            // Act & Assert
            user.Should().NotBeNull();
            user.Id.Should().NotBeEmpty();
            user.Email.Should().Contain("@example.com");
        }
       
      

    }
}
