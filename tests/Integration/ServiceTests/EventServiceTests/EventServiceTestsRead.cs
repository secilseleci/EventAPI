//using AutoMapper;
//using Infrastructure.Services;
//using Moq;
//using System.Linq.Expressions;

//namespace Integration.ServiceTests.EventServiceTestsRead
//{
//    public class EventServiceTestsRead : TestBase
//    {
//        private readonly Mock<IEventRepository> _eventRepositoryMock;
//        private readonly Mock<IMapper> _mapperMock;
//        private readonly Mock<IUserService> _userServiceMock;

//        private readonly EventService _eventService;
//        public EventServiceTestsRead(ApplicationFixture applicationFixture) : base(applicationFixture)
//        {  
//            _eventRepositoryMock = new Mock<IEventRepository>();
//            _mapperMock = new Mock<IMapper>();
//            _userServiceMock = new Mock<IUserService>();

           
//            _eventService = new EventService(
//                _eventRepositoryMock.Object,
//                _mapperMock.Object,
//                _userServiceMock.Object
//            );
//        }
      

//        #region ReadEvent
//        [Fact]
//        public async Task GetEventByIdAsync_Should_Return_ErrorDataResult_When_Event_Not_Found()
//        {
//            // Arrange
//            var eventId= Guid.NewGuid();
//            _eventRepositoryMock.Setup(e => e.GetByIdAsync(eventId))
//                .ReturnsAsync((Event)null);
//            // Act
//            var result= await _eventService.GetEventByIdAsync(eventId, CancellationToken.None);
//            // Assert
//            Assert.IsType<ErrorDataResult<ViewEventDto>>(result);
//            Assert.Equal(Messages.EventNotFound, result.Message);

//        }

//        [Fact]
//        public async Task GetEventByIdAsync_Should_Return_SuccessDataResult_When_Event_Found()
//        {
//            // Arrange
//            var eventId = Guid.NewGuid();
//            var eventEntity = new Event
//            {
//                Id = eventId,
//                EventName = "Test Event",
//                EventDescription = "A sample test event",
//                StartDate = DateTimeOffset.UtcNow,
//                EndDate = DateTimeOffset.UtcNow.AddHours(2),
//                Location = "Test Location",
//                Timezone = "UTC",
//                OrganizerId = Guid.NewGuid()
//            };

//            var viewEventDto = new ViewEventDto
//            {
//                EventName = eventEntity.EventName,
//                EventDescription = eventEntity.EventDescription,
//                StartDate = eventEntity.StartDate,
//                EndDate = eventEntity.EndDate,
//                Location = eventEntity.Location,
//            };
//            _eventRepositoryMock.Setup(e => e.GetByIdAsync(eventId))
//                .ReturnsAsync(eventEntity);
//            _mapperMock.Setup(m => m.Map<ViewEventDto>(eventEntity))
//            .Returns(viewEventDto);
//            // Act
//            var result = await _eventService.GetEventByIdAsync(eventId, CancellationToken.None);
//            // Assert
//            Assert.IsType<SuccessDataResult<ViewEventDto>>(result);
//            Assert.Equal(viewEventDto, result.Data); 
 
//        }

//        [Fact]
//        public async Task GetAllEventsAsync_Should_Return_ErrorDataResult_When_EventList_Is_Empty()
//        {
//            // Arrange
//            Expression<Func<Event, bool>> predicate = e => true;
//            _eventRepositoryMock.Setup(e => e.GetAllAsync(predicate))
//       .ReturnsAsync(new List<Event>()); // Boş liste

//            // Act
//            var result =await _eventService.GetAllEventsAsync(predicate,CancellationToken.None);
//            // Assert
//            Assert.IsType<ErrorDataResult<IEnumerable<ViewEventDto>>>(result);
//            Assert.Equal(Messages.EmptyEventList, result.Message);

//        }

//        [Fact]
//        public async Task GetAllEventsAsync_Should_Return_SuccessDataResult_When_EventList_Is_Not_Empty()
//        {
//            // Arrange
//            Expression<Func<Event, bool>> predicate = e => true;
//            var eventList= new List<Event>
//            {
//                new Event
//        {
//            Id = Guid.NewGuid(),
//            EventName = "Test Event 1",
//            StartDate = DateTimeOffset.UtcNow.AddDays(1),
//            EndDate = DateTimeOffset.UtcNow.AddDays(2),
//            Location = "Location 1",
//            OrganizerId = Guid.NewGuid(),
//            Timezone = "UTC"
//        },
//                new Event
//        {
//            Id = Guid.NewGuid(),
//            EventName = "Test Event 2",
//            StartDate = DateTimeOffset.UtcNow.AddDays(2),
//            EndDate = DateTimeOffset.UtcNow.AddDays(3),
//            Location = "Location 2",
//            OrganizerId = Guid.NewGuid(),
//            Timezone = "UTC"
//        }
//            };
//            var mappedEventList = eventList.Select(e => new ViewEventDto
//            {
//                EventName = e.EventName,
//                StartDate = e.StartDate,
//                EndDate = e.EndDate,
//                Location = e.Location,
//                Timezone = e.Timezone
//            });

