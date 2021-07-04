using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BankEF
{
    public partial class TestDBContext : DbContext
    {
        public TestDBContext()
        {
            Individuals.Load();
            Entities.Load();
            Bills.Load();
            Vips.Load();
        }

        public TestDBContext(DbContextOptions<TestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<Individual> Individuals { get; set; }
        public virtual DbSet<Vip> Vips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.InitialBalance).HasColumnType("money");

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<Entity>(entity =>
            {

                entity.ToTable("Entity");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('DefaultName')")
                    .IsFixedLength(true);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('???')")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Individual>(entity =>
            {

                entity.ToTable("Individual");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasDefaultValueSql("('01.01.1990')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('Defaultname')")
                    .IsFixedLength(true);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('DefaultSurname')")
                    .IsFixedLength(true);

                entity.Property(e => e.Workplace)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('DefaultWorkplace')")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Vip>(entity =>
            {

                entity.ToTable("VIP");

                entity.Property(e => e.Id).ValueGeneratedNever();


                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasDefaultValueSql("('01.01.1990')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('Defaultname')")
                    .IsFixedLength(true);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('DefaultSurname')")
                    .IsFixedLength(true);

                entity.Property(e => e.Workplace)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('DefaultWorkplace')")
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
      
    }
}
