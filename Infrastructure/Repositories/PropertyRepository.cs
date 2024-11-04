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

        public async Task<Result<Guid>> UpdateAsync(Property property)
        {
            try
            {
                var existingProperty = await context.Properties.FindAsync(property.Id);
                if (existingProperty == null)
                {
                    return Result<Guid>.Failure("Property not found.");
                }

                context.Entry(existingProperty).CurrentValues.SetValues(property);
                await context.SaveChangesAsync();
                return Result<Guid>.Success(existingProperty.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(ex.Message);
            }
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
