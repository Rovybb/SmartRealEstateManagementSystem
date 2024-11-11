using Domain.Entities;
using Domain.Utils;

namespace Domain.Repositories
{
    public interface IInquiryRepository
    {
        Task<IEnumerable<Inquiry>> GetAllAsync();
        Task<Result<Inquiry>> GetByIdAsync(Guid id);
        Task<Result<Guid>> CreateAsync(Inquiry inquiry);
        Task<Result<Guid>> UpdateAsync(Inquiry inquiry);
        Task<Result> DeleteAsync(Guid id);
    }
}
