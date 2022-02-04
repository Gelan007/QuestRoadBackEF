using Microsoft.EntityFrameworkCore;
using QuestRoadAuth.Contracts;
using QuestRoadLibrary;
using QuestRoadLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadAuth.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly QuestRoadContext _db;
        public UserRepository(QuestRoadContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _db.Users.ToListAsync();   
        }
        public async Task<User> GetUserAsync(int id)
        {
            return await _db.Users.FindAsync(id);
        }
        public async Task Registration(Registration model)
        {
            UserRole role = UserRole.User;
            int companyId = 0;

            var user = new User();
            user.Email = model.Email;
            user.Phone = model.Phone;
            user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            user.Name = model.Name;
            user.Role = role;
            user.CompanyId = companyId;

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task<User> Login(Login model)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                return null;
            }
            return user;
        }
        public async Task UpdateUserAsync(int id, User model)
        {
            var user = await _db.Users.FirstOrDefaultAsync(us => us.UserId == id);
            user.Email = model.Email;
            user.Phone = model.Phone;
            user.Password = model.Password;
            user.Name = model.Name;
            user.Role = model.Role;
            user.CompanyId = model.CompanyId;
            _db.Entry(user).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
        public async Task DeleteUserAsync(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(us => us.UserId == id);
            _db.Entry(user).State = EntityState.Deleted;
            await _db.SaveChangesAsync();
        }
    }
}
