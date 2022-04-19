using System.IO;

namespace common.data {
    public interface ISerializer<T> {
        T Deserialize(Stream stream);
        void Serialize(Stream stream, T quiz);
    }
}