using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestRoadBackEF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBackEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IHelpRepository _helpRepsitory;
        private int UserId => int.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public RoleController(IHelpRepository helpRepository)
        {
            _helpRepsitory = helpRepository;
        }
        [HttpGet("Role")]
        public async Task<IActionResult> GetUserRoleAsync()
        {
            try
            {
                var role = await _helpRepsitory.IsAdminAsync(UserId);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
