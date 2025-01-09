using AutoMapper;
using Infrastructure.Repositories.Implementations;
using Infrastructure.Services;
using Moq;

namespace Integration.ServiceTests.EventServiceTestsDelete
{
    public class EventServiceTestsDelete : TestBase
    {
        private readonly EventService _eventService;
        private readonly IEventRepository _eventRepository;  
        private readonly IUserService _userService;  
        private readonly IMapper _mapper;

        public EventServiceTestsDelete(ApplicationFixture applicationFixture) : base(applicationFixture)
        {
            _eventRepository = ApplicationFixture.Services.GetRequiredService<IEventRepository>();
            _userService = ApplicationFixture.Services.GetRequiredService<IUserService>();
            _mapper = ApplicationFixture.Services.GetRequiredService<IMapper>();

            _eventService = new EventService(_eventRepository, _mapper, _userService);
        }

        #region DeleteEvent
        [Fact]
        public async Task DeleteEventAsync_Should_Return_EventNotFound_When_Event_Does_Not_Exist()
        {
            //Arrange
            var validUser = await this.RegisterAndGetRandomUserAsync(); // Sistemde geçerli bir kullanıcı
            var nonExistentEventId = Guid.NewGuid(); // Sistemde olmayan bir Event ID

             
            //Act
            var result=await _eventService.DeleteEventAsync(nonExistentEventId, validUser.Id, CancellationToken.None);
            //Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.EventNotFound, result.Message);
        }

        [Fact]
        public async Task DeleteEventAsync_Should_Return_UnauthorizedAccess_When_User_Is_Not_Organizer()
        {
            // Arrange
            var validUser = await this.RegisterAndGetRandomUserAsync(); // Sistemde geçerli bir kullanıcı
            var randomEvent = await this.RegisterAndGetRandomEventAsync(); // Rastgele bir event oluşturuluyor

            var eventInDb = await _eventRepository.GetByIdAsync(randomEvent.Id);
            Assert.NotNull(eventInDb);
            // Act
            var result = await _eventService.DeleteEventAsync(randomEvent.Id, validUser.Id, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.UnauthorizedAccess, result.Message);
        }


        [Fact]
        public async Task DeleteEventAsync_Should_Return_SuccessResult_When_All_Conditions_Are_Met()
        {
            // Arrange
            var validUser = await this.RegisterAndGetRandomUserAsync(); // Sistemde geçerli bir kullanıcı
            var randomEvent = await this.RegisterAndGetRandomEventAsync();

            randomEvent.OrganizerId = validUser.Id;
            await _eventRepository.UpdateAsync(randomEvent);

            var eventInDb = await _eventRepository.GetByIdAsync(randomEvent.Id);
            Assert.NotNull(eventInDb);

            // Act
            var result = await _eventService.DeleteEventAsync(randomEvent.Id, validUser.Id, CancellationToken.None);

            // Assert
            Assert.IsType<SuccessResult>(result);
            Assert.Equal(Messages.DeleteEventSuccess, result.Message);

            var deletedEvent = await _eventRepository.GetByIdAsync(randomEvent.Id);
            Assert.Null(deletedEvent);
        }

        #endregion

    }
}

