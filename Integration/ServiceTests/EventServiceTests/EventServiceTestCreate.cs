namespace Integration.ServiceTests.EventServiceTestsCreate
{
    public class EventServiceTestsCreate : TestBase
    {
         public EventServiceTestsCreate(ApplicationFixture applicationFixture) : base(applicationFixture)
        {}
          
        
        #region CreateEvent
        [Fact]
        public async Task CreateEventAsync_Should_Return_UserNotFound_When_User_Is_Invalid()
        {
            // Arrange
            var invalidUserId = Guid.NewGuid();
          
            // Act
            var result = await EventService.CreateEventAsync(null, invalidUserId, CancellationToken.None);

            // Assert
            Assert.IsType<ErrorResult>(result);
            Assert.Equal(Messages.UserNotFound, result.Message);

        }

       
        [Fact]
        public async Task CreateEventAsync_Should_Return_SuccessResult_When_All_Conditions_Are_Met()
        {
            // Arrange
            var validUser = await this.RegisterAndGetRandomUserAsync();
            var createEventDto = this.CreateRandomEventDto();

            // Act
            var result = await EventService.CreateEventAsync(createEventDto, validUser.Id, CancellationToken.None);

            // Assert
            Assert.IsType<SuccessResult>(result);
            Assert.Equal(Messages.CreateEventSuccess, result.Message);
 
        }

        #endregion


    }
}

