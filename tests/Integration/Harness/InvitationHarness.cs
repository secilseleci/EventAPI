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

            var organizer = organizerId.HasValue
                ? await userRepository.GetByIdAsync(organizerId.Value)
                : await testBase.RegisterAndGetRandomUserAsync();

            var receiver = receiverId.HasValue
                ? await userRepository.GetByIdAsync(receiverId.Value)
                : await testBase.RegisterAndGetRandomUserAsync();

            var invitationToAdd = new Invitation
            {
                EventId = randomEvent.Id,
                OrganizerId = organizer.Id,
                ReceiverId = receiver.Id,
                IsAccepted = false,
                Message = "Test Invitation"
            };

            var registerResult = await invitationRepository.CreateAsync(invitationToAdd);
            AssertRegisterResult(assertSuccess, registerResult);

            return invitationToAdd;
        }

        // 2. Davetiye DTO'ya Dönüştürme
        public static InvitationDto ConvertInvitationToInvitationDto(this TestBase testBase, Invitation invitation)
        {
            return new InvitationDto
            {
                InvitationId = invitation.Id,
                Message = invitation.Message,
                IsAccepted = invitation.IsAccepted,
                EventName = invitation.Event?.EventName ?? string.Empty,
                EventStartDate = invitation.Event?.StartDate ?? DateTimeOffset.MinValue,
                EventEndDate = invitation.Event?.EndDate ?? DateTimeOffset.MinValue,
                Timezone = invitation.Event?.Timezone ?? string.Empty,
                Organizer = invitation.Organizer?.FullName ?? string.Empty,
                Receiver = invitation.Receiver?.FullName ?? string.Empty
            };
        }


        // 3. Davetiye Kabul Etme
        internal static async Task<IResult> ParticipateInInvitationAsync(this TestBase testBase, Invitation invitation, bool assertSuccess = true)
        {
            var invitationRepository = testBase.ApplicationFixture.Services.GetRequiredService<IInvitationRepository>();

            // Davetiye kabul etme
            invitation.IsAccepted = true;
            var updateResult = await invitationRepository.UpdateAsync(invitation);

            if (assertSuccess)
            {
                updateResult.Should().BeGreaterThan(0, "Invitation participation failed. Ensure the repository and database are correctly configured.");
            }

            return updateResult > 0
                ? new SuccessResult(Messages.InvitationAcceptedSuccessfully)
                : new ErrorResult(Messages.UpdateInvitationError);
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
