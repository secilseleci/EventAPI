using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class EventApiDbContext(DbContextOptions<EventApiDbContext> context) : DbContext(context)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

         
        modelBuilder.Entity<User>()
            .HasMany(user => user.OrganizedEvents)
            .WithOne(evt => evt.Organizer)
            .HasForeignKey(evt => evt.OrganizerId);

        modelBuilder.Entity<Participant>()
            .HasKey(p => new { p.Id, p.EventId });

        modelBuilder.Entity<Participant>()
            .HasOne(p => p.User)
            .WithMany(user => user.ParticipatedEvents)
            .HasForeignKey(p => p.Id)
            .OnDelete(DeleteBehavior.Restrict);
        

            modelBuilder.Entity<Participant>()
            .HasOne(p => p.Event)
            .WithMany(evt => evt.Participants)
            .HasForeignKey(p => p.EventId);

        modelBuilder.Entity<Invitation>()
            .HasOne(i => i.Organizer)
            .WithMany(u => u.SentInvitations)
            .HasForeignKey(i => i.OrganizerId)
            .OnDelete(DeleteBehavior.Restrict);  
            ;

            modelBuilder.Entity<Invitation>()
            .HasOne(i => i.Receiver)
            .WithMany(u => u.ReceivedInvitations)
            .HasForeignKey(i => i.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);   
            ;
        } 
        
        public virtual DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Participant> Participants { get; set; }

    }
}
 
