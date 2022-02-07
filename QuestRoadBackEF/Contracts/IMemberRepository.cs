using QuestRoadLibrary;
using QuestRoadLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBackEF.Contracts
{
    public interface IMemberRepository
    {
        public Task<IEnumerable<Member>> GetMembersAsync();
        public Task CreateMemberAsync(Member model);
        public Task<int> GetCountOfUsersByTeamIdAsync(int id);
        public Task CreateMemberForBookingAsync(int userId, int teamId, DateTime whenAssigmed);
    }
}
