using System;
using System.Collections.Generic;
using System.Text;
using CharityCalculator.Controllers;
using CharityCalculator.Domain.IServices;
using CharityCalculator.Domain.Models;
using CharityCalculator.Domain.ServiceInstances;
using Moq;
using Xunit;

namespace CharityTest.Controllers
{
    public class DonationControllerTest
    {

        private readonly DonationController con;

        public DonationControllerTest()
        {
            var mock = new Mock<DonationService>();
            mock.Setup(s => s.GetCurrentTaxRate()).ReturnsAsync(new TaxRate { Rate = 20D });
            mock.Setup(s => s.SetCurrentTaxRate(19)).ReturnsAsync(new TaxRate { Rate = 19D });
            mock.Setup(s => s.GetDeductableAmount(new Donation { Amount = 100D })).ReturnsAsync(25);

            con = new DonationController(mock.Object);
        }



    }
}
