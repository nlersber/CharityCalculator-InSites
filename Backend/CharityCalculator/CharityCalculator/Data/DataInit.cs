using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharityCalculator.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace CharityCalculator.Data
{
    public class DataInit
    {
        private readonly Context context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DataInit(Context context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task Init()
        {

            if (await context.Database.EnsureCreatedAsync())
            {
                await CreateRoles();
                await CreateUsers();
                await InitData();
            }
        }

        private async Task InitData()
        {
            var rate = new TaxRate { Rate = 20D };
            await context.TaxRate.AddAsync(rate);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Creates default users for demo purposes
        /// </summary>
        /// <returns></returns>
        private async Task CreateUsers()
        {
            await CreateUser("SiteAdmin", "admin@test.test", "TestSiteAdmin", new[] { Role.SiteAdmin });
            await CreateUser("Donor", "donor@test.test", "TestDonor", new[] { Role.Donor });
            await CreateUser("EventPromotor", "eventpromotor@test.test", "TestEventPromotor", new[] { Role.EventPromotor });
        }

        private async Task CreateUser(string name, string email, string password, string[] roles)
        {
            var user = new IdentityUser { UserName = name, Email = email };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var createdUser = await userManager.FindByNameAsync(user.UserName);
                await userManager.AddToRolesAsync(createdUser, roles);
            }

        }

        private async Task CreateRoles()
        {
            string[] roles = { Role.Donor, Role.SiteAdmin, Role.EventPromotor };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }


}
