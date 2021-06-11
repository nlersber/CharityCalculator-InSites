using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharityCalculator.Data;
using CharityCalculator.Domain.IServices;
using CharityCalculator.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityCalculator.Domain.ServiceInstances
{
    public class DonationService : ServiceBase, IDonationService
    {
        private readonly DbSet<TaxRate> rate;

        public DonationService(Context context) : base(context)
        {
            this.rate = this.context.TaxRate;
        }

        public Task<TaxRate> GetCurrentTaxRate()
        {
            return rate.FirstAsync();
        }

        public async Task<bool> SetCurrentTaxRate(double amount)
        {
            var oldRate = await rate.FirstAsync();
            oldRate.Rate = amount;

            return (await context.SaveChangesAsync() != 0); // Signals success
        }

        public async Task<double> GetDeductableAmount(Donation donation)
        {
            return Math.Round(
                (await rate.FirstAsync()).CalculateDonationDeductibleAmount(donation), //Get deductible amount
                2); //Round it to 2 decimals
        }
    }
}
