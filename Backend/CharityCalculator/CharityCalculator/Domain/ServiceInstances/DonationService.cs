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
        private readonly DbSet<EventType> eventTypes;

        public DonationService(Context context) : base(context)
        {
            rate = this.context.TaxRate;
            eventTypes = this.context.EventTypes;
        }

        public Task<TaxRate> GetCurrentTaxRate()
        {
            return rate.AsNoTracking().FirstAsync();
        }

        public async Task<TaxRate> SetCurrentTaxRate(double amount)
        {
            var oldRate = await rate.FirstAsync();
            oldRate.Rate = amount;
            await context.SaveChangesAsync();
            return await rate.FirstAsync();
        }

        public async Task<double> GetDeductableAmount(double amount, string eventType)
        {
            var type = await eventTypes.SingleAsync(s => s.Name == eventType);
            var donation = new Donation { Amount = amount, Type = type };
            return Math.Round(
                (await rate.AsNoTracking().FirstAsync()).CalculateDonationDeductibleAmount(donation), //Get deductible amount
                2); //Round it to 2 decimals
        }

        public Task<List<EventType>> GetEventTypes()
        {
            return eventTypes.AsNoTracking().ToListAsync();
        }
    }
}
