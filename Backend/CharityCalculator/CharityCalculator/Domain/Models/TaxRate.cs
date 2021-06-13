using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharityCalculator.Domain.Models
{
    public class TaxRate
    {
        public int Id { get; set; }
        public double Rate { get; set; }

        /// <summary>
        /// Returns a decimal version of the percentage
        /// </summary>
        /// <returns>Decimal</returns>
        public double GetDecimal()
        {
            return Rate / 100;
        }

        /// <summary>
        /// Calculates the Deductible amount for the provided donation
        /// </summary>
        /// <param name="don">Donation to calculate from</param>
        /// <returns>Deductible amount</returns>
        public double CalculateDonationDeductibleAmount(Donation don)
        {
            return don.Amount * (don.Type.AsDecimal()+1) * (Rate / (100 - Rate));
        }
    }
}
