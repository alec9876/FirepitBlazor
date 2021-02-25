using System;
using FirepitAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FirepitAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<Person>
    {
        public DbSet<Beverages> Beverages { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<UserMeeting> UserMeetings { get; set; }
        public DbSet<Quotes> Quotes { get; set; }
        public DbSet<ChatHistory> ChatHistory { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserMeeting>()
            .HasKey(t => new { t.MeetingId, t.PersonId });

            modelBuilder.Entity<UserMeeting>()
                .HasOne(b => b.Meeting)
                .WithMany(b => b.UserMeetings);

            modelBuilder.Entity<UserMeeting>()
                .HasOne(b => b.Person)
                .WithMany(b => b.UserMeetings)
                .HasForeignKey(b => b.PersonId);

            modelBuilder.Entity<Quotes>()
                    .HasOne(m => m.WhoSaidItKey)
                    .WithMany(t => t.WhoSaidItQuotes)
                    .HasForeignKey(m => m.WhoSaidIt)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Quotes>()
                        .HasOne(m => m.PersonIdKey)
                        .WithMany(t => t.PersonQuotes)
                        .HasForeignKey(m => m.PersonId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChatHistory>()
                        .HasOne(m => m.FromUser)
                        .WithMany(t => t.ChatHistoryFromUser)
                        .HasForeignKey(m => m.FromUserId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChatHistory>()
                        .HasOne(m => m.ToUser)
                        .WithMany(t => t.ChatHistoryToUser)
                        .HasForeignKey(m => m.ToUserId)
                        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
