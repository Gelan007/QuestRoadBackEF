using Microsoft.EntityFrameworkCore;
using QuestRoadBackEF.Contracts;
using QuestRoadLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBackEF.Repositories
{
    public class ProfileRepository: IProfileRepository
    {
        private readonly QuestRoadContext _db;
        public ProfileRepository(QuestRoadContext db)
        {
            _db = db;
        }

        public async Task<User> GetUserInfoAsync(int id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task<Booking> GetUserBookingsAsync(int id)
        {
            var booking = await (from book in _db.Bookings
                                 join team in _db.Teams on book.TeamId equals team.TeamId
                                 join member in _db.Members on team.TeamId equals member.TeamId
                                 where member.UserId == id
                                 select new Booking
                                 {
                                     BookingId = book.BookingId,
                                     QuestId = book.QuestId,
                                     TeamId = book.TeamId,
                                     Time = book.Time,
                                     Description = book.Description,
                                     Price = book.Price
                                 }).FirstOrDefaultAsync();
            return booking;
        }
    }
}
