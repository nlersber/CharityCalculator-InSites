using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using CharityCalculator.Controllers.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CharityCalculator.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration config;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration config)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.config = config;
        }

        /// <summary>
        /// Gets the roles of the currently logged in user
        /// </summary>
        /// <returns>List of roles</returns>
        [HttpGet("roles")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IList<string>> GetRolesAsync()
        {
            return await userManager.GetRolesAsync(await userManager.FindByNameAsync(User.Identity.Name));
        }

        /// <summary>
        /// Login method. Checks the provided credentials and returns a JWT token if valid. Token will contain basic information and roles.
        /// </summary>
        /// <param name="model">Model containing login information</param>
        /// <returns>JWT token</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && (await signInManager.CheckPasswordSignInAsync(user, model.Password, false)).Succeeded)
                return Ok(await GetToken(user)); //returns only the token
            return BadRequest();
        }

        /// <summary>
        /// Creates a JWT token based on the provided IdentityUser. Also appends role information
        /// </summary>
        /// <param name="user">User to create a token for</param>
        /// <returns>Token</returns>
        private async Task<string> GetToken(IdentityUser user)
        {
            // Create the token
            var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName) };
            claims.AddRange((await userManager.GetRolesAsync(user)).Select(s => new Claim(ClaimTypes.Role, s)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SomeRandomSecurityKeyButICouldNotShareThisOverGitHubSoThereYouGo"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                null, null,
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

}
