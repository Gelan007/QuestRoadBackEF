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
        public async Task<IEnumerable<Quest>> GetQuests()
        {
            return await _db.Quests.ToListAsync();
        }
    }
}
