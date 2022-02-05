using QuestRoadLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBackEF.Contracts
{
    public interface IMemberRepository
    {
        public Task<IEnumerable<Member>> GetMembersAsync();
        public Task CreateMemberForBookingAsync(int userId, int teamId, DateTime whenAssigmed);
    }
}
