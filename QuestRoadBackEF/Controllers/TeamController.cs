using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestRoadBackEF.Contracts;
using QuestRoadLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBackEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;
        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetTeamsAsync()
        {
            try
            {
                var teams = await _teamRepository.GetTeamsAsync();
                if (teams == null)
                {
                    return StatusCode(404, "Not found");
                }
                return Ok(teams);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamAsync(int id)
        {
            try
            {
                var team = await _teamRepository.GetTeamAsync(id);
                if (team == null)
                {
                    return StatusCode(404, "Not found");
                }
                return Ok(team);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeamAsync(Team model)
        {
            try
            {
                await _teamRepository.CreateTeamAsync(model);
                return Ok($"Ok");
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            } 
        }
    }
}
