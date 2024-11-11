using Domain.Entities;
using Domain.Utils;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<Result<User>> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<Result<Guid>> CreateAsync(User user);
        Task<Result<Guid>> UpdateAsync(User user);
        Task<Result> DeleteAsync(Guid id);
    }
}
