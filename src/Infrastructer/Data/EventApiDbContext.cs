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

            // User -> Event (Cascade delete: User silinince Event'ler de silinir)
            modelBuilder.Entity<User>()
                .HasMany(user => user.OrganizedEvents)
                .WithOne(evt => evt.Organizer)
                .HasForeignKey(evt => evt.OrganizerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(user => user.SentInvitations)
                .WithOne(inv => inv.Organizer)
                .HasForeignKey(inv => inv.OrganizerId)
                .OnDelete(DeleteBehavior.NoAction);


            // User -> Participant (Cascade delete: User silinince Participant kayıtları da silinir)
            modelBuilder.Entity<User>()
                .HasMany(user => user.ParticipatedEvents)
                .WithOne(part => part.User)
                .HasForeignKey(part => part.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            // Event -> Invitation (Cascade delete: Event silinince Invitation'lar da silinir)
            modelBuilder.Entity<Event>()
                .HasMany(evt => evt.Invitations)
                .WithOne(inv => inv.Event)
                .HasForeignKey(inv => inv.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Event -> Participant (Cascade delete: Event silinince Participant kayıtları da silinir)
            modelBuilder.Entity<Event>()
                .HasMany(evt => evt.Participants)
                .WithOne(part => part.Event)
                .HasForeignKey(part => part.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Invitation -> User (Receiver olarak, User silinirse Invitation silinir)
            modelBuilder.Entity<Invitation>()
                .HasOne(inv => inv.Receiver)
                .WithMany(user => user.ReceivedInvitations)
                .HasForeignKey(inv => inv.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);


        }

        public DbSet<User> Users { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Participant> Participants { get; set; }

    }
}
 
