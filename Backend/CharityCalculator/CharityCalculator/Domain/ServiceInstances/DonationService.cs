using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public Task<double> GetCurrentTaxRate()
        {
            return rate.AsNoTracking().Select(s => s.Rate).FirstAsync();
        }

        public async Task<double> SetCurrentTaxRate(double amount)
        {
            var oldRate = await rate.FirstAsync();
            oldRate.Rate = amount;
            await context.SaveChangesAsync();
            return await rate.AsNoTracking().Select(s => s.Rate).FirstAsync();
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

        public async Task<List<double>> GetOptimalSplit(double amount, string eventType)
        {
            var type = await eventTypes.SingleAsync(s => s.Name == eventType);

            var remainder = amount % type.MaxAmount; // Gets remainder
            var times = Convert.ToInt32(Math.Floor(amount / type.MaxAmount)); // Gets amount of times the maximum can be reached

            var output = Enumerable.Repeat(Math.Round(type.MaxAmount, 2), times).ToList(); // Splits the total into maximum size pieces
            if (remainder != 0)
                output.Add(Math.Round(remainder, 2));// Adds the remainder

            return output;
        }

        public async Task<bool> SetConnection(string conn)
        {
            var old = context.Database.GetConnectionString();
            await context.Database.CloseConnectionAsync();
            try
            {
                if (!await context.Database.CanConnectAsync()) return false; //Stop if can't connect

                context.Database.SetConnectionString(conn);
                await context.Database.OpenConnectionAsync(); // Open

                return true;
            }
            catch (Exception)
            {
                context.Database.SetConnectionString(old);
                await context.Database.OpenConnectionAsync();
                return false;
            }
        }

        public Task<bool> SetConnection(string database, string username, string password)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = database,
                UserID = username,
                Password = password
            };

            return SetConnection(builder.ToString());
        }
    }
}
