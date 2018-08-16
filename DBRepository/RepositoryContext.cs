
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBRepository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Owner> Owners { get; set; }

       // public DbSet<CarOwner> CarOwners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarOwner>()
        .HasKey(pc => new { pc.CarId, pc.OwnerId });

            modelBuilder.Entity<CarOwner>()
                .HasOne(co => co.Owner)
                .WithMany(o => o.CarOwners)
                .HasForeignKey(co => co.OwnerId);

            modelBuilder.Entity<CarOwner>()
                .HasOne(co => co.Car)
                .WithMany(c => c.CarOwners)
                .HasForeignKey(co => co.CarId);
        }
    }
}
