using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharityCalculator.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CharityCalculator.Data
{
    public class Context : IdentityDbContext
    {
        public DbSet<TaxRate> TaxRate { get; set; }
        public DbSet<EventType> EventTypes { get; set; }

        public Context(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TaxRate>().HasKey(s => s.Rate); //Single value only
            builder.Entity<EventType>().HasKey(s => s.Name);
        }
    }
}
