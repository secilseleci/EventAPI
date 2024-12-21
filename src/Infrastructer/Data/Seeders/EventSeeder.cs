using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seeders
{
    public static class EventSeeder
    {
        public static ModelBuilder SeedEvents(this ModelBuilder modelBuilder)
        {
            var events = new List<Event>
            {
                new Event
                {
                    Id = Guid.Parse("81e4e565-7bea-4f4f-816a-def22c28f42f"),
                    EventName = "Team Birthday Party",
                    EventDescription = "Celebrate our team leader's 40th birthday!",
                    StartDate = new DateTimeOffset(2025, 2, 1, 8, 0, 0, TimeSpan.Zero),
                    EndDate = new DateTimeOffset(2025, 2, 1, 10, 0, 0, TimeSpan.Zero),
                    Timezone = "UTC",
                    Location = "Istanbul",
                    OrganizerId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479")  // Seçil'in ID'si
                },
                new Event
                {
                    Id = Guid.Parse("5dc9ba5e-53c0-4166-87de-5f6f57021256"),
                    EventName = "Project Kickoff Meeting",
                    EventDescription = "Kickoff meeting for the new project.",
                    StartDate = new DateTimeOffset(2025, 2, 1, 8, 0, 0, TimeSpan.Zero),
                    EndDate = new DateTimeOffset(2025, 2, 1, 10, 0, 0, TimeSpan.Zero),
                    Timezone = "UTC",
                    Location = "Ankara",
                    OrganizerId = Guid.Parse("d8a490c9-ef65-4c6b-9d0a-4d55f54307db")  // Hasan Yüksel'in ID'si
                }
            };

            modelBuilder.Entity<Event>().HasData(events);

            return modelBuilder;
        }
    }
}
