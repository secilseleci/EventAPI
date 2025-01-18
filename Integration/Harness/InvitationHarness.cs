namespace Integration.Harness
{
    internal static class InvitationHarness
    {
        // 1. Random Davetiye Oluşturma
        internal static async Task<Invitation> RegisterAndGetRandomInvitationAsync(
            this TestBase testBase,
            Guid? eventId = null,
            Guid? organizerId = null,
            Guid? receiverId = null,
            bool assertSuccess = true)
        {
            var invitationRepository = testBase.ApplicationFixture.Services.GetRequiredService<IInvitationRepository>();
            var eventRepository = testBase.ApplicationFixture.Services.GetRequiredService<IEventRepository>();
            var userRepository = testBase.ApplicationFixture.Services.GetRequiredService<IUserRepository>();

            // Varsayılan Event ve Kullanıcıları oluştur
            var randomEvent = eventId.HasValue
                ? await eventRepository.GetByIdAsync(eventId.Value)
                : await testBase.RegisterAndGetRandomEventAsync();

          

            var receiver = receiverId.HasValue
                ? await userRepository.GetByIdAsync(receiverId.Value)
                : await testBase.RegisterAndGetRandomUserAsync();

            var invitationToAdd = new Invitation
            {
                EventId = randomEvent.Id,
                OrganizerId = randomEvent.OrganizerId  ,
                ReceiverId = receiver.Id,
                IsAccepted = false,
                Message = "Test Invitation"
            };

            var registerResult = await invitationRepository.CreateAsync(invitationToAdd);
            AssertRegisterResult(assertSuccess, registerResult);

            return invitationToAdd;
        }





        private static void AssertRegisterResult(bool assertSuccess, int registerResult)
        {
            if (assertSuccess)
            {
                registerResult.Should().BeGreaterThan(0, "Invitation registration failed. Ensure the repository and database are correctly configured.");
            }
        }
    }
}
