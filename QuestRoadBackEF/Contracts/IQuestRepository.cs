using QuestRoadLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBackEF.Contracts
{
    public interface IQuestRepository
    {
        public Task<IEnumerable<Quest>> GetQuests();
    }
}
