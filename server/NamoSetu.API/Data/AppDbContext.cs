using Microsoft.EntityFrameworkCore;
using NamoSetu.API.Models;

namespace NamoSetu.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Temple> Temples => Set<Temple>();
    public DbSet<Itinerary> Itineraries => Set<Itinerary>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Accommodation> Accommodations => Set<Accommodation>();
    public DbSet<PujaService> PujaServices => Set<PujaService>();
    public DbSet<PujaBooking> PujaBookings => Set<PujaBooking>();
    public DbSet<GroupYatra> GroupYatras => Set<GroupYatra>();
    public DbSet<GroupYatraMember> GroupYatraMembers => Set<GroupYatraMember>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Unique email
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Temple rating precision
        modelBuilder.Entity<Temple>()
            .Property(t => t.Rating)
            .HasPrecision(3, 2);

        // Accommodation rating precision
        modelBuilder.Entity<Accommodation>()
            .Property(a => a.Rating)
            .HasPrecision(3, 2);
    }
}