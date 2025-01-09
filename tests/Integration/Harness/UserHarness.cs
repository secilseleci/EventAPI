using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Harness
{
    internal static class UserHarness
    {
        internal static async Task<User> RegisterAndGetRandomUserAsync(this TestBase testBase, User? customUser = null, bool assertSuccess = true)
        {
            var userRepository = testBase.ApplicationFixture.Services.GetRequiredService<IUserRepository>();
            var userCounter = Guid.NewGuid().ToString("N"); // Benzersizliği artırmak için "N" formatı

            var userToAdd = customUser ?? new User
            {
                FullName = $"Fullname {userCounter}",
                UserName = $"Username{userCounter}",
                Password = $"Password{userCounter}",
                Email = $"user{userCounter}@example.com"
            };

            try
            {
                Console.WriteLine($"Attempting to create user: {userToAdd.UserName}, {userToAdd.Email}");
                var registerResult = await userRepository.CreateAsync(userToAdd);
                Console.WriteLine($"CreateAsync result: {registerResult}");
                AssertRegisterResult(assertSuccess, registerResult);
                return userToAdd;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while creating user: {ex.Message}");
                throw;
            }
        }
        private static void AssertRegisterResult(bool assertSuccess, int registerResult)
        {
            if (assertSuccess)
            {
                registerResult.Should().BeGreaterThan(0, "User registration failed. Ensure the repository and database are correctly configured.");
            }
        }
    }
}
