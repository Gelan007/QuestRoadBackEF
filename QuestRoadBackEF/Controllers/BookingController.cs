using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestRoadBackEF.Contracts;
using QuestRoadLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBackEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IQuestRepository _questRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IHelpRepository _helpRepository;
        private int UserId => int.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public BookingController(IBookingRepository bookingRepository, IMemberRepository memberRepository, IQuestRepository questRepository, ITeamRepository teamRepository, IHelpRepository helpRepository)
        {
            _bookingRepository = bookingRepository;
            _memberRepository = memberRepository;
            _questRepository = questRepository;
            _teamRepository = teamRepository;
            _helpRepository = helpRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetBookingsAsync()
        {
            try
            {
                var users = await _bookingRepository.GetBookingsAsync();
                if (users == null)
                {
                    return StatusCode(404, "Users not found");
                }
                return Ok(users);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost("Constructor")]
        public async Task<IActionResult> CreateBookingAsync(BookingConstructor model)
        {
            try
            {
                DateTime today = DateTime.Now;
                var quest = await _questRepository.GetQuestAsync(model.QuestId);
                var cap = await _helpRepository.GetPhoneByIdAsync(UserId);

                if (quest == null || cap == null)
                {
                    return NotFound("Такого квеста или командира не существует.");
                }
                if(quest.MaxCountUsers < model.CountOfUsers || model.CountOfUsers <= 0)
                {
                    return BadRequest("Недопустимое количество человек в команде");
                }

                // создание команды
                await _teamRepository.CreateTeamFromBookingAsync(model.TeamName, model.CountOfUsers, cap.Phone);
                // получение айди созданной команды
                var team = await _teamRepository.GetTeamByNameAndPhoneAsync(model.TeamName, cap.Phone);
                if(team == null)
                {
                    return NotFound("Что-то пошло не так");
                }

                // создание мембера
                await _memberRepository.CreateMemberForBookingAsync(UserId, team.TeamId, today);

                await _bookingRepository.CreateBookingAsync(model.QuestId, team.TeamId, (int)quest.Price, model.Date, model.Description);
                return Ok("Ok!");
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        
    }
}
