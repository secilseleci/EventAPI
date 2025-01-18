using System.Threading;

namespace Integration.ServiceTests.InvitationServiceTests
{
    public class InvitationServiceTest: TestBase
    {
        
        public InvitationServiceTest(ApplicationFixture applicationFixture) : base(applicationFixture)
        {
        }

        [Fact]
        public async Task GetReceivedInvitationsAsync_Should_Return_UserNotFound_When_User_Does_Not_Exist()
        {
            // Arrange
            var invalidUserId = Guid.NewGuid();

            // Act
            var result = await InvitationService.GetReceivedInvitationsAsync(invalidUserId, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorDataResult<IEnumerable<InvitationDto>>>(result);
            Assert.Equal(Messages.UserNotFound, result.Message);
        }

        [Fact]
        public async Task GetReceivedInvitationsAsync_Should_Return_EmptyInvitationList_When_User_Has_No_Invitations()
        {
            // Arrange
            var validUser = await this.RegisterAndGetRandomUserAsync();

            // Act
            var result = await InvitationService.GetReceivedInvitationsAsync(validUser.Id, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorDataResult<IEnumerable<InvitationDto>>>(result);
            Assert.Equal(Messages.EmptyInvitationList, result.Message);
        }

        [Fact]
        public async Task GetReceivedInvitationsAsync_Should_Return_Invitations_When_User_Has_Invitations()
        {
            // Arrange
            var randomInvitation = await this.RegisterAndGetRandomInvitationAsync();

            // Act
            var result = await InvitationService.GetReceivedInvitationsAsync(randomInvitation.ReceiverId, CancellationToken.None);

            // Assert
            Assert.IsType<SuccessDataResult<IEnumerable<InvitationDto>>>(result);
            Assert.NotNull(result.Data);
            Assert.NotEmpty(result.Data);
        }


        [Fact]
        public async Task GetSentInvitationsAsync_Should_Return_UserNotFound_When_User_Does_Not_Exist()
        {
                     
            // Arrange
            var invalidUserId = Guid.NewGuid();

            // Act
            var result = await InvitationService.GetSentInvitationsAsync(invalidUserId, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorDataResult<IEnumerable<InvitationDto>>>(result);
            Assert.Equal(Messages.UserNotFound, result.Message);
        
        }
        [Fact]
        public async Task GetSentInvitationsAsync_Should_Return_EmptyInvitationList_When_User_Has_No_Sent_Invitations()
        {
            // Arrange
            var validUser = await this.RegisterAndGetRandomUserAsync();

            // Act
            var result = await InvitationService.GetSentInvitationsAsync(validUser.Id, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorDataResult<IEnumerable<InvitationDto>>>(result);
            Assert.Equal(Messages.EmptyInvitationList, result.Message);
        }

        [Fact]
        public async Task GetSentInvitationsAsync_Should_Return_SentInvitations_When_User_Has_Sent_Invitations()
        {
            // Arrange
            var randomInvitation = await this.RegisterAndGetRandomInvitationAsync();

            // Act
            var result = await InvitationService.GetSentInvitationsAsync(randomInvitation.OrganizerId, CancellationToken.None);

            // Assert
            Assert.IsType<SuccessDataResult<IEnumerable<InvitationDto>>>(result);
            Assert.NotNull(result.Data);  
            Assert.NotEmpty(result.Data);
        }


        [Fact]
        public async Task GetSingleInvitationAsync_Should_Return_UserNotFound_When_User_Does_Not_Exist()
        {
            // Arrange
            var invalidUserId = Guid.NewGuid();
            var validEventId = Guid.NewGuid();

            // Act
            var result = await InvitationService.GetSingleInvitationAsync(invalidUserId, validEventId, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorDataResult<InvitationDto?>>(result);
            Assert.Equal(Messages.UserNotFound, result.Message);
        }
        [Fact]
        public async Task GetSingleInvitationAsync_Should_Return_InvitationNotFound_When_Invitation_Does_Not_Exist()
        {
            // Arrange
            var validUser = await this.RegisterAndGetRandomUserAsync();  
            var invalidEventId = Guid.NewGuid();  

            // Act
            var result = await InvitationService.GetSingleInvitationAsync(validUser.Id, invalidEventId, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorDataResult<InvitationDto?>>(result);
            Assert.Equal(Messages.InvitationNotFound, result.Message);
        }
        [Fact]
        public async Task GetSingleInvitationAsync_Should_Return_Success_When_Invitation_Exists()
        {
            // Arrange
            var validInvitation = await this.RegisterAndGetRandomInvitationAsync();  

            // Act
            var result = await InvitationService.GetSingleInvitationAsync(validInvitation.ReceiverId, validInvitation.EventId, CancellationToken.None);

            // Assert
            Assert.IsType<SuccessDataResult<InvitationDto?>>(result);
            Assert.NotNull(result.Data);  
        }


        [Fact]
        public async Task SendInvitationAsync_Should_Return_UserNotFound_When_Organizer_Does_Not_Exist()
        {
            //Arrange
            var invalidUserId = Guid.NewGuid();
            var randomEvent = await this.RegisterAndGetRandomEventAsync();  
            
            //Act
            var result = await InvitationService.SendInvitationAsync(invalidUserId, randomEvent.Id, null, CancellationToken.None);

            //Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.UserNotFound, result.Message);

        }

        [Fact]
        public async Task SendInvitationAsync_Should_Return_EventNotFound_When_Event_Does_Not_Exist()
        {
            //Arrange
            var validUser  = await this.RegisterAndGetRandomUserAsync();
            var randomEvent = Guid.NewGuid();

            //Act
            var result = await InvitationService.SendInvitationAsync(validUser.Id, randomEvent , null, CancellationToken.None);

            //Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.EventNotFound, result.Message);

        }

        [Fact]
        public async Task SendInvitationAsync_Should_Return_UnauthorizedAccess_When_Organizer_Is_Not_The_Event_Organizer()
        {
            //Arrange
            var unauthorizedUser = await this.RegisterAndGetRandomUserAsync();
            var randomEvent = await this.RegisterAndGetRandomEventAsync();

            //Act
            var result = await InvitationService.SendInvitationAsync(unauthorizedUser.Id, randomEvent.Id, null, CancellationToken.None);

            //Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.UnauthorizedAccess, result.Message);

        }
     
        [Fact]
        public async Task SendInvitationAsync_Should_Return_UserNotFound_When_No_Users_Are_Valid()
        {
            //Arrange
            var randomEvent = await this.RegisterAndGetRandomEventAsync();
             
            //Act
            var result = await InvitationService.SendInvitationAsync(randomEvent.OrganizerId, randomEvent.Id, new List<Guid>(), CancellationToken.None);

            //Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.UserNotFound, result.Message);

        }

        [Fact]
        public async Task SendInvitationAsync_Should_Return_AllUsersAlreadyInvited_When_All_Users_Are_Already_Invited()
        {
            //Arrange
            var randomInvitation=await this.RegisterAndGetRandomInvitationAsync();
            List<Guid> validUserIds = new List<Guid> { randomInvitation.ReceiverId };
              
            //Act
            var result = await InvitationService.SendInvitationAsync(randomInvitation.OrganizerId, randomInvitation.EventId, validUserIds, CancellationToken.None);

            //Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.AllUsersAlreadyInvited, result.Message);

        }

        [Fact]
        public async Task SendInvitationAsync_Should_Return_Success_When_New_Invitations_Are_Sent()
        {
            //Arrange
            var randomInvitation = await this.RegisterAndGetRandomInvitationAsync();
            var randomUser = await this.RegisterAndGetRandomUserAsync();
            List<Guid> validUserIds = new List<Guid> { randomUser.Id };

            //Act
            var result = await InvitationService.SendInvitationAsync(randomInvitation.OrganizerId, randomInvitation.EventId, validUserIds, CancellationToken.None);

            //Assert
            Assert.IsType<SuccessResult>(result);
            Assert.Equal(Messages.InvitationsSentSuccessfully, result.Message);

        }


        [Fact]
        public async Task ParticipateInvitationAsync_Should_Return_UserNotFound_When_User_Is_Not_Valid()
        {
            //Arrange
            var randomInvitation = await this.RegisterAndGetRandomInvitationAsync();
            var invalidUserId = Guid.NewGuid();
            //Act
            var result = await InvitationService.ParticipateInvitationAsync(invalidUserId, randomInvitation.EventId, CancellationToken.None);

            //Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.UserNotFound, result.Message);
        }

        [Fact]
        public async Task ParticipateInvitationAsync_Should_Return_InvitationNotFound_When_Invitation_Does_Not_Exist()
        {
            //Arrange
            var randomEvent = await this.RegisterAndGetRandomEventAsync();
            var validUser  = await this.RegisterAndGetRandomUserAsync(); ;
            //Act
            var result = await InvitationService.ParticipateInvitationAsync(validUser.Id, randomEvent.Id, CancellationToken.None);

            //Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.InvitationNotFound, result.Message);
        }

        [Fact]
        public async Task ParticipateInvitationAsync_Should_Return_SuccessResult_When_Invitation_Is_Updated_Successfully()
        {
            //Arrange
            var randomInvitation = await this.RegisterAndGetRandomInvitationAsync();
             //Act
            var result = await InvitationService.ParticipateInvitationAsync(randomInvitation.ReceiverId, randomInvitation.EventId, CancellationToken.None);

            //Assert
            Assert.IsType<SuccessResult>(result);
            Assert.Equal(Messages.InvitationAcceptedSuccessfully, result.Message);
        }
    }
}

 
