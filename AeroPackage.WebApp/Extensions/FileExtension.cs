using System;
namespace AeroPackage.WebApp.Extensions;

public static class FileExtension
{
    public static double ConvertBytesToMegabytes(long bytes)
    {
        return Math.Round((bytes / 1024f) / 1024f, 2);
    }
}

