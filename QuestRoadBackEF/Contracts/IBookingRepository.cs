using QuestRoadLibrary;
using QuestRoadLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBackEF.Contracts
{
    public interface IBookingRepository
    {
        public Task<IEnumerable<Booking>> GetBookingsAsync();
        public Task CreateBookingAsync(int questId, int teamId, int price, DateTime date, string description);
        public Task UpdateBookingPriceAsync(int teamId, double coef);



    }
}
