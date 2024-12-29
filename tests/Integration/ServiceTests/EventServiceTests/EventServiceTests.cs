using AutoMapper;
using Core.DTOs.Event;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Utilities.Constants;
using Core.Utilities.Results;
using Infrastructure.Services;
using Integration.Base;
using Integration.Fixtures;
using Moq;
using Xunit;

namespace Integration.ServiceTests.EventServiceTests
{
    public class EventServiceTests : TestBase
    {
        private readonly Mock<IEventRepository> _eventRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUserService> _userServiceMock;

        private readonly EventService _eventService;
        public EventServiceTests(ApplicationFixture applicationFixture) : base(applicationFixture)
        {  
            _eventRepositoryMock = new Mock<IEventRepository>();
            _mapperMock = new Mock<IMapper>();
            _userServiceMock = new Mock<IUserService>();

           
            _eventService = new EventService(
                _eventRepositoryMock.Object,
                _mapperMock.Object,
                _userServiceMock.Object
            );
        }
        #region CreateEvent
        [Fact]
        public async Task CreateEventAsync_Should_Return_UserNotFound_When_User_Is_Invalid()
        {
            // Arrange
            var invalidUserId = Guid.NewGuid();
          
            _userServiceMock.Setup(u => u.IsUserValidAsync(invalidUserId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(false);  

            // Act
            var result = await _eventService.CreateEventAsync(null, invalidUserId, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.UserNotFound, result.Message);

        }

        [Fact]
        public async Task CreateEventAsync_Should_Return_SuccessResult_When_User_Is_Valid()
        {
            // Arrange
            var validUserId = Guid.NewGuid();
            var createEventDto = new CreateEventDto
            {
                Location = "test location",
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                EventDescription = "Test Event",
                EventName = "TestEvent",
                Timezone="UTC",
            };

            var mappedEvent = new Event
            {
                Id = Guid.NewGuid(),
                EventName = createEventDto.EventName,
                EventDescription = createEventDto.EventDescription,
                StartDate = createEventDto.StartDate,
                EndDate = createEventDto.EndDate,
                Location = createEventDto.Location,
                Timezone= createEventDto.Timezone,
                OrganizerId = validUserId
            };

            _userServiceMock.Setup(u=>u.IsUserValidAsync(validUserId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _mapperMock.Setup(m => m.Map<Event>(createEventDto))
                .Returns(mappedEvent);

            _eventRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Event>()))
                .ReturnsAsync(1);

            // Act
            var result = await _eventService.CreateEventAsync(createEventDto, validUserId, CancellationToken.None);

            //Assert
            Assert.IsType<SuccessResult>(result);
            Assert.Equal(Messages.CreateEventSuccess, result.Message);
        } 
        #endregion
    }
}
