using AutoMapper;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Integration.ServiceTests.EventServiceTestsUpdate
{
    public class EventServiceTestsUpdate : TestBase
    {
        private readonly EventService _eventService;
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        public EventServiceTestsUpdate(ApplicationFixture applicationFixture) : base(applicationFixture)
        {
            _userRepository = ApplicationFixture.Services.GetRequiredService<IUserRepository>();

            _eventRepository = ApplicationFixture.Services.GetRequiredService<IEventRepository>();
            var userService = ApplicationFixture.Services.GetRequiredService<IUserService>();
            var mapper = ApplicationFixture.Services.GetRequiredService<IMapper>();
            _eventService = new EventService(_eventRepository, mapper, userService);
        }

        #region UpdateEvent Tests
        [Fact]
        public async Task UpdateEventAsync_Should_Return_EventNotFound_When_Event_Does_Not_Exist()
        {
            // Arrange
            var organizer = new User
            {
                Id = Guid.NewGuid(),
                FullName = "Test Organizer",
                Email = "organizer@example.com",
                UserName = "Organizer123",
                Password = "password123"
            };
            await _userRepository.CreateAsync(organizer);

            var updateEventDto = new UpdateEventDto
            {
                Id = Guid.NewGuid(), // Sistemde olmayan ID
                EventName = "Updated Event Name",
                EventDescription = "Updated Description",
                StartDate = DateTimeOffset.UtcNow,
                EndDate = DateTimeOffset.UtcNow.AddHours(2),
                Location = "Updated Location",
                Timezone = "UTC"
            };

            // Act
            var result = await _eventService.UpdateEventAsync(updateEventDto, organizer.Id, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.EventNotFound, result.Message);
        }

        [Fact]
        public async Task UpdateEventAsync_Should_Return_UnauthorizedAccess_When_User_Is_Not_Organizer()
        {
            // Arrange
            var invalidUser = await this.RegisterAndGetRandomUserAsync();
            var randomEvent = await this.RegisterAndGetRandomEventAsync(); // Rastgele bir event oluşturuluyor

            var eventInDb = await _eventRepository.GetByIdAsync(randomEvent.Id);
            Assert.NotNull(eventInDb);

            
            var updateEventDto = new UpdateEventDto
            {
                Id = randomEvent.Id,
                EventName = "Updated Event Name",
                EventDescription = "Updated Description",
                StartDate = DateTimeOffset.UtcNow,
                EndDate = DateTimeOffset.UtcNow.AddHours(2),
                Location = "Updated Location",
                Timezone = "UTC"
            };

            // Act
            var result = await _eventService.UpdateEventAsync(updateEventDto, invalidUser.Id, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.UnauthorizedAccess, result.Message);
        }

        [Fact]
        public async Task UpdateEventAsync_Should_Return_SuccessResult_When_All_Conditions_Are_Met()
        {

            // Arrange
 
            // Act
 
            // Assert
           
        }

            
     


        #endregion
    }
}
