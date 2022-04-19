using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace common.data {
    public class AESCipher : ICipher {
        private byte[] Key { get; set; }
        private byte[] IV { get; set; }

        public AESCipher(string key, string iv) {
            Key = Encoding.UTF8.GetBytes(key);
            IV = Encoding.UTF8.GetBytes(iv);
        }

        public Stream EncryptStream(Stream stream) {
            using var rm = InitAlgorithm();
            var encryptor = rm.CreateEncryptor(rm.Key, rm.IV);
            return new CryptoStream(stream, encryptor, CryptoStreamMode.Write);
        }

        public Stream DecryptStream(Stream stream) {
            using var rm = InitAlgorithm();
            var decryptor = rm.CreateDecryptor(rm.Key, rm.IV);
            return new CryptoStream(stream, decryptor, CryptoStreamMode.Read);
        }

        private RijndaelManaged InitAlgorithm() {
            return new RijndaelManaged {
                Key = Key,
                IV = IV,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.Zeros
            };
        }
    }
}