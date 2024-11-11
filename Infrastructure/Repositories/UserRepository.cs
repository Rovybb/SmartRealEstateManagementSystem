using Domain.Entities;
using Domain.Repositories;
using Domain.Utils;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<Result<User>> GetByIdAsync(Guid id)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return Result<User>.Failure("User not found");
                }
                return Result<User>.Success(user);
            }
            catch (Exception ex)
            {
                return Result<User>.Failure(ex.Message);
            }
        }

        public async Task<Result<Guid>> CreateAsync(User user)
        {
            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                return Result<Guid>.Success(user.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(ex.Message);
            }
        }

        public async Task<Result<Guid>> UpdateAsync(User user)
        {
            try
            {
                var existingUser = await context.Users.FindAsync(user.Id);
                if (existingUser == null)
                {
                    return Result<Guid>.Failure("User not found.");
                }

                context.Entry(existingUser).CurrentValues.SetValues(user);
                await context.SaveChangesAsync();
                return Result<Guid>.Success(existingUser.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(ex.Message);
            }
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return Result.Failure("User not found.");
                }

                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
