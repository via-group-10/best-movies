using System.Security.Cryptography;
using System.Text;

namespace BestMovies.Api.Utils
{

    public class HashedKey
    {
        public HashedKey(HashAlgorithm hashAlgorithm, string key)
        {
            KeyData = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(key));
            KeyHexString = string.Join(string.Empty, KeyData.Select(h => string.Format("{0:X2}", h))).ToLower();
        }

        public string KeyHexString { get; set; }

        public byte[] KeyData { get; set; }
    }
}
