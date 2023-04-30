using System.Data;
using AeroPackage.Application.Common.Models;
using AeroPackage.Application.Users.Commands.CreateUser;
using AeroPackage.Application.Users.Commands.DeleteUser;
using AeroPackage.Application.Users.Commands.UpdateUser;
using AeroPackage.Application.Users.Queries.GetUserById;
using AeroPackage.Application.Users.Queries.GetUsers;
using AeroPackage.Contracts.Authentication;
using AeroPackage.Contracts.User;
using AeroPackage.Contracts.User;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeroPackage.Api.Controllers;

[Route("api/users")]
[Authorize(Roles = "Admin")]
public class UsersController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public UsersController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    /// <summary>
    /// Get all Users
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(int pageSize = 10, int pageNumber = 1)
    {
        var query = new GetUsersQuery(pageSize, pageNumber);

        var getUserResult = await _mediator.Send(query);

        return getUserResult.Match(
            users => Ok(_mapper.Map<PaginatedResult<UserResponse>>(users)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Get User by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetUserByIdQuery(id);

        var getUserResult = await _mediator.Send(query);

        return getUserResult.Match(
            user => Ok(_mapper.Map<UserResponse>(user)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Create User
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserRequest request)
    {
        var command = _mapper.Map<CreateUserCommand>((request));

        var createUserResult = await _mediator.Send(command);

        return createUserResult.Match(
            user => Ok(_mapper.Map<UserResponse>(user)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Update User
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateUserRequest request)
    {
        if (id != request.Id)
        {
            List<Error> errors = new();
            errors.Add(Error.Validation("UserId", "Url Id is not equal than Request Id."));
            return Problem(errors);
        }

        var command = _mapper.Map<UpdateUserCommand>((request));

        var updateUserResult = await _mediator.Send(command);

        return updateUserResult.Match(
            user => Ok(_mapper.Map<UserResponse>(user)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Delete User
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = _mapper.Map<DeleteUserCommand>((new DeleteUserRequest(id)));

        var deleteUserResult = await _mediator.Send(command);

        return deleteUserResult.Match(
            user => Ok(_mapper.Map<UserResponse>(user)),
            errors => Problem(errors));
    }
}

