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
    public class QuestController : ControllerBase
    {
        private readonly IQuestRepository _questRepository;

        public QuestController(IQuestRepository questRepository)
        {
            _questRepository = questRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestsAsync()
        {
            try
            {
                var quests = await _questRepository.GetQuestsAsync();
                if (quests == null)
                {
                    return StatusCode(404, "Quests not found");
                }
                return Ok(quests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestAsync([FromRoute]int id)
        {
            try
            {
                var quest = await _questRepository.GetQuestAsync(id);
                if (quest == null)
                {
                    return StatusCode(404, "Not found");
                }
                return Ok(quest);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
}
