using Domain.Entities;
using Domain.Utils;

namespace Domain.Repositories
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllAsync();
        Task<Property> GetByIdAsync(Guid id);
        Task<Result<Guid>> CreateAsync(Property property);
        Task<Result<Guid>> UpdateAsync(Property property);
        Task DeleteAsync(Guid id);
    }
}
