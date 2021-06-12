using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CharityCalculator.DTOs
{
    public sealed class TaxRateDTO
    {
        [Required]
        [Range(0, 100)]
        public double Amount { get; set; }
    }
}
