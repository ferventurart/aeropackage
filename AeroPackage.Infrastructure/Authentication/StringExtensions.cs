using System;
namespace AeroPackage.Infrastructure.Authentication;

public static class StringExtensions
{
    public static string ToBase64UrlString(this string value)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(value);
        var base64 = Convert.ToBase64String(bytes);
        var base64Url = base64.TrimEnd('=').Replace('+', '-').Replace('/', '_');
        return base64Url;
    }
}

