 namespace Integration.Harness;
//{
//    internal static  class EventHArness
//    {
//        internal static async Task<Event> RegisterAndGetRandomEventAsync(this TestBase testBase, bool assertSuccess = true) 
//        { 
//            var eventRepository = testBase.ApplicationFixture.Services.GetRequiredService<IEventRepository>();
//            var eventCounter = Guid.NewGuid().ToString()[..5];

//            var eventToAdd = new Event ()
//            {
//                EventName = $"Event {eventCounter}",
//                EventDescription = $"Event Description {eventCounter}"
//            };

//            ////var registerResult = await eventRepository.CreateAsync(eventToAdd);
//            AssertRegisterResult(assertSuccess, registerResult);

//            return eventToAdd;
//    }
//        private static void AssertRegisterResult(bool assertSuccess, int registerResult)
//        {
//            if (assertSuccess)
//                registerResult.Should().BeGreaterThan(0);
//        }
//        public static ViewEventDto ConvertEventToViewEventDto(this TestBase testBase, Event event)
//        {
//            return new ViewEventDto()
//            {
//                Name = event.EventName,
//                Description = event.EventDescription
//            };
//        }
//    } }
