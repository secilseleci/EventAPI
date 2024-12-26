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
            Message = "Sen de davetlisin Hasan!",
            IsAccepted = true,
            EventId = Guid.Parse("81e4e565-7bea-4f4f-816a-def22c28f42f"), // Event ID
            OrganizerId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"), // Seçil's User ID
            ReceiverId = Guid.Parse("d8a490c9-ef65-4c6b-9d0a-4d55f54307db")  // Hasan's User ID
        },
         new Invitation
        {
            Id = Guid.Parse("b95c33b8-0b68-4a5c-8255-8c4a48224862"),
            Message = "Sen de davetlisin Eda!",
            IsAccepted = true,
            EventId = Guid.Parse("81e4e565-7bea-4f4f-816a-def22c28f42f"), // Event ID
            OrganizerId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"), // Seçil's User ID
            ReceiverId = Guid.Parse("b12f1fc3-a9e7-4b53-90a7-0b2e1e7d3a12")  // eda's User ID
        },
          new Invitation
        {
            Id = Guid.Parse("f20d308d-fdd5-4b1b-b71d-b0a0c26c1280"),
            Message = "Sen de davetlisin Gokhan!",
            IsAccepted = true,
            EventId = Guid.Parse("81e4e565-7bea-4f4f-816a-def22c28f42f"), // Event ID
            OrganizerId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"), // Seçil's User ID
            ReceiverId = Guid.Parse("9c24e2f7-52b1-4f78-8dce-3ae146b7f9d5")  // Gokhan's User ID
        } ,
          new Invitation
        {
            Id = Guid.Parse("cfcf8770-728d-482c-90d8-fd40cba5551c"),
            Message = "Sen de davetlisin Gokhan!",
            IsAccepted = true,
            EventId = Guid.Parse("5d7e2f23-49d2-4e7e-9517-3a14c67e36a9"), // Event ID
            OrganizerId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"), // Seçil's User ID
            ReceiverId = Guid.Parse("9c24e2f7-52b1-4f78-8dce-3ae146b7f9d5")  // Gokhan's User ID
        } 

    };

            modelBuilder.Entity<Invitation>().HasData(invitations);

            return modelBuilder;
        }

    }
}

