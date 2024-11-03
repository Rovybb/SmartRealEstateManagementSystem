using Domain.Entities;
using Domain.Repositories;
using Domain.Utils;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ApplicationDbContext context;

        public PropertyRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Property>> GetAllAsync()
        {
            return await context.Properties.ToListAsync();
        }

        public async Task<Property> GetByIdAsync(Guid id)
        {
            return await context.Properties.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Result<Guid>> CreateAsync(Property property)
        {
            try
            {
                await context.Properties.AddAsync(property);
                await context.SaveChangesAsync();
                return Result<Guid>.Success(property.Id);
            }
            catch (Exception ex) {
                return Result<Guid>.Failure(ex.Message);
            }
        }

        public async Task UpdateAsync(Property property)
        {
            context.Entry(property).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var property = await context.Properties.FirstOrDefaultAsync(x => x.Id == id);
            if (property != null)
            {
                context.Properties.Remove(property);
                await context.SaveChangesAsync();
            }
        }
    }
}
