using Microsoft.EntityFrameworkCore;
using QuestRoadBackEF.Contracts;
using QuestRoadLibrary;
using QuestRoadLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBackEF.Repositories
{
    public class HelpRepository:IHelpRepository
    {
        private readonly QuestRoadContext _db;
        public HelpRepository(QuestRoadContext db)
        {
            _db = db;
        }

        public async Task<User> GetPhoneByIdAsync(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<UserRole> IsAdminAsync(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == id);
            return user.Role;
        }
        
    }
}
