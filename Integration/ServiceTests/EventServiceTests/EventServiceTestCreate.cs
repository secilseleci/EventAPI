using AutoMapper;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Repositories.Implementations;
using Infrastructure.Services;
 

namespace Integration.ServiceTests.EventServiceTestsCreate
{
    public class EventServiceTestsCreate : TestBase
    {
        private readonly EventService _eventService;
        private readonly IEventRepository _eventRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public EventServiceTestsCreate(ApplicationFixture applicationFixture) : base(applicationFixture)
        {
            _eventRepository = ApplicationFixture.Services.GetRequiredService<IEventRepository>();
            _userService = ApplicationFixture.Services.GetRequiredService<IUserService>();
            _mapper = ApplicationFixture.Services.GetRequiredService<IMapper>();

            _eventService = new EventService(_eventRepository, _mapper, _userService);
        }
        #region CreateEvent
        [Fact]
        public async Task CreateEventAsync_Should_Return_UserNotFound_When_User_Is_Invalid()
        {
            // Arrange
            var invalidUserId = Guid.NewGuid();
          
            // Act
            var result = await _eventService.CreateEventAsync(null, invalidUserId, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.UserNotFound, result.Message);

        }

      
     

        
        [Fact]
        public async Task CreateEventAsync_Should_Return_SuccessResult_When_All_Conditions_Are_Met()
        {
            // Arrange
            var validUser = await this.RegisterAndGetRandomUserAsync();
            var createEventDto = this.CreateRandomEventDto();

            // Act
            var result = await _eventService.CreateEventAsync(createEventDto, validUser.Id, CancellationToken.None);

            // Assert
            Assert.IsType<SuccessResult>(result);
            Assert.Equal(Messages.CreateEventSuccess, result.Message);

            var eventInDb = await _eventRepository.GetAllAsync(e => e.EventName == createEventDto.EventName);
            Assert.NotEmpty(eventInDb);
        }

        #endregion


    }
}

