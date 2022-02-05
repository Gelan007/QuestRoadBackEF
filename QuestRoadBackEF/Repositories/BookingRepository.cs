using Microsoft.EntityFrameworkCore;
using QuestRoadBackEF.Contracts;
using QuestRoadLibrary;
using QuestRoadLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBackEF.Repositories
{
    public class BookingRepository: IBookingRepository
    {
        private readonly QuestRoadContext _db;
        
        public BookingRepository(QuestRoadContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Booking>> GetBookingsAsync()
        {
            return await _db.Bookings.ToListAsync();
        }
        public async Task CreateBookingAsync(int questId, int teamId, int price, DateTime date, string description)
        {
            var booking = new Booking();
            booking.QuestId = questId;
            booking.TeamId = teamId;
            booking.Price = price;
            booking.Time = date;
            booking.Description = description;
            await _db.Bookings.AddAsync(booking);
            await _db.SaveChangesAsync();
        }
       
           
    }
}
