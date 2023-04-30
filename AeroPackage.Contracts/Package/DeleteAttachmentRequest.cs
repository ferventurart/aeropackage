using System;
namespace AeroPackage.Contracts.Package;

public record DeleteAttachmentRequest(int Id, string OwnTrackingNumber, string FileName);

