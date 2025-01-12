 namespace Integration.Harness
{
internal static class EventHarness
{
        internal static async Task<Event> RegisterAndGetRandomEventAsync(this TestBase testBase, bool assertSuccess = true)
        {
            var eventRepository=testBase.ApplicationFixture.Services.GetService<IEventRepository>();
            var userRepository = testBase.ApplicationFixture.Services.GetService<IUserRepository>();
            
            var organizer = await testBase.RegisterAndGetRandomUserAsync();
            var eventCounter = Guid.NewGuid().ToString()[..5];

                var eventToAdd = new Event()
                {
                    EventName = $"Event{eventCounter}",
                    EventDescription = $"Event Description {eventCounter}",
                    Location = $"Test location {eventCounter}",
                    EndDate = DateTimeOffset.UtcNow.AddHours(2),
                    StartDate = DateTimeOffset.UtcNow,
                    Timezone = "UTC",
                    OrganizerId = organizer.Id,
                };

                var registerResult = await eventRepository.CreateAsync(eventToAdd);
                AssertRegisterResult(assertSuccess, registerResult);
                return eventToAdd;
        }
        private static void AssertRegisterResult(bool assertSuccess, int registerResult)
        {
            if (assertSuccess)
                registerResult.Should().BeGreaterThan(0);
        }
        internal static CreateEventDto CreateRandomEventDto(this TestBase testBase)
        {
            var eventCounter = Guid.NewGuid().ToString()[..5];

            return new CreateEventDto
            {
                EventName = $"Event{eventCounter}",
                EventDescription = $"Description for Event{eventCounter}",
                Location = $"Test Location {eventCounter}",
                StartDate = DateTimeOffset.UtcNow,
                EndDate = DateTimeOffset.UtcNow.AddHours(2),
                Timezone = "UTC"
            };
        }
        public static ViewEventDto ConvertEventToViewEventDto(this TestBase testBase, Event eventEntity)
    {
        return new ViewEventDto
        {
            EventName = eventEntity.EventName,
            Location = eventEntity.Location,
            StartDate = eventEntity.StartDate,
            EndDate = eventEntity.EndDate,
            Timezone = eventEntity.Timezone,
        };
    }
        internal static UpdateEventDto CreateRandomUpdateEventDto(this TestBase testBase, Event eventEntity)
        {
            return new UpdateEventDto
            {
                Id = eventEntity.Id,
                EventName = $"Updated {eventEntity.EventName}",
                EventDescription = $"Updated {eventEntity.EventDescription ?? "Description"}",
                StartDate = eventEntity.StartDate.AddHours(1), // Example of a change
                EndDate = eventEntity.EndDate.AddHours(1), // Example of a change
                Location = $"Updated {eventEntity.Location}",
                Timezone = eventEntity.Timezone
            };
        }


        public static UpdateEventDto ConvertEventToUpdateEventDto(this TestBase testBase, Event eventEntity)
        {
            return new UpdateEventDto
            {Id= eventEntity.Id,
                EventName = eventEntity.EventName,
                Location = eventEntity.Location,
                StartDate = eventEntity.StartDate,
                EndDate = eventEntity.EndDate,
                Timezone = eventEntity.Timezone,
            };
        }





    }
}
