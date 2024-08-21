using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.DBService.Model
{
    public partial class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public virtual DbSet<TblUser> TblUsers { get; set; }
        public virtual DbSet<TblRoomCategory> TblRoomCategories { get; set; }
        public virtual DbSet<TblRoom> TblRooms { get; set; }
       public virtual DbSet<TblBooking> TblBookings { get; set; }

        public virtual DbSet<TblPayment> TblPayments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblUser>(entity =>
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<TblUser>(entity =>
                {
                    entity.HasKey(e => e.UserId); // Primary key
                    entity.ToTable("Tbl_User"); // Table name

                    entity.Property(e => e.UserId)
                        .IsRequired()
                        .ValueGeneratedOnAdd(); // Identity column

                    entity.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(50);

                    entity.Property(e => e.PhoneNo)
                        .IsRequired()
                        .HasMaxLength(50);

                    entity.Property(e => e.Password)
                        .IsRequired()
                        .HasMaxLength(50);

                    entity.Property(e => e.Address)
                        .HasMaxLength(50);

                    entity.Property(e => e.Gender)
                        .HasMaxLength(50);

                    entity.Property(e => e.Role)
                        .HasMaxLength(50);
                });

                modelBuilder.Entity<TblRoomCategory>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.ToTable("Tbl_RoomCategory");
                    entity.Property(e => e.RoomType).HasMaxLength(50);

                });

                modelBuilder.Entity<TblRoom>(entity =>
                {
                    entity.HasKey(e => e.RoomId);
                    entity.ToTable("Tbl_Room");
                    entity.Property(e => e.RoomNo).HasMaxLength(50);
                    entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
                    entity.Property(e => e.facilities).HasMaxLength(50);
                    entity.Property(e => e.Status).HasMaxLength(50);
                    entity.Property(e => e.Image).HasMaxLength(50);
                    entity.Property(e => e.RoomCategoryId).IsRequired();
                });

                modelBuilder.Entity<TblBooking>(entity =>
                {
                    entity.HasKey(e => e.BookingId);
                    entity.ToTable("Tbl_Booking");
                    entity.Property(e => e.UserId).IsRequired();
                    entity.Property(e => e.RoomId).IsRequired();
                    entity.Property(e => e.CheckInDate).HasColumnType("datetime").IsRequired();
                    entity.Property(e => e.CheckOutDate).HasColumnType("datetime").IsRequired();
                    entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 0)").IsRequired();
                    entity.Property(e => e.PaymentStatus).HasMaxLength(50).IsRequired();
                    entity.Property(e => e.PaymentDate).HasColumnType("datetime");
                    entity.Property(e => e.CreatedDate).HasColumnType("datetime").IsRequired();
                    entity.Property(e => e.UpdatedAt).HasColumnType("datetime").IsRequired();
                });
                    modelBuilder.Entity<TblPayment>(entity =>
                    {
                        entity.HasKey(e => e.PaymentId);
                        entity.ToTable("Tbl_Payment");
                        entity.Property(e => e.BookingId).IsRequired();
                        entity.Property(e => e.PaymentMethod).HasMaxLength(50);
                        entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
                        entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                    });



            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
    }
