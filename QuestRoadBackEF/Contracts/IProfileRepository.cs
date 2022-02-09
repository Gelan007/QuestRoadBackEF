using QuestRoadLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBackEF.Contracts
{
    public interface IProfileRepository
    {
        public Task<User> GetUserInfoAsync(int id);
        public Task<Booking> GetUserBookingsAsync(int id);
    }
}
