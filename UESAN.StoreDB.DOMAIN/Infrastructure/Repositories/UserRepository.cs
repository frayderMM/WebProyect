using Microsoft.EntityFrameworkCore;
using UESAN.StoreDB.DOMAIN.Core.Entities;
using UESAN.StoreDB.DOMAIN.Core.Interfaces;
using UESAN.StoreDB.DOMAIN.Infrastructure.Data;

namespace UESAN.StoreDB.DOMAIN.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly PromcoserContext _dbContext;

        public UserRepository(PromcoserContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> SignIn(string email, string pwd)
        {
            return await _dbContext
                    .User
                    .Where(u => u.Email == email && u.Password == pwd)
                    .FirstOrDefaultAsync();
        }

        public async Task<bool> SignUp(User user)
        {
            await _dbContext.User.AddAsync(user);
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }


    }
}
