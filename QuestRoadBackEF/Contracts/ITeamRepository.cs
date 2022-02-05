using QuestRoadLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBackEF.Contracts
{
    public interface ITeamRepository
    {
        public Task<IEnumerable<Team>> GetTeamsAsync();
        public Task<Team> GetTeamAsync(int id);
        public Task CreateTeamAsync(Team model);
        public Task CreateTeamFromBookingAsync(string name, int count, string phone);
        public Task<Team> GetTeamByNameAndPhoneAsync(string name, string phone);

    }
}
