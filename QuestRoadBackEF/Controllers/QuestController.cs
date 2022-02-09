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
        [HttpGet("Popular")]
        public async Task<IActionResult> GetMostPopularQuestsAsync()
        {
            try
            {
                var quests = await _questRepository.GetMostPopularQuestsAsync();
                if(quests == null)
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
        [HttpPost]
        public async Task<IActionResult> CreateQuestAsync([FromBody]Quest model)
        {
            try
            {
                await _questRepository.CreateQuestAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteQuestAsync(int id)
        {
            try
            {
                var quest = await _questRepository.GetQuestAsync(id);
                if(quest == null)
                {
                    return StatusCode(404, "Quest not found");
                }
                await _questRepository.DeleteQuestAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateQuestAsync(int id,[FromBody] Quest model)
        {
            try
            {
                var quest = await _questRepository.GetQuestAsync(id);
                if (quest == null)
                {
                    return StatusCode(404, "Quest not found");
                }
                await _questRepository.UpdateQuestAsync(id, model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("Comp")]
        public async Task<IActionResult> GetQuestsByCompanyIdAsync(int id)
        {
            try
            {
                var quests = await _questRepository.GetQuestsByCompanyIdAsync(id);
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
    }
}
