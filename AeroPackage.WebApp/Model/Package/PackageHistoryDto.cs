﻿using System;
namespace AeroPackage.WebApp.Model.Package;

public class PackageHistoryDto
{
	public Guid Id { get; set; }
	public DateTime DateMovement { get; set; }
	public string Status { get; set; }
}

