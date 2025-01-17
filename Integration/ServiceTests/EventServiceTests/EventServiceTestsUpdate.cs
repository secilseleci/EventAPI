using AutoMapper;
using Infrastructure.Services;

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

            var randomUserId = Guid.NewGuid(); 

            // Act
            var result = await _eventService.UpdateEventAsync(updateEventDto, randomUserId, CancellationToken.None);
            // Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.EventNotFound, result.Message);
        }

        [Fact]
        public async Task UpdateEventAsync_Should_Return_UnauthorizedAccess_When_User_Is_Not_Organizer()
        {
            // Arrange
            var unauthorizedUser = await this.RegisterAndGetRandomUserAsync();
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
            var result = await _eventService.UpdateEventAsync(updateEventDto, unauthorizedUser.Id, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.UnauthorizedAccess, result.Message);
        }

        [Fact]
        public async Task UpdateEventAsync_Should_Return_Success_When_Update_Is_Successful()
        {

            // Arrange
            var randomEvent= await this.RegisterAndGetRandomEventAsync();
            var updateEventDto=new UpdateEventDto {
                Id = randomEvent.Id,
                EventName = "Updated Event Name",
                EventDescription = "Updated Description",
                StartDate = randomEvent.StartDate.AddDays(1),
                EndDate = randomEvent.EndDate.AddDays(1),
                Location = "Updated Location",
                Timezone = randomEvent.Timezone
            };

            // Act
            var result = await _eventService.UpdateEventAsync(updateEventDto, randomEvent.OrganizerId, CancellationToken.None);
            // Assert
            Assert.IsType<SuccessResult>(result);
            Assert.Equal(Messages.UpdateEventSuccess, result.Message);
        }
        [Fact]
        public async Task UpdateEventAsync_Should_Return_Error_When_Update_Fails()
        {
            // Arrange
            var randomEvent = await this.RegisterAndGetRandomEventAsync();
             var updateEventDto = new UpdateEventDto
            {
                Id = randomEvent.Id,
                EventName =  randomEvent.EventName,
                 EventDescription = randomEvent.EventDescription,
                StartDate = randomEvent.StartDate,
                EndDate = randomEvent.EndDate,
                Location = randomEvent.Location,
                Timezone = randomEvent.Timezone
            };

 
            // Act
            var result = await _eventService.UpdateEventAsync(updateEventDto, randomEvent.OrganizerId, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.UpdateEventError, result.Message);
        }





        #endregion
    }
}
