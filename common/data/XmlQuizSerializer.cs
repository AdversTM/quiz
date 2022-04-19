using System.IO;
using System.Xml.Serialization;
using common.model;

namespace common.data {
    public class XmlQuizSerializer : ISerializer<Quiz> {
        private static readonly XmlSerializer Serializer = new(typeof(Quiz));

        public Quiz Deserialize(Stream stream) {
            return Serializer.Deserialize(stream) as Quiz;
        }

        public void Serialize(Stream stream, Quiz quiz) {
            Serializer.Serialize(stream, quiz);
        }
    }
}