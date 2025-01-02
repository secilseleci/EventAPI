using AutoMapper;
using Core.Utilities.Results;
using Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq.Expressions;

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


        #endregion
        #region DeleteEvent
        [Fact]
        public async Task DeleteEventAsync_Should_Return_EventNotFound_When_Event_Does_Not_Exist()
        {
            //Arrange
            var validUserId = Guid.NewGuid();
            var eventId = Guid.NewGuid();

            _eventRepositoryMock.Setup(e => e.GetByIdAsync(eventId))
            .ReturnsAsync((Event)null);

            //Act
            var result=await _eventService.DeleteEventAsync(eventId, validUserId, CancellationToken.None);
            //Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.EventNotFound, result.Message);
        }

        [Fact]
        public async Task DeleteEventAsync_Should_Return_UnauthorizedAccess_When_User_Is_Not_Organizer()
        {
            //Arrange
            var validUserId = Guid.NewGuid();
            var eventId = Guid.NewGuid();

            var eventEntity = new Event
            {
                Id = eventId,
                OrganizerId = Guid.NewGuid(),
                EventName = "Test Event",
                Location = "Test location",
                EndDate = DateTimeOffset.UtcNow.AddHours(2),
                StartDate = DateTimeOffset.UtcNow,
                EventDescription = "A description for the test event",
                Timezone = "UTC",
            };
            _eventRepositoryMock.Setup(e => e.GetByIdAsync(eventId))
            .ReturnsAsync(eventEntity);

            //Act
            var result = await _eventService.DeleteEventAsync(eventId, validUserId, CancellationToken.None);
            //Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.UnauthorizedAccess, result.Message);
        }

        [Fact]
        public async Task DeleteEventAsync_Should_Return_ErrorResult_When_DatabaseRemove_Fails()
        {
            //Arrange
            var validUserId = Guid.NewGuid();
            var eventId = Guid.NewGuid();
            var eventEntity = new Event
            {
                Id = eventId,
                OrganizerId = validUserId,
                EventName = "Test Event",
                Location = "Test location",
                EndDate = DateTimeOffset.UtcNow.AddHours(2),
                StartDate = DateTimeOffset.UtcNow,
                EventDescription = "A description for the test event",
                Timezone = "UTC",
            };
            _eventRepositoryMock.Setup(e => e.GetByIdAsync(eventId))
            .ReturnsAsync(eventEntity);
            _eventRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Guid>()))
            .ReturnsAsync(0);
            //Act
            var result = await _eventService.DeleteEventAsync(eventId, validUserId, CancellationToken.None);
            //Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.DeleteEventError, result.Message);
        }

        [Fact]
        public async Task DeleteEventAsync_Should_Return_SuccessResult_When_All_Conditions_Are_Met()
        {
            //Arrange
            var validUserId = Guid.NewGuid();
            var eventId = Guid.NewGuid();
            var eventEntity = new Event
            {
                Id = eventId,
                OrganizerId = validUserId,
                EventName = "Test Event",
                Location = "Test location",
                EndDate = DateTimeOffset.UtcNow.AddHours(2),
                StartDate = DateTimeOffset.UtcNow,
                EventDescription = "A description for the test event",
                Timezone = "UTC",
            };
            _eventRepositoryMock.Setup(e => e.GetByIdAsync(eventId))
            .ReturnsAsync(eventEntity);
            _eventRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Guid>()))
            .ReturnsAsync(1);
            //Act
            var result = await _eventService.DeleteEventAsync(eventId, validUserId, CancellationToken.None);
            //Assert
            Assert.IsType<SuccessResult>(result);
            Assert.Equal(Messages.DeleteEventSuccess, result.Message);

        }

        #endregion
        #region UpdateEvent
        [Fact]
        public async Task UpdateEventAsync_Should_Return_EventNotFound_When_Event_Does_Not_Exist()
        {
            //Arrange
            var validUserId = Guid.NewGuid();
            var updateEventDto = new UpdateEventDto
            {
                Id = Guid.NewGuid(),
                Location = "Test location",
                EndDate = DateTimeOffset.UtcNow.AddHours(2),
                StartDate = DateTimeOffset.UtcNow,
                EventDescription = "A description for the test event",
                EventName = "Test Event",
                Timezone = "UTC",
            };

            _eventRepositoryMock.Setup(e => e.GetByIdAsync(updateEventDto.Id))
            .ReturnsAsync((Event)null);

            //Act
            var result = await _eventService.UpdateEventAsync(updateEventDto, validUserId, CancellationToken.None);
            //Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.EventNotFound, result.Message);
        }

        [Fact]
        public async Task UpdateEventAsync_Should_Return_UnauthorizedAccess_When_User_Is_Not_Organizer()
        {
            //Arrange
            var validUserId = Guid.NewGuid();
            var updateEventDto = new UpdateEventDto
            {
                Id = Guid.NewGuid(),
                Location = "Test location",
                EndDate = DateTimeOffset.UtcNow.AddHours(2),
                StartDate = DateTimeOffset.UtcNow,
                EventDescription = "A description for the test event",
                EventName = "Test Event",
                Timezone = "UTC",
                
            };

            var mappedEvent = new Event
            {
                Id = updateEventDto.Id,
                Location = updateEventDto.Location,
                Timezone = updateEventDto.Timezone,
                EndDate = updateEventDto.EndDate,
                StartDate = updateEventDto.StartDate,
                EventDescription = updateEventDto.EventDescription,
                EventName = updateEventDto.EventName,
                OrganizerId = Guid.NewGuid(),
            };
            _eventRepositoryMock.Setup(e => e.GetByIdAsync(updateEventDto.Id))
            .ReturnsAsync(mappedEvent);
            //Act
            var result = await _eventService.UpdateEventAsync(updateEventDto, validUserId, CancellationToken.None);
            //Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.UnauthorizedAccess, result.Message);
        }
        [Fact]
        public async Task UpdateEventAsync_Should_Return_ErrorResult_When_DateRange_Is_InValid()
        {
            // Arrange
            var validUserId = Guid.NewGuid();
            var updateEventDto = new UpdateEventDto
            {
                Id = Guid.NewGuid(),
                Location = "Test location",
                EndDate = DateTimeOffset.UtcNow, // Bitiş tarihi, başlangıç tarihinden önce
                StartDate = DateTimeOffset.UtcNow.AddHours(2),
                EventDescription = "A description for the test event",
                EventName = "Test Event",
                Timezone = "UTC",
            };

            var eventEntity = new Event
            {
                Id = updateEventDto.Id,
                OrganizerId = validUserId,
                EventName = "Old Event",
                EventDescription = "Old Description",
                StartDate = DateTimeOffset.UtcNow.AddHours(-1),
                EndDate = DateTimeOffset.UtcNow,
                Location = "Old Location",
                Timezone = "PST",
            };

            _eventRepositoryMock.Setup(e => e.GetByIdAsync(updateEventDto.Id))
                .ReturnsAsync(eventEntity); // Sistemdeki mevcut event

            // Act
            var result = await _eventService.UpdateEventAsync(updateEventDto, validUserId, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.InvalidDateRange, result.Message);
        }

        [Fact]
        public async Task UpdateEventAsync_Should_Return_ErrorResult_When_Entity_Not_Updated()
        {
            // Arrange
            var validUserId = Guid.NewGuid();
            var updateEventDto = new UpdateEventDto
            {
                Id = Guid.NewGuid(),
                Location = "Updated Location",
                EndDate = DateTimeOffset.UtcNow.AddHours(2),
                StartDate = DateTimeOffset.UtcNow,
                EventDescription = "Updated Description",
                EventName = "Updated Event",
                Timezone = "UTC",
            };

            var eventEntity = new Event
            {
                Id = updateEventDto.Id,
                OrganizerId = validUserId,
                EventName = "Old Event",
                EventDescription = "Old Description",
                StartDate = DateTimeOffset.UtcNow.AddHours(-1),
                EndDate = DateTimeOffset.UtcNow,
                Location = "Old Location",
                Timezone = "PST",
            };

            _eventRepositoryMock.Setup(e => e.GetByIdAsync(updateEventDto.Id))
                .ReturnsAsync(eventEntity);

            // Mock: UpdateAsync çağrıldığında eski değerlerin hâlâ geçerli olduğunu kontrol et
            _eventRepositoryMock.Setup(r => r.UpdateAsync(It.Is<Event>(e =>
                e.EventName == "Old Event" && // Değerler değişmemiş
                e.Location == "Old Location"
            ))).ReturnsAsync(0); // Başarısız güncelleme

            // Act
            var result = await _eventService.UpdateEventAsync(updateEventDto, validUserId, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.UpdateEventError, result.Message);
        }
        [Fact]
        public async Task UpdateEventAsync_Should_Return_SuccessResult_When_All_Conditions_Are_Met()
        {
            // Arrange
            var validUserId = Guid.NewGuid();
            var updateEventDto = new UpdateEventDto
            {
                Id = Guid.NewGuid(),
                Location = "Updated Location",
                EndDate = DateTimeOffset.UtcNow.AddHours(2),
                StartDate = DateTimeOffset.UtcNow,
                EventDescription = "Updated Description",
                EventName = "Updated Event",
                Timezone = "UTC",
            };

            var eventEntity = new Event
            {
                Id = updateEventDto.Id,
                OrganizerId = validUserId,
                EventName = "Old Event",
                EventDescription = "Old Description",
                StartDate = DateTimeOffset.UtcNow.AddHours(-1),
                EndDate = DateTimeOffset.UtcNow,
                Location = "Old Location",
                Timezone = "PST",
            };

            _eventRepositoryMock.Setup(e => e.GetByIdAsync(updateEventDto.Id ))
                .ReturnsAsync(eventEntity);

            _eventRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Event>()))
                .ReturnsAsync(1); // Başarılı güncelleme

            // Act
            var result = await _eventService.UpdateEventAsync(updateEventDto, validUserId, CancellationToken.None);

            // Assert
            Assert.IsType<SuccessResult>(result);
            Assert.Equal(Messages.UpdateEventSuccess, result.Message);
        }

        #endregion

        #region ReadEvent
        [Fact]
        public async Task GetEventByIdAsync_Should_Return_ErrorDataResult_When_Event_Not_Found()
        {
            // Arrange
            var eventId= Guid.NewGuid();
            _eventRepositoryMock.Setup(e => e.GetByIdAsync(eventId))
                .ReturnsAsync((Event)null);
            // Act
            var result= await _eventService.GetEventByIdAsync(eventId, CancellationToken.None);
            // Assert
            Assert.IsType<ErrorDataResult<ViewEventDto>>(result);
            Assert.Equal(Messages.EventNotFound, result.Message);

        }

        [Fact]
        public async Task GetEventByIdAsync_Should_Return_SuccessDataResult_When_Event_Found()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var eventEntity = new Event
            {
                Id = eventId,
                EventName = "Test Event",
                EventDescription = "A sample test event",
                StartDate = DateTimeOffset.UtcNow,
                EndDate = DateTimeOffset.UtcNow.AddHours(2),
                Location = "Test Location",
                Timezone = "UTC",
                OrganizerId = Guid.NewGuid()
            };

            var viewEventDto = new ViewEventDto
            {
                EventName = eventEntity.EventName,
                EventDescription = eventEntity.EventDescription,
                StartDate = eventEntity.StartDate,
                EndDate = eventEntity.EndDate,
                Location = eventEntity.Location,
            };
            _eventRepositoryMock.Setup(e => e.GetByIdAsync(eventId))
                .ReturnsAsync(eventEntity);
            _mapperMock.Setup(m => m.Map<ViewEventDto>(eventEntity))
            .Returns(viewEventDto);
            // Act
            var result = await _eventService.GetEventByIdAsync(eventId, CancellationToken.None);
            // Assert
            Assert.IsType<SuccessDataResult<ViewEventDto>>(result);
            Assert.Equal(viewEventDto, result.Data); 
 
        }

        [Fact]
        public async Task GetAllEventsAsync_Should_Return_ErrorDataResult_When_EventList_Is_Empty()
        {
            // Arrange
            Expression<Func<Event, bool>> predicate = e => true;
            _eventRepositoryMock.Setup(e => e.GetAllAsync(predicate))
       .ReturnsAsync(new List<Event>()); // Boş liste

            // Act
            var result =await _eventService.GetAllEventsAsync(predicate,CancellationToken.None);
            // Assert
            Assert.IsType<ErrorDataResult<IEnumerable<ViewEventDto>>>(result);
            Assert.Equal(Messages.EmptyEventList, result.Message);

        }

        [Fact]
        public async Task GetAllEventsAsync_Should_Return_SuccessDataResult_When_EventList_Is_Not_Empty()
        {
            // Arrange
            Expression<Func<Event, bool>> predicate = e => true;
            var eventList= new List<Event>
            {
                new Event
        {
            Id = Guid.NewGuid(),
            EventName = "Test Event 1",
            StartDate = DateTimeOffset.UtcNow.AddDays(1),
            EndDate = DateTimeOffset.UtcNow.AddDays(2),
            Location = "Location 1",
            OrganizerId = Guid.NewGuid(),
            Timezone = "UTC"
        },
                new Event
        {
            Id = Guid.NewGuid(),
            EventName = "Test Event 2",
            StartDate = DateTimeOffset.UtcNow.AddDays(2),
            EndDate = DateTimeOffset.UtcNow.AddDays(3),
            Location = "Location 2",
            OrganizerId = Guid.NewGuid(),
            Timezone = "UTC"
        }
            };
            var mappedEventList = eventList.Select(e => new ViewEventDto
            {
                EventName = e.EventName,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Location = e.Location,
                Timezone = e.Timezone
            });

            _eventRepositoryMock.Setup(e => e.GetAllAsync(predicate))
                .ReturnsAsync(eventList);

            _mapperMock.Setup(m => m.Map<IEnumerable<ViewEventDto>>(eventList))
                .Returns(mappedEventList);
            // Act
            var result = await _eventService.GetAllEventsAsync(predicate, CancellationToken.None);
            // Assert
            Assert.IsType<SuccessDataResult<IEnumerable<ViewEventDto>>>(result);
            result.Data.Should().BeEquivalentTo(mappedEventList); // Koleksiyon içeriklerini karşılaştır

        }

        [Fact]
        public async Task GetAllEventsWithPaginationAsync_Should_Return_Error_When_EventsWithPagination_Is_Null()
        {
            // Arrange
            var page = 1;
            var pageSize = 10;

            _eventRepositoryMock.Setup(r => r.GetAllEventsWithPaginationAsync(page, pageSize))
                .ReturnsAsync((PaginationDto<Event>?)null);

            // Act
            var result = await _eventService.GetAllEventsWithPaginationAsync(page, pageSize, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorDataResult<PaginationDto<ViewEventDto>>>(result);
            Assert.Equal(Messages.EmptyEventList, result.Message);
        }

        [Fact]
        public async Task GetAllEventsWithPaginationAsync_Should_Return_Error_When_EventsWithPaginationData_Is_Null()
        {
            // Arrange
            var page = 1;
            var pageSize = 10;

            var paginationDto = new PaginationDto<Event>
            {
                Data = null,
                CurrentPage = page,
                TotalPages = 1,
                PageSize = pageSize,
                TotalCount = 0
            };

            _eventRepositoryMock.Setup(r => r.GetAllEventsWithPaginationAsync(page, pageSize))
                .ReturnsAsync(paginationDto);

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
            var page = 1;
            var pageSize = 5;

            var eventList = new List<Event>
    {
        new Event { Id = Guid.NewGuid(), EventName = "Test Event 1", StartDate = DateTimeOffset.UtcNow, EndDate = DateTimeOffset.UtcNow.AddHours(2), Location = "Location 1", OrganizerId = Guid.NewGuid(), Timezone = "UTC" },
        new Event { Id = Guid.NewGuid(), EventName = "Test Event 2", StartDate = DateTimeOffset.UtcNow.AddHours(3), EndDate = DateTimeOffset.UtcNow.AddHours(4), Location = "Location 2", OrganizerId = Guid.NewGuid(), Timezone = "UTC" }
    };

            var paginationDto = new PaginationDto<Event>
            {
                Data = eventList,
                CurrentPage = page,
                TotalPages = 1,
                PageSize = pageSize,
                TotalCount = 2
            };

            var mappedEventList = eventList.Select(e => new ViewEventDto
            {
                EventName = e.EventName,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Location = e.Location,
                Timezone = e.Timezone
            });

            _eventRepositoryMock.Setup(r => r.GetAllEventsWithPaginationAsync(page, pageSize))
                .ReturnsAsync(paginationDto);

            _mapperMock.Setup(m => m.Map<IEnumerable<ViewEventDto>>(eventList))
                .Returns(mappedEventList);

            // Act
            var result = await _eventService.GetAllEventsWithPaginationAsync(page, pageSize, CancellationToken.None);

            // Assert
            Assert.IsType<SuccessDataResult<PaginationDto<ViewEventDto>>>(result);
            Assert.Collection(result.Data.Data,
             item1 =>
             {
                 Assert.Equal("Test Event 1", item1.EventName);
                 Assert.Equal("Location 1", item1.Location);
             },
             item2 =>
             {
                 Assert.Equal("Test Event 2", item2.EventName);
                 Assert.Equal("Location 2", item2.Location);
             });
        }

        [Fact]
        public async Task GetEventWithParticipantsAsync_Should_Return_EventNotFound_When_Event_Does_Not_Exist()
        {
            // Arrange
            var eventId = Guid.NewGuid();

            _eventRepositoryMock.Setup(e => e.GetEventWithParticipantsAsync(eventId))
                .ReturnsAsync((Event)null);

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
            var eventId = Guid.NewGuid();
            var eventEntity = new Event
            {
                Id = eventId,
                EventName = "Test Event",
                Location = "Test location",
                EndDate = DateTimeOffset.UtcNow.AddHours(2),
                StartDate = DateTimeOffset.UtcNow,
                EventDescription = "A description for the test event",
                Timezone = "UTC",
                Participants = new List<Participant>()
            };
            _eventRepositoryMock.Setup(e => e.GetEventWithParticipantsAsync(eventId))
                .ReturnsAsync(eventEntity);
          
            // Act
            var result = await _eventService.GetEventWithParticipantsAsync(eventId, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorDataResult<ViewEventWithParticipantsDto>>(result);
            Assert.Equal(Messages.EmptyParticipantList, result.Message);

        }

        [Fact]
        public async Task GetEventWithParticipantsAsync_Should_Return_SuccessDataResult_When_All_Conditions_Are_Met()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var eventEntity = new Event
            {
                Id = eventId,
                EventName = "Test Event",
                Location = "Test location",
                EndDate = DateTimeOffset.UtcNow.AddHours(2),
                StartDate = DateTimeOffset.UtcNow,
                EventDescription = "A description for the test event",
                Timezone = "UTC",
                Participants = new List<Participant>
                {
                    new Participant
                    {
                        UserId = new Guid()
                    },

                }
            };

          
            _eventRepositoryMock.Setup(e => e.GetEventWithParticipantsAsync(eventId))
                .ReturnsAsync(eventEntity);
            _mapperMock.Setup(m => m.Map<ViewEventWithParticipantsDto>(eventEntity))
                .Returns(new ViewEventWithParticipantsDto{

                 
                    Id = eventId,
                    EventName = eventEntity.EventName,
                    EventDescription = eventEntity.EventDescription,
                    StartDate = eventEntity.StartDate,
                    EndDate = eventEntity.EndDate,
                    Timezone = eventEntity.Timezone,
                     Participants = eventEntity.Participants.Select(p => new ParticipantDto
                     { 
                         UserId = p.UserId
                         
                        
                     }).ToList()
                 
            });
            // Act
            var result = await _eventService.GetEventWithParticipantsAsync(eventId, CancellationToken.None);

            // Assert
            Assert.IsType<SuccessDataResult<ViewEventWithParticipantsDto>>(result);
            Assert.NotNull(result.Data);
            Assert.Equal(eventEntity.EventName, result.Data.EventName);
            Assert.Equal(eventEntity.Participants.Count, result.Data.Participants.Count);
        }
        #endregion
    }
}