//            _eventRepositoryMock.Setup(e => e.GetAllAsync(predicate))
//                .ReturnsAsync(eventList);

//            _mapperMock.Setup(m => m.Map<IEnumerable<ViewEventDto>>(eventList))
//                .Returns(mappedEventList);
//            // Act
//            var result = await _eventService.GetAllEventsAsync(predicate, CancellationToken.None);
//            // Assert
//            Assert.IsType<SuccessDataResult<IEnumerable<ViewEventDto>>>(result);
//            result.Data.Should().BeEquivalentTo(mappedEventList); // Koleksiyon içeriklerini karşılaştır

//        }

//        [Fact]
//        public async Task GetAllEventsWithPaginationAsync_Should_Return_Error_When_EventsWithPagination_Is_Null()
//        {
//            // Arrange
//            var page = 1;
//            var pageSize = 10;

//            _eventRepositoryMock.Setup(r => r.GetAllEventsWithPaginationAsync(page, pageSize))
//                .ReturnsAsync((PaginationDto<Event>?)null);

//            // Act
//            var result = await _eventService.GetAllEventsWithPaginationAsync(page, pageSize, CancellationToken.None);

//            // Assert
//            Assert.IsType<ErrorDataResult<PaginationDto<ViewEventDto>>>(result);
//            Assert.Equal(Messages.EmptyEventList, result.Message);
//        }

//        [Fact]
//        public async Task GetAllEventsWithPaginationAsync_Should_Return_Error_When_EventsWithPaginationData_Is_Null()
//        {
//            // Arrange
//            var page = 1;
//            var pageSize = 10;

//            var paginationDto = new PaginationDto<Event>
//            {
//                Data = null,
//                CurrentPage = page,
//                TotalPages = 1,
//                PageSize = pageSize,
//                TotalCount = 0
//            };

//            _eventRepositoryMock.Setup(r => r.GetAllEventsWithPaginationAsync(page, pageSize))
//                .ReturnsAsync(paginationDto);

//            // Act
//            var result = await _eventService.GetAllEventsWithPaginationAsync(page, pageSize, CancellationToken.None);

//            // Assert
//            Assert.IsType<ErrorDataResult<PaginationDto<ViewEventDto>>>(result);
//            Assert.Equal(Messages.EmptyEventList, result.Message);
//        }

    
//        [Fact]
//        public async Task GetAllEventsWithPaginationAsync_Should_Return_SuccessResult_When_Data_Is_Not_Empty()
//        {
//            // Arrange
//            var page = 1;
//            var pageSize = 5;

//            var eventList = new List<Event>
//    {
//        new Event { Id = Guid.NewGuid(), EventName = "Test Event 1", StartDate = DateTimeOffset.UtcNow, EndDate = DateTimeOffset.UtcNow.AddHours(2), Location = "Location 1", OrganizerId = Guid.NewGuid(), Timezone = "UTC" },
//        new Event { Id = Guid.NewGuid(), EventName = "Test Event 2", StartDate = DateTimeOffset.UtcNow.AddHours(3), EndDate = DateTimeOffset.UtcNow.AddHours(4), Location = "Location 2", OrganizerId = Guid.NewGuid(), Timezone = "UTC" }
//    };

//            var paginationDto = new PaginationDto<Event>
//            {
//                Data = eventList,
//                CurrentPage = page,
//                TotalPages = 1,
//                PageSize = pageSize,
//                TotalCount = 2
//            };

//            var mappedEventList = eventList.Select(e => new ViewEventDto
//            {
//                EventName = e.EventName,
//                StartDate = e.StartDate,
//                EndDate = e.EndDate,
//                Location = e.Location,
//                Timezone = e.Timezone
//            });

//            _eventRepositoryMock.Setup(r => r.GetAllEventsWithPaginationAsync(page, pageSize))
//                .ReturnsAsync(paginationDto);

