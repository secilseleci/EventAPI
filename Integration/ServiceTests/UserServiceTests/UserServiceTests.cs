 
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
        [Fact]
        public async Task RegisterUser_Should_Use_Custom_User_Data()
        {
            // Arrange
            var customUser = new User
            {
                FullName = "Custom User",
                UserName = "CustomUser123",
                Password = "CustomPassword!",
                Email = "custom@example.com"
            };
            var user = await this.RegisterAndGetRandomUserAsync(customUser);

            // Act & Assert
            user.Should().NotBeNull();
            user.FullName.Should().Be("Custom User");
            user.Email.Should().Be("custom@example.com");
        }

    }
}
