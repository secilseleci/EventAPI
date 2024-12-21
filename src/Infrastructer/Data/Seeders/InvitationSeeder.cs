using Core.Entities;
using Core.Interfaces.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seeders
{
    public static class InvitationSeeder
    {
        public static ModelBuilder SeedInvitations(this ModelBuilder modelBuilder)
        {
            var invitations = new List<Invitation>
    {
        new Invitation
        {
            Id = Guid.Parse("2a5b59a3-d486-4b8b-b0e4-3fb27cf8b85b"),
            Message = "Join us for the birthday party!",
            IsAccepted = false,
            EventId = Guid.Parse("81e4e565-7bea-4f4f-816a-def22c28f42f"), // Event ID
            OrganizerId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"), // Seçil's User ID
            ReceiverId = Guid.Parse("d8a490c9-ef65-4c6b-9d0a-4d55f54307db")  // Hasan's User ID
        },
        new Invitation
        {
            Id = Guid.Parse("3b5c59a3-d486-4b8b-b0e4-3fb27cf8b85b"),
            Message = "Kickoff meeting invitation",
            IsAccepted = false,
            EventId = Guid.Parse("5dc9ba5e-53c0-4166-87de-5f6f57021256"), // Event ID
            OrganizerId = Guid.Parse("d8a490c9-ef65-4c6b-9d0a-4d55f54307db"), // Hasan's User ID
            ReceiverId = Guid.Parse("b12f1fc3-a9e7-4b53-90a7-0b2e1e7d3a12")  // Eda's User ID
        }
    };

            modelBuilder.Entity<Invitation>().HasData(invitations);

            return modelBuilder;
        }

    }
}

