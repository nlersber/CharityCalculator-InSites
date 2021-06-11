using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CharityCalculator.DTOs
{
    public class DonationDTO
    {
        [Required]
        [Range(0, double.MaxValue)]
        public double Amount { get; set; }
    }
}
