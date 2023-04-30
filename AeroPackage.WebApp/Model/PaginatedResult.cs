using System;
namespace AeroPackage.WebApp.Model;

public class PaginatedResult<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public List<T> Results { get; set; }
}
