using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seeders
{
    public static class ParticipantSeeder
    {
        public static ModelBuilder SeedParticipants(this ModelBuilder modelBuilder)
        {
            var participants = new List<Participant>
            {
                new Participant
                {
                    Id = Guid.NewGuid(),
                    EventId = Guid.Parse("81e4e565-7bea-4f4f-816a-def22c28f42f"), // Team Birthday Party
                    UserId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479")  // Seçil
                },
                new Participant
                {
                    Id = Guid.NewGuid(),
                    EventId = Guid.Parse("5dc9ba5e-53c0-4166-87de-5f6f57021256"), // Project Kickoff Meeting
                    UserId = Guid.Parse("d8a490c9-ef65-4c6b-9d0a-4d55f54307db")  // Hasan
                }
            };

            modelBuilder.Entity<Participant>().HasData(participants);

            return modelBuilder;
        }
    }
}
