using NguyenHoangKhang_Test_3.Data;
using System.Linq.Expressions;

namespace NguyenHoangKhang_Test_3.Repository
{
    public interface IUserRepository
    {
        Task<bool> Create(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(User user);
        Task<List<User>> GetAll(Expression<Func<User, bool>> filter);
        Task<User> Get(Expression<Func<User, bool>> filter);
    }
}
