using System;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace AeroPackage.WebApp.Providers.Interfaces;

public interface IBrowserFileProvider
{
	Task<List<IFormFile>> ConvertFromBrowserToApiFile(IList<IBrowserFile> browserFiles);
}

