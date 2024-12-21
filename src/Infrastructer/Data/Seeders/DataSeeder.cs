using Microsoft.EntityFrameworkCore;
  

namespace Infrastructure.Data.Seeders { 

public static class DataSeeder
{
    public static ModelBuilder Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder
             .SeedEvents()
             .SeedInvitations();

        return modelBuilder;
    }
}
}