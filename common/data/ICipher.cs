using System.IO;

namespace common.data {
    public interface ICipher {
        Stream DecryptStream(Stream stream);
        Stream EncryptStream(Stream stream);
    }
}