using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharityCalculator.Controllers.Annotations;
using CharityCalculator.Controllers.DTOs;
using CharityCalculator.Domain.IServices;
using CharityCalculator.Domain.Models;
using CharityCalculator.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace CharityCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DonationController : ControllerBase
    {
        private readonly IDonationService donationService;

        public DonationController(IDonationService donationService)
        {
            this.donationService = donationService;
        }

        /// <summary>
        /// Fetches the current default tax rate for donations
        /// </summary>
        /// <returns>Current Tax Rate as a floating point number</returns>
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

        /// <summary>
        /// Sets the current tax rate to a new value
        /// </summary>
        /// <param name="dto">New value</param>
        /// <returns>New value</returns>
        [HttpPut]
        [Route("update")]
        [AuthorizeSiteAdmin]
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

        /// <summary>
        /// Calculates how much of a donation is tax deductible based on the amount and the event
        /// </summary>
        /// <param name="donation">Donation object with amount and event type</param>
        /// <returns>Deductible amount</returns>
        [HttpPost]
        [Route("calculateamount")]
        public async Task<ActionResult<double>> GetDeductibleAmount([FromBody] DonationDTO donation)
        {
            try
            {
                return Ok(await donationService.GetDeductableAmount(donation.Amount, donation.Type));
            }
            catch (Exception)
            {
                return BadRequest("Some error message");
            }
        }

        /// <summary>
        /// Fetches all the event types
        /// </summary>
        /// <returns>List of event types and what additional percentage gets added to the deductible amount</returns>
        [HttpGet]
        [Route("events")]
        public async Task<ActionResult<List<EventType>>> GetEventTypes()
        {
            try
            {
                return Ok(await donationService.GetEventTypes());
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Splits a donation into optimal pieces
        /// </summary>
        /// <param name="type">Name of type of event</param>
        /// <param name="amount">Amount in donation</param>
        /// <returns></returns>
        [HttpGet]
        [Route("optimal")]
        public async Task<ActionResult<List<double>>> GetOptimalSplit([FromQuery] string type,
            [FromQuery] double amount)
        {
            try
            {
                return Ok(await donationService.GetOptimalSplit(amount, type));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Changes the data store provider based on the given connection options
        /// </summary>
        /// <param name="dto">Object containing connection information</param>
        /// <returns>True if successful</returns>
        [HttpPut]
        [Route("changeprovider")]
        [AuthorizeSiteAdmin]
        public async Task<ActionResult<bool>> ChangeDataStoreProvider([FromBody] ConnectionDTO dto)
        {
            try
            {
                return Ok(await donationService.SetConnection(dto.Database, dto.Username, dto.Password));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

    }
}
