using System;
namespace AeroPackage.WebApp.Model.Package;

public class PackageDto
{
    public int Id { get; set; }
    public string OwnTrackingNumber { get; set; }
    public Guid UserId { get; set; }
    public Guid CustomerId { get; set; }
    public string Consignee { get; set; }
    public string Store { get; set; }
    public string? Courier { get; set; }
    public string? CourierTrackingNumber { get; set; }
    public decimal Weight { get; set; }
    public string Description { get; set; }
    public decimal DeclaredValue { get; set; }
    public decimal? TaxValue { get; set; }
    public string CreatedBy { get; set; }
    public List<string> Attachments { get; set; }
    public List<PackageHistoryDto> PackageHistories { get; set; }
}

