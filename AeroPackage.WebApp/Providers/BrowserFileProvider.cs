using System;
using AeroPackage.WebApp.Providers.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace AeroPackage.WebApp.Providers;

public class BrowserFileProvider : IBrowserFileProvider
{
    public async Task<List<IFormFile>> ConvertFromBrowserToApiFile(IList<IBrowserFile> browserFiles)
    {
        List<IFormFile> files = new();
        foreach (var browserFile in browserFiles)
        {
            using var ms = new MemoryStream();
            await browserFile.OpenReadStream().CopyToAsync(ms);

            ms.Seek(0, SeekOrigin.Begin); // Volver al inicio del MemoryStream
            var msCopy = new MemoryStream();
            await ms.CopyToAsync(msCopy);

            files.Add(new FormFile(msCopy, 0, browserFile.Size, browserFile.Name, browserFile.Name));
        }

        return files;
    }
}

