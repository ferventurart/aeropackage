using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Common.Models;
using AeroPackage.Domain.UserAggregate;
using AeroPackage.Domain.UserAggregate.ValueObjects;
using AeroPackage.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AeroPackage.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly AeroPackageDbContext _dbContext;

    public UserRepository(AeroPackageDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(User user)
    {
        _dbContext.Add(user);
        _dbContext.SaveChanges();
    }

    public bool Exists(UserId userId) => _dbContext.Users.Any(u => u.Id == userId);

    public async Task<PaginatedResult<User>> GetAll(int pageSize, int pageNumber)
    {
        return await _dbContext.Users
                       .ToPaginatedListAsync(pageNumber, pageSize);
    }

    public User? GetUserByEmail(string email) => _dbContext.Users.SingleOrDefault(u => u.Email == email);

    public User? GetUserById(UserId userId) => _dbContext.Users.SingleOrDefault(u => u.Id == userId);

    public void Update(User user)
    {
        _dbContext.Update(user);
        _dbContext.SaveChanges();
    }

    public void Delete(User user)
    {
        _dbContext.Remove(user);
        _dbContext.SaveChanges();
    }
}