using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            // Configure Property entity
            modelBuilder.Entity<Property>(entity =>
            {
                entity.ToTable("Property");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("uuid")
                    .HasDefaultValueSql("uuid_generate_v4()")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Type).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.Address).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Area).IsRequired();
                entity.Property(e => e.ConstructionYear).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired();

                // Configure relationship with User
                entity.HasOne(p => p.User)
                      .WithMany(u => u.Properties)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("uuid")
                    .HasDefaultValueSql("uuid_generate_v4()")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Role).IsRequired();
                entity.Property(e => e.Address).IsRequired().HasMaxLength(200);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Nationality).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.Company).HasMaxLength(100);
                entity.Property(e => e.Type).HasMaxLength(100);
            });

            // Configure Payment entity
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("uuid")
                    .HasDefaultValueSql("uuid_generate_v4()")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.PropertyId).IsRequired();
                entity.Property(e => e.BuyerId).IsRequired();
                entity.Property(e => e.SellerId).IsRequired();
                entity.Property(e => e.Type).IsRequired();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.PaymentMethod).IsRequired();

                // Configure relationships
                entity.HasOne(p => p.Property)
                      .WithMany()
                      .HasForeignKey(p => p.PropertyId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Buyer)
                      .WithMany(u => u.Payments)
                      .HasForeignKey(p => p.BuyerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Seller)
                      .WithMany()
                      .HasForeignKey(p => p.SellerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Inquiry entity
            modelBuilder.Entity<Inquiry>(entity =>
            {
                entity.ToTable("Inquiry");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("uuid")
                    .HasDefaultValueSql("uuid_generate_v4()")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.PropertyId).IsRequired();
                entity.Property(e => e.ClientId).IsRequired();
                entity.Property(e => e.AgentId).IsRequired();
                entity.Property(e => e.Message).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();

                // Configure relationships
                entity.HasOne(i => i.Property)
                      .WithMany()
                      .HasForeignKey(i => i.PropertyId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(i => i.Client)
                      .WithMany(u => u.Inquiries)
                      .HasForeignKey(i => i.ClientId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(i => i.Agent)
                      .WithMany()
                      .HasForeignKey(i => i.AgentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
