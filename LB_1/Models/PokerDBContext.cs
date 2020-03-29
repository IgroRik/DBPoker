using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LB_1
{
    public partial class PokerDBContext : DbContext
    {
        public PokerDBContext()
        {
        }

        public PokerDBContext(DbContextOptions<PokerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Config> Config { get; set; }
        public virtual DbSet<Croupiers> Croupiers { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<GameList> GameList { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<Tables> Tables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning    To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-4U26IPS\\SQLEXPRESS; Database=PokerDB; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Croupiers>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Config)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.ConfigId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Game_Config");

                entity.HasOne(d => d.Croupier)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.CroupierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Game_Croupiers");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.TableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Game_Tables");
            });

            modelBuilder.Entity<GameList>(entity =>
            {
                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameList)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GameList_Game");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.GameList)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GameList_Player");
            });

            modelBuilder.Entity<Players>(entity =>
            {
                entity.Property(e => e.Birth).HasColumnType("date");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartCapital).HasColumnType("money");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