//            _mapperMock.Setup(m => m.Map<IEnumerable<ViewEventDto>>(eventList))
//                .Returns(mappedEventList);

//            // Act
//            var result = await _eventService.GetAllEventsWithPaginationAsync(page, pageSize, CancellationToken.None);

//            // Assert
//            Assert.IsType<SuccessDataResult<PaginationDto<ViewEventDto>>>(result);
//            Assert.Collection(result.Data.Data,
//             item1 =>
//             {
//                 Assert.Equal("Test Event 1", item1.EventName);
//                 Assert.Equal("Location 1", item1.Location);
//             },
//             item2 =>
//             {
//                 Assert.Equal("Test Event 2", item2.EventName);
//                 Assert.Equal("Location 2", item2.Location);
//             });
//        }

//        [Fact]
//        public async Task GetEventWithParticipantsAsync_Should_Return_EventNotFound_When_Event_Does_Not_Exist()
//        {
//            // Arrange
//            var eventId = Guid.NewGuid();

//            _eventRepositoryMock.Setup(e => e.GetEventWithParticipantsAsync(eventId))
//                .ReturnsAsync((Event)null);

//            // Act
//            var result = await _eventService.GetEventWithParticipantsAsync(eventId, CancellationToken.None);

//            // Assert
//            Assert.IsType<ErrorDataResult<ViewEventWithParticipantsDto>>(result);
//            Assert.Equal(Messages.EventNotFound, result.Message);
//        }
//        [Fact]
//        public async Task GetEventWithParticipantsAsync_Should_Return_ErrorDataResult_When_ParticipantsList_Is_Empty()
//        {
//            // Arrange
//            var eventId = Guid.NewGuid();
//            var eventEntity = new Event
//            {
//                Id = eventId,
//                EventName = "Test Event",
//                Location = "Test location",
//                EndDate = DateTimeOffset.UtcNow.AddHours(2),
//                StartDate = DateTimeOffset.UtcNow,
//                EventDescription = "A description for the test event",
//                Timezone = "UTC",
//                Participants = new List<Participant>()
//            };
//            _eventRepositoryMock.Setup(e => e.GetEventWithParticipantsAsync(eventId))
//                .ReturnsAsync(eventEntity);
          
//            // Act
//            var result = await _eventService.GetEventWithParticipantsAsync(eventId, CancellationToken.None);

//            // Assert
//            Assert.IsType<ErrorDataResult<ViewEventWithParticipantsDto>>(result);
//            Assert.Equal(Messages.EmptyParticipantList, result.Message);

//        }

//        [Fact]
//        public async Task GetEventWithParticipantsAsync_Should_Return_SuccessDataResult_When_All_Conditions_Are_Met()
//        {
//            // Arrange
//            var eventId = Guid.NewGuid();
//            var eventEntity = new Event
//            {
//                Id = eventId,
//                EventName = "Test Event",
//                Location = "Test location",
//                EndDate = DateTimeOffset.UtcNow.AddHours(2),
//                StartDate = DateTimeOffset.UtcNow,
//                EventDescription = "A description for the test event",
//                Timezone = "UTC",
//                Participants = new List<Participant>
//                {
//                    new Participant
//                    {
//                        UserId = new Guid()
//                    },

//                }
//            };

          
//            _eventRepositoryMock.Setup(e => e.GetEventWithParticipantsAsync(eventId))
//                .ReturnsAsync(eventEntity);
//            _mapperMock.Setup(m => m.Map<ViewEventWithParticipantsDto>(eventEntity))
//                .Returns(new ViewEventWithParticipantsDto{

                 
//                    Id = eventId,
//                    EventName = eventEntity.EventName,
//                    EventDescription = eventEntity.EventDescription,
//                    StartDate = eventEntity.StartDate,
//                    EndDate = eventEntity.EndDate,
//                    Timezone = eventEntity.Timezone,
//                     Participants = eventEntity.Participants.Select(p => new ParticipantDto
//                     { 
//                         UserId = p.UserId
                         
                        
//                     }).ToList()
                 
//            //});
//            // Act
//            var result = await _eventService.GetEventWithParticipantsAsync(eventId, CancellationToken.None);

//            // Assert
//            Assert.IsType<SuccessDataResult<ViewEventWithParticipantsDto>>(result);
//            Assert.NotNull(result.Data);
//            Assert.Equal(eventEntity.EventName, result.Data.EventName);
//            Assert.Equal(eventEntity.Participants.Count, result.Data.Participants.Count);
//        }
//        #endregion
//    }
//}

