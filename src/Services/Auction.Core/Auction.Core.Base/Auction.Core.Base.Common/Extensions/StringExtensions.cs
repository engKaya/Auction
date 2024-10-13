using System.Text.RegularExpressions;

namespace Auction.Core.Base.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string value) { return value.Substring(0, 1).ToLower() + value.Substring(1); }
        public static string RemoveTurkishChars(this string value)
        {

            value = value.Trim();
            if (value.Length > 0)
                value = value.Replace("İ", "I").Replace("ı", "i").Replace("Ğ", "G").Replace("ğ", "g").Replace("Ü", "U").Replace("ü", "u").Replace("Ş", "S").Replace("ş", "s").Replace("Ö", "O").Replace("ö", "o").Replace("Ç", "C").Replace("ç", "c");

            return value;
        }
        public static string ReturnRegExpressionResult(this string value, string regex)
        {
            return value.Trim().Length > 0 ? Regex.Match(value, regex).Value : value;
        }

        public static byte[] StringToByteArray(this string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
