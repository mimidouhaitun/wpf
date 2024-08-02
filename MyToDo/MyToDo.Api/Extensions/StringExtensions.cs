using System.Security.Cryptography;
using System.Text;

namespace MyToDo.Api.Extensions
{
    public static class StringExtensions
    {
        public static string GetMd5(this string value) 
        {
            if(string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));
            var md5ByteArr=MD5.Create().ComputeHash(Encoding.Default.GetBytes(value));
            return Convert.ToBase64String(md5ByteArr); 
        }
    }
}
