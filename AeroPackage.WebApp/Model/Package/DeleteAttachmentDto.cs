using System;
namespace AeroPackage.WebApp.Model.Package;

public class DeleteAttachmentDto
{
	public int Id { get; set; }
	public string OwnTrackingNumber { get; set; }
	public string FileName { get; set; }
}

