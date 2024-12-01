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

        public async Task<PaginatedList<Property>> GetPropertiesAsync(int pageNumber, int pageSize, Dictionary<string, string>? filters)
        {
            var query = context.Properties.AsQueryable();

            // Apply filters dynamically using a switch
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    switch (filter.Key.ToLower())
                    {
                        case "title":
                            query = query.Where(p => p.Title.Contains(filter.Value));
                            break;
                        case "description":
                            query = query.Where(p => p.Description.Contains(filter.Value));
                            break;
                        case "price_min":
                            if (decimal.TryParse(filter.Value, out var priceMin))
                            {
                                query = query.Where(p => p.Price >= priceMin);
                            }
                            break;
                        case "price_max":
                            if (decimal.TryParse(filter.Value, out var priceMax))
                            {
                                query = query.Where(p => p.Price <= priceMax);
                            }
                            break;
                        case "rooms":
                            if (int.TryParse(filter.Value, out var rooms))
                            {
                                query = query.Where(p => p.Rooms == rooms);
                            }
                            break;
                        case "bathrooms":
                            if (int.TryParse(filter.Value, out var bathrooms))
                            {
                                query = query.Where(p => p.Bathrooms == bathrooms);
                            }
                            break;
                        case "constructionyear":
                            if (int.TryParse(filter.Value, out var year))
                            {
                                query = query.Where(p => p.ConstructionYear == year);
                            }
                            break;
                        case "pagenumber":
                        case "pagesize":
                            // Skip these keys
                            break;
                        // Add more cases as needed for additional filters
                        default:
                            throw new ArgumentException($"Filter key '{filter.Key}' is not supported.");
                    }
                }
            }

            // Apply pagination
            var totalItems = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return new PaginatedList<Property>(items, totalItems, pageNumber, pageSize);
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
