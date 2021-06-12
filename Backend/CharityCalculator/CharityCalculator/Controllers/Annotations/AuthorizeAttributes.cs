using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharityCalculator.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace CharityCalculator.Controllers.Annotations
{
    public class AuthorizeSiteAdmin : AuthorizeAttribute
    {
        public AuthorizeSiteAdmin()
        {
            Roles = Role.SiteAdmin;
        }
    }

    public class AuthorizeEventPromotor : AuthorizeAttribute
    {
        public AuthorizeEventPromotor()
        {
            Roles = Role.EventPromotor;
        }
    }
}
