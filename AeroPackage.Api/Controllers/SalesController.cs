using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AeroPackage.Api.Handlers.Interfaces;
using AeroPackage.Application.Packages.Commands.CreatePackage;
using AeroPackage.Application.Sales.Commands.CreateInvoice;
using AeroPackage.Contracts.Package;
using AeroPackage.Contracts.Sale;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AeroPackage.Api.Controllers;

[Route("api/sales")]
[Authorize(Roles = "Admin,Executive")]
public class SalesController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;
    private readonly IFileHandler _fileHandler;

    public SalesController(IMapper mapper, ISender mediator, IFileHandler fileHandler)
    {
        _mapper = mapper;
        _mediator = mediator;
        _fileHandler = fileHandler;
    }

    /// <summary>
    /// Create Sale
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateInvoiceRequest request)
    {
        var command = _mapper.Map<CreateInvoiceCommand>((request));

        var createInvoiceResult = await _mediator.Send(command);

        return createInvoiceResult.Match(
            invoice => Ok(_mapper.Map<SaleResponse>(invoice)),
            errors => Problem(errors));
    }
}

