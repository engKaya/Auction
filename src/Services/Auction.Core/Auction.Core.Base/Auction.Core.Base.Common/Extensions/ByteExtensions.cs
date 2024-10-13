using System.Text;

namespace Auction.Core.Base.Common.Extensions
{
    public static class ByteExtensions
    {
        public static string ToHexString(this byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
