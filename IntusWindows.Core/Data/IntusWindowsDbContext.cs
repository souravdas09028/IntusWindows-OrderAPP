using IntusWindows.Common;
using IntusWindows.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntusWindows.Core.Data
{
    public class IntusWindowsDbContext : DbContext
    {
        public IntusWindowsDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            setDefaultValues(modelBuilder);
        }

        private void setDefaultValues(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Order>()
                .Property(b => b.ModifiedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Window>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Window>()
                .Property(b => b.ModifiedDate)
                .HasDefaultValueSql("getdate()");
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Window> Windows { get; set; }
        public DbSet<SubElement> SubElements { get; set; }
    }
}
