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
                    EventName = "Tanışma Toplantısı",
                    EventDescription = "Aramıza katılan yeni arkadaşlar, hoş geldiniz!",
                    StartDate = new DateTimeOffset(2025, 2, 1, 8, 0, 0, TimeSpan.Zero),
                    EndDate = new DateTimeOffset(2025, 2, 1, 10, 0, 0, TimeSpan.Zero),
                    Timezone = "UTC",
                    Location = "Ofis",
                    OrganizerId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479")  
                } ,
                new Event
                {
                    Id = Guid.Parse("5d7e2f23-49d2-4e7e-9517-3a14c67e36a9"),
                    EventName = "Proje Planlama Toplantısı",
                    EventDescription = "Yeni projeler için planlama ve görev dağılımı yapılacaktır.",
                    StartDate = new DateTimeOffset(2025, 2, 8, 14, 0, 0, TimeSpan.Zero),
                    EndDate = new DateTimeOffset(2025, 2, 8, 16, 0, 0, TimeSpan.Zero),
                    Timezone = "UTC",
                    Location = "Ofis",
                    OrganizerId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479")   
                }

            };

            modelBuilder.Entity<Event>().HasData(events);

            return modelBuilder;
        }
    }
}
