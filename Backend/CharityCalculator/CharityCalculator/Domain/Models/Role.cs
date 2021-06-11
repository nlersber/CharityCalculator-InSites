using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharityCalculator.Domain.Models
{
    public class Role
    {
        /// <summary>
        /// Site administrator, can change data
        /// </summary>
        public const string SiteAdmin = "SiteAdmin";

        /// <summary>
        /// Normal User
        /// </summary>
        public const string Donor = "Donor";

        /// <summary>
        /// Can change rates based on event type
        /// </summary>
        public const string EventPromotor = "EventPromotor";

    }
}
