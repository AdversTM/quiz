using System.IO;

namespace generator.data {
    public interface ICipher {
        Stream DecryptStream(Stream stream);
        Stream EncryptStream(Stream stream);
    }
}