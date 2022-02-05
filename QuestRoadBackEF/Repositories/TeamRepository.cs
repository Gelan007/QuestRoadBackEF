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

        public async Task CreateTeamFromBookingAsync(string name, int count, string phone)
        {
            var team = new Team();
            team.Name = name;
            team.Count = count;
            team.Phone = phone;

            await _db.Teams.AddAsync(team);
            await _db.SaveChangesAsync();

        }

        public async Task<Team> GetTeamByNameAndPhoneAsync(string name, string phone)
        {
            return await _db.Teams.FirstOrDefaultAsync(t => t.Name == name && t.Phone == phone);
        }
    }

}
