using Microsoft.EntityFrameworkCore;
using NguyenHoangKhang_Test_3.Data;
using NguyenHoangKhang_Test_3.Data.Configurations;
using System.Linq.Expressions;

namespace NguyenHoangKhang_Test_3.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(User user)
        {
            await _context.Users.AddAsync(user);
            var result = await _context.SaveChangesAsync();
            return Convert.ToBoolean(result);
        }

        public async Task<bool> Delete(User user)
        {
            _context.Users.Remove(user);
            var result = await _context.SaveChangesAsync();
            return Convert.ToBoolean(result);
        }

        public async Task<User> Get(Expression<Func<User, bool>> filter)
        {
            return await _context.Users.FirstOrDefaultAsync(filter);
        }

        public async Task<List<User>> GetAll(Expression<Func<User, bool>> filter)
        {
            return await _context.Users.Where(filter).ToListAsync();
        }

        public async Task<bool> Update(User user)
        {
            _context.Update(user);
            var result = await _context.SaveChangesAsync();
            return Convert.ToBoolean(result);
        }
    }
}
