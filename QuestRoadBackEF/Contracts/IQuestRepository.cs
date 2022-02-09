using QuestRoadLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBackEF.Contracts
{
    public interface IQuestRepository
    {
        public Task<IEnumerable<Quest>> GetQuestsAsync();
        public Task<Quest> GetQuestAsync(int id);
        public Task CreateQuestAsync(Quest quest);
        public Task UpdateQuestAsync(int id, Quest quest);
        public Task DeleteQuestAsync(int id);
        public Task<IEnumerable<Quest>> GetMostPopularQuestsAsync();
        public Task<IEnumerable<Quest>> GetQuestsByCompanyIdAsync(int id);
        public Task CreateQuestAsync(string Name, string Description, string Difficulty_level, string City, string Adress, string Category, string Actors, int Company_id, int Max_count_users, int Price, string Photo);
    }
}
