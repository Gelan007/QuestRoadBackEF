using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestRoadBackEF.Contracts;
using QuestRoadLibrary;
using QuestRoadLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBackEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IBookingRepository _bookingRepository;
        public MemberController(IMemberRepository memberRepository, IBookingRepository bookingRepository)
        {
            _memberRepository = memberRepository;
            _bookingRepository = bookingRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetMembersAsync()
        {
            try
            {
                var members = await _memberRepository.GetMembersAsync();
                if (members == null)
                {
                    return StatusCode(404, "Members not found");
                }
                return Ok(members);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }     
        }
        [HttpPost]
        public async Task<IActionResult> CreateMemberWithBookingSalesAsync(Member model)
        {
            try
            {
                int count = await _memberRepository.GetCountOfUsersByTeamIdAsync(model.TeamId);
                double coef = 1;
                await _memberRepository.CreateMemberAsync(model);
               
                if(count >= 3)
                {
                    switch (count)
                    {
                        case 3:
                            coef = 0.95;
                            break;
                        case 4:
                            coef = 0.90;
                            break;
                        default:
                            coef = 0.85;
                            break;
                    }
                    await _bookingRepository.UpdateBookingPriceAsync(model.TeamId, coef);
                }
                return Ok("Ok");
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }


    }
}
