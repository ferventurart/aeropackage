using AeroPackage.Application.Common.Models;
using AeroPackage.Application.Customers.Commands.CreateCustomer;
using AeroPackage.Application.Customers.Commands.DeleteCustomer;
using AeroPackage.Application.Customers.Commands.UpdateCustomer;
using AeroPackage.Application.Customers.Queries.GetCustomerById;
using AeroPackage.Application.Customers.Queries.GetCustomerByIdentification;
using AeroPackage.Application.Customers.Queries.GetCustomers;
using AeroPackage.Application.Customers.Queries.GetCustomersActiveByName;
using AeroPackage.Contracts.Customer;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeroPackage.Api.Controllers;

[Route("api/customers")]
[Authorize(Roles = "Admin,Executive")]
public class CustomersController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public CustomersController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    /// <summary>
    /// Get all Customers
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(int pageSize = 10, int pageNumber = 1, int? status = null)
    {
        var query = new GetCustomersQuery(pageSize, pageNumber, status);

        var getCustomerResult = await _mediator.Send(query);

        return getCustomerResult.Match(
            customers => Ok(_mapper.Map<PaginatedResult<CustomerResponse>>(customers)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Get Customer By National Identification
    /// </summary>
    /// <param name="identification"></param>
    /// <returns></returns>
    [HttpGet("identification/{identification}")]
    public async Task<IActionResult> GetByIdentification(string identification)
    {
        var query = new GetCustomerByIdentificationQuery(identification);

        var getCustomerResult = await _mediator.Send(query);

        return getCustomerResult.Match(
            customer => Ok(_mapper.Map<CustomerResponse>(customer)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Get Customers Active by Name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("active/{name}")]
    public async Task<IActionResult> GetActivesByName(string name)
    {
        var query = new GetCustomersActiveByNameQuery(name);

        var getCustomerResult = await _mediator.Send(query);

        return getCustomerResult.Match(
            customer => Ok(_mapper.Map<List<CustomerResponse>>(customer)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Get Customer by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetCustomerByIdQuery(id);

        var getCustomerResult = await _mediator.Send(query);

        return getCustomerResult.Match(
            customer => Ok(_mapper.Map<CustomerResponse>(customer)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Create Customer
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerRequest request)
    {
        var command = _mapper.Map<CreateCustomerCommand>((request));

        var createCustomerResult = await _mediator.Send(command);

        return createCustomerResult.Match(
            customer => Ok(_mapper.Map<CustomerResponse>(customer)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Update Customer
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateCustomerRequest request)
    {
        if (id != request.Id)
        {
            List<Error> errors = new();
            errors.Add(Error.Validation("CustomerId", "Url Id is not equal than Request Id."));
            return Problem(errors);
        }

        var command = _mapper.Map<UpdateCustomerCommand>((request));

        var updateCustomerResult = await _mediator.Send(command);

        return updateCustomerResult.Match(
            customer => Ok(_mapper.Map<CustomerResponse>(customer)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Delete Customer
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = _mapper.Map<DeleteCustomerCommand>((new DeleteCustomerRequest(id)));

        var deleteCustomerResult = await _mediator.Send(command);

        return deleteCustomerResult.Match(
            customer => Ok(_mapper.Map<CustomerResponse>(customer)),
            errors => Problem(errors));
    }
}

