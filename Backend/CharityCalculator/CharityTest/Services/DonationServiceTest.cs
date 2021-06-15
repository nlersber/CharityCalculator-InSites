using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharityCalculator.Data;
using CharityCalculator.Domain.Models;
using CharityCalculator.Domain.ServiceInstances;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CharityTest.NewFolder
{
    public class DonationServiceTest
    {
        private readonly DonationService service;
        private readonly List<EventType> data = new List<EventType>()
        {
             new EventType { Name = "Other", Percentage = 0, MaxAmount = 10000 },
             new EventType { Name = "Sports", Percentage = 5, MaxAmount = 100000 },
             new EventType { Name = "Political", Percentage = 3, MaxAmount = 50000 }
        };

        public DonationServiceTest()
        {
            service = new DonationService(SetupMock());
        }

        /// <summary>
        /// Sets up a mock DbContext and DbSet
        /// </summary>
        /// <returns>Mocked and set-up DbContext as object</returns>
        private Context SetupMock()
        {
            var mockContext = new Mock<Context>();
            var mockSet = new Mock<DbSet<EventType>>();
            var queryableData = data.AsQueryable();

            mockSet.As<IQueryable<EventType>>().Setup(m => m.Provider)
                .Returns(queryableData.Provider);

            mockSet.As<IQueryable<EventType>>().Setup(m => m.Expression)
                .Returns(queryableData.Expression);

            mockSet.As<IQueryable<EventType>>().Setup(m => m.ElementType)
                .Returns(queryableData.ElementType);

            mockSet.As<IQueryable<EventType>>().Setup(m => m.GetEnumerator())
                .Returns(queryableData.GetEnumerator());

            mockContext.SetupGet(s => s.EventTypes).Returns(mockSet.Object);

            return mockContext.Object;
        }


        //new EventType{Name = "Sports", Percentage = 5, MaxAmount = 100000},
        //new EventType{Name = "Political", Percentage = 3, MaxAmount = 50000},
        //new EventType { Name = "Other", Percentage = 0, MaxAmount = 10000 }
        [Theory]
        [InlineData(10001D, 2, 1, "Other")]
        [InlineData(10000D, 1, 0, "Other")]
        [InlineData(50001D, 2, 1, "Political")]
        [InlineData(50000D, 1, 0, "Political")]
        [InlineData(100001D, 2, 1, "Sports")]
        [InlineData(100000D, 1, 0, "Sports")]
        public void TestGetOptimalSplit(double input, int length, double remainder, string type)
        {
            var result = service.GetOptimalSplit(input, type).Result;
            Assert.Equal(length, result.Count);
            Assert.Contains(remainder, result);
        }
    }
}
