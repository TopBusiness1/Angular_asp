using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserWebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using UserWebAPI.Entities;

namespace UserWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(
            IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("api/auth/Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]AccountModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded)
            {
                return Ok(new
                {
                    token = GenerateJwtToken(model)
                });
            }
            return Unauthorized();
        }

        [AllowAnonymous]
        [Route("api/User/Register", Name = "GetUser")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]AccountModel model) //add async Task<Result>
        {
            var userStore = _mapper.Map<User>(model);
            var manager = await _userManager.CreateAsync(userStore, model.Password);
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address
            };

            if (manager.Succeeded)
            {
                return CreatedAtRoute("GetUser", new { id = userStore.Id }, user);
            }
            return BadRequest(manager.Errors);
        }

        private static string GenerateJwtToken(AccountModel model)
        {
            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.NameIdentifier, model.Id.ToString()),
                new Claim (ClaimTypes.Name, model.UserName)
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = signinCredentials,
                Audience = "http://localhost:5000",
                Issuer = "http://localhost:5000"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
