using System.Text;

namespace MasterClass.Core.Tools
{
    public static class EncodingExtensions
    {
        public static byte[] ToBytes(this string input)
        {
            return Encoding.UTF8.GetBytes(input);
        }
    }
}