using AeroPackage.Application.Common.Models;
using AeroPackage.Application.Services.Commands.CreateService;
using AeroPackage.Application.Services.Commands.DeleteService;
using AeroPackage.Application.Services.Commands.UpdateService;
using AeroPackage.Application.Services.Queries.GetServiceById;
using AeroPackage.Application.Services.Queries.GetServices;
using AeroPackage.Application.Services.Queries.GetServicesActiveByName;
using AeroPackage.Contracts.Service;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeroPackage.Api.Controllers;

[Route("api/services")]
[Authorize(Roles = "Admin,Executive")]
public class ServicesController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public ServicesController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    /// <summary>
    /// Get all Services
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(int pageSize = 10, int pageNumber = 1, int? status = null)
    {
        var query = new GetServicesQuery(pageSize, pageNumber, status);

        var getServiceResult = await _mediator.Send(query);

        return getServiceResult.Match(
            services => Ok(_mapper.Map<PaginatedResult<ServiceResponse>>(services)),
            errors => Problem(errors));
    }


    /// <summary>
    /// Get Services Active by Name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("active/{name}")]
    public async Task<IActionResult> GetActivesByName(string name)
    {
        var query = new GetServicesActiveByNameQuery(name);

        var getServiceResult = await _mediator.Send(query);

        return getServiceResult.Match(
            customer => Ok(_mapper.Map<List<ServiceResponse>>(customer)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Get Service by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetServiceByIdQuery(id);

        var getServiceResult = await _mediator.Send(query);

        return getServiceResult.Match(
            customer => Ok(_mapper.Map<ServiceResponse>(customer)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Create Service
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateServiceRequest request)
    {
        var command = _mapper.Map<CreateServiceCommand>((request));

        var createServiceResult = await _mediator.Send(command);

        return createServiceResult.Match(
            customer => Ok(_mapper.Map<ServiceResponse>(customer)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Update Service
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateServiceRequest request)
    {
        if (id != request.Id)
        {
            List<Error> errors = new();
            errors.Add(Error.Validation("ServiceId", "Url Id is not equal than Request Id."));
            return Problem(errors);
        }

        var command = _mapper.Map<UpdateServiceCommand>((request));

        var updateServiceResult = await _mediator.Send(command);

        return updateServiceResult.Match(
            customer => Ok(_mapper.Map<ServiceResponse>(customer)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Delete Service
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = _mapper.Map<DeleteServiceCommand>((new DeleteServiceRequest(id)));

        var deleteServiceResult = await _mediator.Send(command);

        return deleteServiceResult.Match(
            customer => Ok(_mapper.Map<ServiceResponse>(customer)),
            errors => Problem(errors));
    }
}

