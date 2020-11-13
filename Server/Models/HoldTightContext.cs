using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Server.Models
{
    public partial class HoldTightContext : DbContext
    {
        public HoldTightContext()
        {
        }

        public HoldTightContext(DbContextOptions<HoldTightContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GoogleID> GoogleIds { get; set; }
        public virtual DbSet<ItemState> ItemStates { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<UserItemState> UserItemStates { get; set; }
        public virtual DbSet<UserItem> UserItems { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=hold_tight;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GoogleID>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("google_ids");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GoogleId)
                    .IsRequired()
                    .HasColumnName("google_id")
                    .HasMaxLength(21)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.User)
                    .WithOne(p => p.GoogleId)
                    .HasForeignKey<GoogleID>(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_google_ids_users");
            });

            modelBuilder.Entity<ItemState>(entity =>
            {
                entity.HasKey(e => e.ItemStateId);

                entity.ToTable("item_states");

                entity.Property(e => e.ItemStateId).HasColumnName("item_state_id");

                entity.Property(e => e.ItemStateName)
                    .HasColumnName("item_state_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.ToTable("items");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.ItemName)
                    .HasColumnName("item_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserItemState>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ItemId })
                    .HasName("PK_user_itemstate");

                entity.ToTable("user_item_states");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.ItemStateId).HasColumnName("item_state_id");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.UserItemStates)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_user_item_states_items");

                entity.HasOne(d => d.ItemState)
                    .WithMany(p => p.UserItemStates)
                    .HasForeignKey(d => d.ItemStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_item_states_item_states");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserItemStates)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_user_item_states_users");
            });

            modelBuilder.Entity<UserItem>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ItemId });

                entity.ToTable("user_items");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.UserItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_user_items_items");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserItems)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_user_items_users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK_users_1");

                entity.ToTable("users");

                entity.Property(e => e.Uid)
                    .HasColumnName("uid")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CharSkinId).HasColumnName("char_skin_id");

                entity.Property(e => e.Coins).HasColumnName("coins");

                entity.Property(e => e.Crystals).HasColumnName("crystals");

                entity.Property(e => e.DeviceId)
                    .HasColumnName("device_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TutorialParts).HasColumnName("tutorial_parts");

                entity.Property(e => e.UserLevel)
                    .HasColumnName("user_level")
                    .HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
