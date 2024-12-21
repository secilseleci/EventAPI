using Core.Entities;
using Infrastructure.Data.Seeders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class EventApiDbContext(DbContextOptions<EventApiDbContext> context) : DbContext(context)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedUsers();
            modelBuilder.SeedEvents();
           
            modelBuilder.SeedParticipants();
            modelBuilder.SeedInvitations();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
            .HasMany(user => user.OrganizedEvents)
            .WithOne(evt => evt.Organizer)
            .HasForeignKey(evt => evt.OrganizerId)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Invitation>()
           .HasOne(i => i.Event)
           .WithMany(e => e.Invitations)
           .HasForeignKey(i => i.EventId)
           .OnDelete(DeleteBehavior.Cascade);

 

            modelBuilder.Entity<Participant>()
             .HasOne(p => p.Event)
             .WithMany(e => e.Participants)
             .HasForeignKey(p => p.EventId)
             .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<Invitation>()
              .HasOne(i => i.Organizer)
              .WithMany(u => u.SentInvitations)
              .HasForeignKey(i => i.OrganizerId)
              .OnDelete(DeleteBehavior.NoAction);



        }

        public DbSet<User> Users { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Participant> Participants { get; set; }

    }
}
 
