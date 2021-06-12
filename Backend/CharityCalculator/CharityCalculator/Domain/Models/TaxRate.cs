using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharityCalculator.Domain.Models
{
    public class TaxRate
    {
        public double Rate { get; set; }

        /// <summary>
        /// Calculates the Deductible amount for the provided donation
        /// </summary>
        /// <param name="don">Donation to calculate from</param>
        /// <returns>Deductible amount</returns>
        public double CalculateDonationDeductibleAmount(Donation don)
        {
            return don.Amount * don.Type.Percentage * (Rate / (100 - Rate));
        }
    }
}
