using AutoMapper;
using Infrastructure.Services;
using Moq;

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
                Location = "Test location",
                EndDate = DateTimeOffset.UtcNow.AddHours(2),
                StartDate = DateTimeOffset.UtcNow,
                EventDescription = "A description for the test event",
                EventName = "Test Event",
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

        [Fact]
        public async Task CreateEventAsync_Should_Return_ErrorResult_When_DateRange_Is_InValid()
        {           
            // Arrange
            var validUserId = Guid.NewGuid();
            var createEventDto = new CreateEventDto
            {
                Location = "Test location",
                EndDate = DateTimeOffset.UtcNow,
                StartDate = DateTimeOffset.UtcNow.AddHours(2),
                EventDescription = "A description for the test event",
                EventName = "Test Event",
                Timezone = "UTC",
            };
            

            _userServiceMock.Setup(u => u.IsUserValidAsync(validUserId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _eventService.CreateEventAsync(createEventDto, validUserId, CancellationToken.None);
            //Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.InvalidDateRange, result.Message);
        }

        [Fact]
        public async Task CreateEventAsync_Should_Return_ErrorResult_When_Dto_Is_Not_Mapped()
        {
            // Arrange
            var validUserId = Guid.NewGuid();
            var createEventDto = new CreateEventDto
            {
                Location = "Test location",
                EndDate = DateTimeOffset.UtcNow.AddHours(2),
                StartDate = DateTimeOffset.UtcNow,
                EventDescription = "A description for the test event",
                EventName = "Test Event",
                Timezone = "UTC",
            };


            _userServiceMock.Setup(u => u.IsUserValidAsync(validUserId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _mapperMock.Setup(m => m.Map<Event>(createEventDto))
                .Returns((Event)null);
            // Act
            var result = await _eventService.CreateEventAsync(createEventDto, validUserId, CancellationToken.None);

            //Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.CreateEventError, result.Message);
        }
        
        [Fact]
        public async Task CreateEventAsync_Should_Return_ErrorResult_When_DatabaseInsert_Fails()
        {
            //Arrange
            var validUserId = Guid.NewGuid();
            var createEventDto = new CreateEventDto
            {
                Location = "Test location",
                EndDate = DateTimeOffset.UtcNow.AddHours(2),
                StartDate = DateTimeOffset.UtcNow,
                EventDescription = "A description for the test event",
                EventName = "Test Event",
                Timezone = "UTC",
            };
            var mappedEvent = new Event
            {
                Id = Guid.NewGuid(),
                EventName = createEventDto.EventName,
                EventDescription = createEventDto.EventDescription,
                StartDate = createEventDto.StartDate,
                EndDate = createEventDto.EndDate,
                Location = createEventDto.Location,
                Timezone = createEventDto.Timezone,
                OrganizerId = validUserId
            };
            _userServiceMock.Setup(u => u.IsUserValidAsync(validUserId, It.IsAny<CancellationToken>()))
              .ReturnsAsync(true);
            _mapperMock.Setup(m => m.Map<Event>(createEventDto))
            .Returns(mappedEvent);

            _eventRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Event>()))
            .ReturnsAsync(0);

            // Act
            var result = await _eventService.CreateEventAsync(createEventDto, validUserId, CancellationToken.None);

            //Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.CreateEventError, result.Message);
        }

        [Fact]
        public async Task CreateEventAsync_Should_Return_SuccessResult_When_All_Conditions_Are_Met()
        {
            //Arrange
            var validUserId = Guid.NewGuid();
            var createEventDto = new CreateEventDto
            {
                Location = "Test location",
                EndDate = DateTimeOffset.UtcNow.AddHours(2),
                StartDate = DateTimeOffset.UtcNow,
                EventDescription = "A description for the test event",
                EventName = "Test Event",
                Timezone = "UTC",
            };
            var mappedEvent = new Event
            {
                Id = Guid.NewGuid(),
                EventName = createEventDto.EventName,
                EventDescription = createEventDto.EventDescription,
                StartDate = createEventDto.StartDate,
                EndDate = createEventDto.EndDate,
                Location = createEventDto.Location,
                Timezone = createEventDto.Timezone,
                OrganizerId = validUserId
            };
            _userServiceMock.Setup(u => u.IsUserValidAsync(validUserId, It.IsAny<CancellationToken>()))
              .ReturnsAsync(true); 

            _mapperMock.Setup(m => m.Map<Event>(createEventDto))
                .Returns(mappedEvent);  

            _eventRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Event>()))
                .ReturnsAsync(1);

            //Act
            var result = await _eventService.CreateEventAsync(createEventDto, validUserId, CancellationToken.None);

            //Assert
            Assert.IsType<SuccessResult> (result);
            Assert.Equal(Messages.CreateEventSuccess, result.Message);

        }

        [Fact]
        public async Task CreateEventAsync_Should_Return_ErrorResult_When_EventName_Is_Too_Short()
        {
            // Arrange
            var validUserId = Guid.NewGuid();
            var createEventDto = new CreateEventDto
            {
                EventName = "A", // Çok kısa
                EndDate = DateTimeOffset.UtcNow.AddHours(2),
                StartDate = DateTimeOffset.UtcNow,
                Location = "Test location",
                Timezone = "UTC"
            };

            // Act
            var result = await _eventService.CreateEventAsync(createEventDto, validUserId, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.InvalidDto, result.Message);
        }







        #endregion
    }
}
