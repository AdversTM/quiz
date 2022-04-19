using System.IO;
using System.Text.Json;
using common.model;

namespace common.data {
    public class JsonQuizSerializer : ISerializer<Quiz> {
        
        public Quiz Deserialize(Stream stream) {
            return JsonSerializer.Deserialize<Quiz>(stream);
        }

        public void Serialize(Stream stream, Quiz quiz) {
            JsonSerializer.Serialize(stream, quiz);
        }
    }
}