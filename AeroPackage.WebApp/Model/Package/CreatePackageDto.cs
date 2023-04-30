using System;
using Microsoft.AspNetCore.Components.Forms;
namespace AeroPackage.WebApp.Model.Package;

public class CreatePackageDto
{
	public Guid UserId { get; set; }
    public Guid CustomerId { get; set; }
    public string Store { get; set; }
    public string? Courier { get; set; }
    public string? CourierTrackingNumber { get; set; }
    public decimal Weight { get; set; }
    public int QuantityArticles { get; set; }
    public string Description { get; set; }
    public decimal DeclaredValue { get; set; }
    public decimal? TaxValue { get; set; }
    public List<IBrowserFile> Attachments { get; set; }
}

