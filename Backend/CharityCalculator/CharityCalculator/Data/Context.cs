using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharityCalculator.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityCalculator.Data
{
    public class Context : DbContext
    {
        public DbSet<TaxRate> TaxRate { get; set; }

        public Context(DbContextOptions options) : base(options) { }
    }
}
