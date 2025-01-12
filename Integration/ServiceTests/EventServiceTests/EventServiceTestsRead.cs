using AutoMapper;
using Infrastructure.Services;
using Moq;
using System.Linq.Expressions;

namespace Integration.ServiceTests.EventServiceTestsRead
{
    public class EventServiceTestsRead : TestBase
    {
        private readonly EventService _eventService;
        private readonly IEventRepository _eventRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

         public EventServiceTestsRead(ApplicationFixture applicationFixture) : base(applicationFixture)
        {
            _eventRepository = ApplicationFixture.Services.GetRequiredService<IEventRepository>();
            _userService = ApplicationFixture.Services.GetRequiredService<IUserService>();
            _mapper = ApplicationFixture.Services.GetRequiredService<IMapper>();

            _eventService = new EventService(_eventRepository, _mapper, _userService);
        }


        #region ReadEvent
        [Fact]
        public async Task GetEventByIdAsync_Should_Return_ErrorDataResult_When_Event_Not_Found()
        {
            // Arrange
            var randomEventId = Guid.NewGuid();

            // Act
            var result = await _eventService.GetEventByIdAsync(randomEventId, CancellationToken.None);
            // Assert
            Assert.IsType<ErrorDataResult<ViewEventDto>>(result);
            Assert.Equal(Messages.EventNotFound, result.Message);

        }

        [Fact]
        public async Task GetEventByIdAsync_Should_Return_SuccessDataResult_When_Event_Found()
        {
            // Arrange
            var randomEvent = await this.RegisterAndGetRandomEventAsync();  

            // Act
            var result = await _eventService.GetEventByIdAsync(randomEvent.Id, CancellationToken.None);

            // Assert
            Assert.IsType<SuccessDataResult<ViewEventDto>>(result);
            Assert.NotNull(result.Data);
            Assert.Equal(randomEvent.EventName, result.Data.EventName);
            Assert.Equal(randomEvent.Location, result.Data.Location);
            Assert.Equal(randomEvent.StartDate, result.Data.StartDate);
            Assert.Equal(randomEvent.EndDate, result.Data.EndDate);
            Assert.Equal(randomEvent.Timezone, result.Data.Timezone);
        }

        [Fact]
        public async Task GetAllEventsAsync_Should_Return_ErrorDataResult_When_EventList_Is_Empty()
        {
            // Arrange
            Expression<Func<Event, bool>> predicate = e => e.Location == "NonExistentLocation";
            // Act
            var result = await _eventService.GetAllEventsAsync(predicate, CancellationToken.None);
            // Assert
            Assert.IsType<ErrorDataResult<IEnumerable<ViewEventDto>>>(result);
            Assert.Equal(Messages.EmptyEventList, result.Message);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GetAllEventsAsync_Should_Return_SuccessDataResult_When_EventList_Is_Not_Empty()
        {
            // Arrange
            var randomEvent1 = await this.RegisterAndGetRandomEventAsync();
            var randomEvent2 = await this.RegisterAndGetRandomEventAsync();
            Expression<Func<Event, bool>> predicate = e => true;

            // Act
            var result = await _eventService.GetAllEventsAsync(predicate, CancellationToken.None);
            // Assert
            Assert.IsType<SuccessDataResult<IEnumerable<ViewEventDto>>>(result);
            Assert.NotNull(result.Data);
            Assert.Contains(result.Data, e => e.EventName == randomEvent1.EventName); 
            Assert.Contains(result.Data, e => e.EventName == randomEvent2.EventName);
        }

        [Fact]
        public async Task GetAllEventsWithPaginationAsync_Should_Return_Error_When_EventsWithPagination_Is_Null()
        {
            // Arrange
            await ApplicationFixture.ClearDatabaseAsync();  

            var page = 1;
            var pageSize = 10;


            // Act
            var result = await _eventService.GetAllEventsWithPaginationAsync(page, pageSize, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorDataResult<PaginationDto<ViewEventDto>>>(result);
            Assert.Equal(Messages.EmptyEventList, result.Message);
        }

        [Fact]
        public async Task GetAllEventsWithPaginationAsync_Should_Return_SuccessResult_When_Data_Is_Not_Empty()
        {
            // Arrange
            await ApplicationFixture.ClearDatabaseAsync();

            var page = 1;
            var pageSize = 5;
            var randomEvent = await this.RegisterAndGetRandomEventAsync();
            // Act
            var result = await _eventService.GetAllEventsWithPaginationAsync(page, pageSize, CancellationToken.None);

            // Assert
            Assert.IsType<SuccessDataResult<PaginationDto<ViewEventDto>>>(result);
            Assert.Single(result.Data.Data);

        }

        [Fact]
        public async Task GetEventWithParticipantsAsync_Should_Return_EventNotFound_When_Event_Does_Not_Exist()
        {
            // Arrange
            var eventId = Guid.NewGuid();

            // Act
            var result = await _eventService.GetEventWithParticipantsAsync(eventId, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorDataResult<ViewEventWithParticipantsDto>>(result);
            Assert.Equal(Messages.EventNotFound, result.Message);
        }


        [Fact]
        public async Task GetEventWithParticipantsAsync_Should_Return_ErrorDataResult_When_ParticipantsList_Is_Empty()
        {
        // Arrange
        var randomEvent = await this.RegisterAndGetRandomEventAsync();

        // Act
        var result = await _eventService.GetEventWithParticipantsAsync(randomEvent.Id, CancellationToken.None);

        // Assert
        Assert.IsType<ErrorDataResult<ViewEventWithParticipantsDto>>(result);
        Assert.Equal(Messages.EmptyParticipantList, result.Message);

        }

        [Fact]
        public async Task GetEventWithParticipantsAsync_Should_Return_SuccessDataResult_When_All_Conditions_Are_Met()
        {
            // Arrange
            using (var scope = ApplicationFixture.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<EventApiDbContext>();

                var randomEvent = await this.RegisterAndGetRandomEventAsync();

                var participant1 = await this.RegisterAndGetRandomUserAsync();
                var participant2 = await this.RegisterAndGetRandomUserAsync();

                await dbContext.Participants.AddRangeAsync(
                    new Participant { UserId = participant1.Id, EventId = randomEvent.Id },
                    new Participant { UserId = participant2.Id, EventId = randomEvent.Id }
                );
                await dbContext.SaveChangesAsync();

                // Act
                var eventService = scope.ServiceProvider.GetRequiredService<IEventService>();
                var result = await eventService.GetEventWithParticipantsAsync(randomEvent.Id, CancellationToken.None);

                // Assert
                Assert.IsType<SuccessDataResult<ViewEventWithParticipantsDto>>(result);
                Assert.NotNull(result.Data);
                Assert.Equal(2, result.Data.Participants.Count());
            }
        }


        #endregion
    }
}
