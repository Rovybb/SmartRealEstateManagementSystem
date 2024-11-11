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

        public async Task<Result<Property>> GetByIdAsync(Guid id)
        {
            var property = await context.Properties.FindAsync(id);
            if (property == null)
            {
                return Result<Property>.Failure("Property not found.");
            }

            return Result<Property>.Success(property);
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

        public async Task<Result> UpdateAsync(Property property)
        {
            try
            {
                var existingProperty = await context.Properties.FindAsync(property.Id);
                if (existingProperty == null)
                {
                    return Result.Failure("Property not found.");
                }

                context.Entry(existingProperty).CurrentValues.SetValues(property);
                await context.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }


        public async Task<Result> DeleteAsync(Guid id)
        {
            var property = await context.Properties.FirstOrDefaultAsync(x => x.Id == id);
            if (property == null)
            {
                return Result.Failure("Property not found.");
            }

            context.Properties.Remove(property);
            await context.SaveChangesAsync();
            return Result.Success();
        }
    }
}
