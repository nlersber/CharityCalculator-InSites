using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharityCalculator.Domain.Models;

namespace CharityCalculator.Domain.IServices
{
    public interface IDonationService
    {
        /// <summary>
        /// Returns the current tax rate.
        /// </summary>
        /// <returns>Current tax rate</returns>
        Task<TaxRate> GetCurrentTaxRate();
        /// <summary>
        /// Updates the tax rate.
        /// </summary>
        /// <param name="amount">New value</param>
        /// <returns>Whether the update was successful</returns>
        Task<TaxRate> SetCurrentTaxRate(double amount);

        /// <summary>
        /// Calculates the deductible amount for the given donation.
        /// </summary>
        /// <param name="donation">Donation to calculate deductible amount of</param>
        /// <returns>Deductible amount</returns>
        Task<double> GetDeductableAmount(Donation donation);
    }
}
