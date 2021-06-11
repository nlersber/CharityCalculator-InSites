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
    public class TaxRateTest
    {

        private TaxRate rate;

        public TaxRateTest()
        {
            rate = new TaxRate { Rate = 20D };
        }

        [Fact]
        public void TestRate()
        {
            Assert.Equal(25D, rate.CalculateDonationDeductibleAmount(new Donation { Amount = 100 }));
        }



    }
}
