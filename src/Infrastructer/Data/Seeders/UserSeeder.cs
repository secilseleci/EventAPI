using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seeders;

public static class UserSeeder
{
    public static ModelBuilder SeedUsers(this ModelBuilder modelBuilder)
    {
        var users = GetUsers();

        foreach (var user in users)
        {
            modelBuilder.Entity<User>()
                .HasData(user);
        }

        return modelBuilder;
    }

    private static List<User> GetUsers()
    {
        return new List<User>
        {
            new User
            {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                FullName ="Seçil Seleci"
,                UserName = "secilSeleci",
                Email = "secil@example.com"
            },
            new User
            {
                Id = Guid.Parse("d8a490c9-ef65-4c6b-9d0a-4d55f54307db"),
                UserName = "hasanYuksel",
                FullName ="Hasan Yüksel",
                Email = "hasan@example.com"
            },
             new User
            {
                Id = Guid.Parse("b12f1fc3-a9e7-4b53-90a7-0b2e1e7d3a12"),
                UserName = "edaMayali",
                FullName ="Eda Mayalı",
                Email = "eda@example.com"
            },
              new User
            {
                Id = Guid.Parse("9c24e2f7-52b1-4f78-8dce-3ae146b7f9d5"),
                UserName = "gokhanBilir",
                FullName ="Gökhan Bilir",
                Email = "gokhan@example.com"
            }
        };
    }
}
