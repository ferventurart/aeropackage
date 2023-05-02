using System;
namespace AeroPackage.WebApp.Model.Common;

public class DownloadedFile
{
    public string FileName { get; set; }
    public byte[] Content { get; set; }
    public string ContentType { get; set; }
}

