using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UmeaBTKRanking.Models.Entities
{
    public partial class PingisContext : DbContext
    {
		public PingisContext()
		{
		}

		public virtual DbSet<Match> Match { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Team> Team { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Player1)
                    .WithMany(p => p.MatchPlayer1)
                    .HasForeignKey(d => d.Player1Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayerOne");

                entity.HasOne(d => d.Player2)
                    .WithMany(p => p.MatchPlayer2)
                    .HasForeignKey(d => d.Player2Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayerTwo");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.Elo).HasDefaultValueSql("((1000))");

                entity.Property(e => e.MatchesLost).HasColumnName("MAtchesLost");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TeamId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Player)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Team");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
