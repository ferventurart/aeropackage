using System;
namespace AeroPackage.WebApp.Extensions;

public static class PasswordExtension
{
	public static string GeneratePassword(int length)
	{
        string password = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length)
              .Select(s => s[new Random().Next(s.Length)]).ToArray());

        return password;
    }
}

