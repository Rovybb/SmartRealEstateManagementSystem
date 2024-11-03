using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllAsync();
        Task<Property> GetByIdAsync(Guid id);
        Task<Property> CreateAsync(Property property);
        Task UpdateAsync(Property property);
        Task DeleteAsync(Guid id);
    }
}
