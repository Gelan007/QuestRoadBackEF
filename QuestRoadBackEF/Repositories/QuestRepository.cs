using Microsoft.EntityFrameworkCore;
using QuestRoadBackEF.Contracts;
using QuestRoadLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBackEF.Repositories
{
    public class QuestRepository: IQuestRepository
    {
        private readonly QuestRoadContext _db;

        public QuestRepository(QuestRoadContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Quest>> GetQuestsAsync()
        {
            return await _db.Quests.ToListAsync();
        }
        public async Task<Quest> GetQuestAsync(int id)
        {
            return await _db.Quests.FindAsync(id);
        }

        public async Task CreateQuestAsync(Quest model)
        {
            var quest = new Quest();

            quest.Name = model.Name;
            quest.Description = model.Description;
            quest.DifficultyLevel = model.DifficultyLevel;
            quest.City = model.City;
            quest.Adress = model.Adress;
            quest.Category = model.Category;
            quest.Actors = model.Actors;
            quest.CompanyId = model.CompanyId;
            quest.MaxCountUsers = model.MaxCountUsers;
            quest.Price = model.Price;
            quest.Photo = model.Photo;

            await _db.Quests.AddAsync(quest);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateQuestAsync(int id, Quest model)
        {
            var quest = await _db.Quests.FirstOrDefaultAsync(q => q.QuestId == id);

            quest.Name = model.Name;
            quest.Description = model.Description;
            quest.DifficultyLevel = model.DifficultyLevel;
            quest.City = model.City;
            quest.Adress = model.Adress;
            quest.Category = model.Category;
            quest.Actors = model.Actors;
            quest.CompanyId = model.CompanyId;
            quest.MaxCountUsers = model.MaxCountUsers;
            quest.Price = model.Price;
            quest.Photo = model.Photo;

            _db.Entry(quest).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteQuestAsync(int id)
        {
            var quest = await _db.Quests.FirstOrDefaultAsync(q => q.QuestId == id);
            _db.Entry(quest).State = EntityState.Deleted;
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Quest>> GetMostPopularQuestsAsync()
        {
            var quests = await _db.Quests.FromSqlRaw("select Quest.quest_id, Quest.name, Quest.description, Quest.difficulty_level, Quest.city, Quest.adress, Quest.category, Quest.actors, Quest.company_id, Quest.max_count_users, Quest.price, Quest.photo from Quest Left join Booking on Quest.quest_id = Booking.quest_id group by Quest.quest_id, Quest.name, Quest.description, Quest.difficulty_level, Quest.city, Quest.adress, Quest.category, Quest.actors, Quest.company_id, Quest.max_count_users, Quest.price, Quest.photo having count(Booking.booking_id) >= 0 order by count(Booking.booking_id) desc").ToListAsync();
            return quests;
        }

        public async Task<IEnumerable<Quest>> GetQuestsByCompanyIdAsync(int id)
        {
            return await _db.Quests.Where(q => q.CompanyId == id).ToListAsync();
     
        }

        public Task CreateQuestAsync(string Name, string Description, string Difficulty_level, string City, string Adress, string Category, string Actors, int Company_id, int Max_count_users, int Price, string Photo)
        {
            throw new NotImplementedException();
        }
    }
}
