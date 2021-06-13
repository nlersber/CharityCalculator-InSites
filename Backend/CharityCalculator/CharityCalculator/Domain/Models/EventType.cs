using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharityCalculator.Domain.Models
{
    public class EventType
    {
        /// <summary>
        /// ID for DB
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Event name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Additional percentage added to deductible amount, as a percentage (e.g. '5' for '5%')
        /// </summary>
        public double Percentage { get; set; }
        /// <summary>
        /// Max amount that can be donated in a single donation to be deductible
        /// </summary>
        public double MaxAmount { get; set; }


        /// <summary>
        /// Changes the percentage from a percentage to a decimal (e.g. '5' to 0.05)
        /// </summary>
        /// <returns>Decimal form of percentage</returns>
        public double AsDecimal()
        {
            return Percentage / 100;
        }
    }
}
