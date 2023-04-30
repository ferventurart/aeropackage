using System;
namespace AeroPackage.WebApp.Model.Package;

public class DropPackageItemDto
{
    public string OwnTrackingNumber { get; set; }
    public string Description { get; set; }
    public int QuantityArticles { get; set; }
    public string Store { get; set; }
    public string Status { get; set; }
}

