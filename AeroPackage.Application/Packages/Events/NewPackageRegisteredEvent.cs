using MediatR;

namespace AeroPackage.Application.Packages.Events;

public class NewPackageRegisteredEvent : INotification
{
	public Guid CustomerId { get; set; }
	public int PackageId { get; set; }
}

