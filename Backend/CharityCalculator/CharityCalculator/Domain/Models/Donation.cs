using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharityCalculator.Domain.Models
{
    public class Donation
    {
        /// <summary>
        /// Amount in donation
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// Type of event that this donation was donated to
        /// </summary>
        public EventType Type { get; set; }
    }

}
