using System;
namespace AeroPackage.WebApp.Model.Service;

public class UpdateServiceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Rate { get; set; }
    public int Status { get; set; }
}

