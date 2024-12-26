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
                    EventId = Guid.Parse("81e4e565-7bea-4f4f-816a-def22c28f42f"),  
                    UserId = Guid.Parse("b12f1fc3-a9e7-4b53-90a7-0b2e1e7d3a12")    
                },
                 new Participant
                {
                    Id = Guid.NewGuid(),
                    EventId = Guid.Parse("81e4e565-7bea-4f4f-816a-def22c28f42f"),  
                    UserId = Guid.Parse("9c24e2f7-52b1-4f78-8dce-3ae146b7f9d5")   
                },
                    new Participant
                {
                    Id = Guid.NewGuid(),
                    EventId = Guid.Parse("81e4e565-7bea-4f4f-816a-def22c28f42f"),
                    UserId = Guid.Parse("d8a490c9-ef65-4c6b-9d0a-4d55f54307db")
                },
                    new Participant
                {
                    Id = Guid.NewGuid(),
                    EventId = Guid.Parse("5d7e2f23-49d2-4e7e-9517-3a14c67e36a9"),
                    UserId = Guid.Parse("9c24e2f7-52b1-4f78-8dce-3ae146b7f9d5")
                }

            };

            modelBuilder.Entity<Participant>().HasData(participants);

            return modelBuilder;
        }
    }
}
