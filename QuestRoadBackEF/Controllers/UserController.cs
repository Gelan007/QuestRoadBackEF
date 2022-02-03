using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuestRoadBackEF.Contracts;
using QuestRoadLibrary;
using QuestRoadLibrary.DataAccess;
using QuestRoadLibrary.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuestRoadBackEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IOptions<AuthOptions> _options;
        public UserController(IUserRepository userRepository, IOptions<AuthOptions> options)
        {
            _userRepository = userRepository;
            _options = options;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            try
            {
                if (users == null)
                {
                    return StatusCode(404, "Пользователи не найдены");
                }
                return Ok(users);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromBody] Registration model)
        {
            try
            {
                await _userRepository.Registration(model);
                return Ok("Ok");
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            try
            {
                var user = await _userRepository.Login(model);
                if (user != null)
                {
                    //Generate token
                    var token = GenerateJWT(user);
                    return Ok(new { access_token = token });
                }
                return NotFound("Неверный логин или пароль");
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute]int id, User model)
        {
            try
            {
                var user = _userRepository.GetUserAsync(id);
                if(user == null)
                {
                    return StatusCode(404, "Not found");
                }
                else
                {
                    await _userRepository.UpdateUserAsync(id, model);
                    return Ok("Ok");
                }
                
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            try
            {
                var user = await _userRepository.GetUserAsync(id);
                if (user == null)
                {
                    return StatusCode(404, "Not found");
                }
                else
                {
                    await _userRepository.DeleteUserAsync(id);
                    return Ok($"Ok. User {id} has been deleted");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
            
        }

        private string GenerateJWT(User user)
        {
            var authParams = _options.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new Claim("user_id", user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.Phone),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("role", user.Role.ToString()),
                new Claim("company_id", user.CompanyId.ToString())
            };
            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
