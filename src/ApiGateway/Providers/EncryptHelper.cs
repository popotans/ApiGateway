using System.Linq;
using System.Text;

namespace ApiGateway.Providers
{
    public class EncryptHelper
    {
        public static string SHA1Encrypt(string source)
        {
            byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
            using (System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create())
            {
                var targetBytes = sha1.ComputeHash(sourceBytes);

                return string.Join("", targetBytes.Select(i => i.ToString("x2")));
            }
        }
    }
}
