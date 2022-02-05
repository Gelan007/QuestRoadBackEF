using Microsoft.EntityFrameworkCore;
using QuestRoadBackEF.Contracts;
using QuestRoadLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBackEF.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly QuestRoadContext _db;
        public MemberRepository(QuestRoadContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Member>> GetMembersAsync()
        {
            return await _db.Members.ToListAsync();
        }

        public async Task CreateMemberForBookingAsync(int userId, int teamId, DateTime whenAssigned)
        {
            var member = new Member();
            member.UserId = userId;
            member.TeamId = teamId;
            member.WhenAssigned = whenAssigned;
            await _db.Members.AddAsync(member);
            await _db.SaveChangesAsync();
        }

        
    }
}
