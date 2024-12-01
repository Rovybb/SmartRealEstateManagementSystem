using Domain.Entities;
using Domain.Utils;

namespace Domain.Repositories
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllAsync();
        Task<PaginatedList<Property>> GetPropertiesAsync(int pageNumber, int pageSize, Dictionary<string, string>? filters);
        Task<Result<Property>> GetByIdAsync(Guid id);
        Task<Result<Guid>> CreateAsync(Property property);
        Task<Result> UpdateAsync(Property property);
        Task<Result> DeleteAsync(Guid id);
    }
}
