using System;
using AeroPackage.Contracts.User;
using AeroPackage.WebApp.Model;
using AeroPackage.WebApp.Model.User;

namespace AeroPackage.WebApp.Services.Interfaces;

public interface IUserService
{
    Task<PaginatedResult<UserResponse>> GetAllUsers(int pageSize = 10, int pageNumber = 1);
    Task<UserResponse> GetById(Guid Id);
    Task<UserResponse> Create(CreateUserDto user);
    Task<UserResponse> Update(UpdateUserDto user);
    Task<UserResponse> UpdateProfile(UpdateUserProfileDto user);
    Task<UserResponse> Delete(Guid Id);
}

