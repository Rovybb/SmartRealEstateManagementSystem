using Domain.Entities;
using Domain.Utils;

namespace Domain.Repositories
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllAsync();
        Task<Result<Property>> GetByIdAsync(Guid id);
        Task<Result<Guid>> CreateAsync(Property property);
        Task<Result> UpdateAsync(Property property);
        Task<Result> DeleteAsync(Guid id);
    }
}
