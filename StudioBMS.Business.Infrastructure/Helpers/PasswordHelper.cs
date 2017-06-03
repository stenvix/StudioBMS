using System;
using System.Linq;

namespace StudioBMS.Business.Infrastructure.Helpers
{
    public static class PasswordHelper
    {
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private const int Length = 8;
        private static readonly Random Random = new Random();

        public static string GetRandomPassword()
        {
            return new string(Enumerable.Repeat(Chars, Length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}