using AeroPackage.Application.Common.Models;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Domain.UserAggregate;
using AeroPackage.Domain.UserAggregate.ValueObjects;

namespace AeroPackage.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    bool Exists(UserId userId);
    User? GetUserById(UserId userId);
    User? GetUserByEmail(string email);
    Task<PaginatedResult<User>> GetAll(int pageSize, int pageNumber);
    void Add(User user);
    void Update(User user);
    void Delete(User user);
}