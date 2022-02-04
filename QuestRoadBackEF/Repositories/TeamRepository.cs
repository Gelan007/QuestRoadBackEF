using Microsoft.EntityFrameworkCore;
using QuestRoadBackEF.Contracts;
using QuestRoadLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBackEF.Repositories
{
    public class TeamRepository: ITeamRepository
    {
        private readonly QuestRoadContext _db;
        public TeamRepository(QuestRoadContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Team>> GetTeamsAsync()
        {
            return await _db.Teams.ToListAsync();
        }
        public async Task<Team> GetTeamAsync(int id)
        {
            return await _db.Teams.FindAsync(id);
        }
        public async Task CreateTeamAsync(Team model)
        {
            var team = new Team();
            team.Name = model.Name;
            team.Count = model.Count;
            team.Phone = model.Phone;

            await _db.Teams.AddAsync(team);
            await _db.SaveChangesAsync();
        }

    }
}
