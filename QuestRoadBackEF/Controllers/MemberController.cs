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
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        public MemberController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
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
        
    }
}
