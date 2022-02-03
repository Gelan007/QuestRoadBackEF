using QuestRoadLibrary;
using QuestRoadLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBackEF.Contracts
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetUsersAsync();
        public Task<User> GetUserAsync(int id);
        public Task Registration(Registration model);
        public Task<User> Login(Login model);
        public Task UpdateUserAsync(int id, User model);
        public Task DeleteUserAsync(int id);
    }
}
