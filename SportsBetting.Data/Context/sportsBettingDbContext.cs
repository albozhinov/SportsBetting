using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBetting.Data.Context
{
    public class sportsBettingDbContext : DbContext
    {

        public sportsBettingDbContext()
        {

        }

        public sportsBettingDbContext(DbContextOptions<sportsBettingDbContext> options)
            : base(options)
        {

        }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
