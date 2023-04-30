﻿using System;
using Microsoft.AspNetCore.Http;

namespace AeroPackage.Contracts.Package;

public record UpdatePackageRequest(
    int Id,
    string OwnTrackingNumber,
    Guid UserId,
    Guid CustomerId,
    string Store,
    string? Courier,
    string? CourierTrackingNumber,
    decimal Weight,
    int QuantityArticles,
    string Description,
    decimal DeclaredValue,
    List<IFormFile>? Attachments);