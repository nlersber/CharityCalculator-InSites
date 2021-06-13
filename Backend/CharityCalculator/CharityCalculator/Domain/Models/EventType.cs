using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharityCalculator.Domain.Models
{
    public class EventType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Percentage { get; set; }

        public double AsDecimal()
        {
            return Percentage / 100;
        }
    }
}
