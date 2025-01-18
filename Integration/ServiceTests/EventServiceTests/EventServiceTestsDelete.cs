

namespace Integration.ServiceTests.EventServiceTestsDelete
{
    public class EventServiceTestsDelete : TestBase
    { 
        public EventServiceTestsDelete(ApplicationFixture applicationFixture) : base(applicationFixture)
        {}
            
        

        #region DeleteEvent
        [Fact]
        public async Task DeleteEventAsync_Should_Return_EventNotFound_When_Event_Does_Not_Exist()
        {
            //Arrange
            var validUser = await this.RegisterAndGetRandomUserAsync();  
            var nonExistentEventId = Guid.NewGuid();  
             
            //Act
            var result=await EventService.DeleteEventAsync(nonExistentEventId, validUser.Id, CancellationToken.None);
            //Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.EventNotFound, result.Message);
        }

        [Fact]
        public async Task DeleteEventAsync_Should_Return_UnauthorizedAccess_When_User_Is_Not_Organizer()
        {
            // Arrange
            var validUser = await this.RegisterAndGetRandomUserAsync();  
            var randomEvent = await this.RegisterAndGetRandomEventAsync();  
 
            // Act
            var result = await EventService.DeleteEventAsync(randomEvent.Id, validUser.Id, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.UnauthorizedAccess, result.Message);
        }


        [Fact]
        public async Task DeleteEventAsync_Should_Return_SuccessResult_When_All_Conditions_Are_Met()
        {
            // Arrange
             var randomEvent = await this.RegisterAndGetRandomEventAsync();
             
            // Act
            var result = await EventService.DeleteEventAsync(randomEvent.Id, randomEvent.OrganizerId, CancellationToken.None);

            // Assert
            Assert.IsType<SuccessResult>(result);
            Assert.Equal(Messages.DeleteEventSuccess, result.Message);
 
        }

        #endregion

    }
}

