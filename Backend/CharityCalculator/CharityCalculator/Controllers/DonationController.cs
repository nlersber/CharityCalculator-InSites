using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharityCalculator.Domain.IServices;
using CharityCalculator.Domain.Models;
using CharityCalculator.DTOs;

namespace CharityCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly IDonationService donationService;

        public DonationController(IDonationService donationService)
        {
            this.donationService = donationService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("currentrate")]
        public async Task<ActionResult<double>> GetCurrentTaskRate()
        {
            try
            {
                return Ok(await donationService.GetCurrentTaxRate());
            }
            catch (Exception)
            {
                return NotFound("Some error message");
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult<bool>> SetCurrentTaskRate([FromBody] TaxRateDTO dto)
        {
            try
            {
                return Ok(await donationService.SetCurrentTaxRate(dto.Amount));
            }
            catch (Exception)
            {
                return BadRequest("Some error message");
            }
        }

        [HttpGet]
        [Route("getdeductible")]
        public async Task<ActionResult<double>> GetDeductibleAmount([FromBody] DonationDTO donation)
        {
            try
            {
                return Ok(await donationService.GetDeductableAmount(new Donation { Amount = donation.Amount }));
            }
            catch (Exception)
            {
                return BadRequest("Some error message");
            }
        }

    }
}
