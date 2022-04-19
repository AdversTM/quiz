using System;
using System.Diagnostics;
using System.IO;
using common.model;

namespace common.data {
    public class QuizStore : IStore<Quiz> {
        private readonly ISerializer<Quiz> _serializer;
        private readonly ICipher _cipher;

        public QuizStore(ISerializer<Quiz> serializer, ICipher cipher = null) {
            _serializer = serializer;
            _cipher = cipher;
        }

        public Quiz Load(string file) {
            if (!File.Exists(file)) return null;

            Quiz quiz = null;
            try {
                using var stream = File.OpenRead(file);
                if (_cipher == null)
                    quiz = _serializer.Deserialize(stream);
                else {
                    using var cs = _cipher.DecryptStream(stream);
                    quiz = _serializer.Deserialize(cs);
                }
            }
            catch (Exception e) {
                Debug.WriteLine(e.ToString());
            }

            return quiz;
        }

        public bool Save(Quiz quiz, string file) {
            using var stream = File.Create(file);
            try {
                if (_cipher == null)
                    _serializer.Serialize(stream, quiz);
                else {
                    using var cs = _cipher.EncryptStream(stream);
                    _serializer.Serialize(cs, quiz);
                }

                return true;
            }
            catch (Exception e) {
                Debug.WriteLine(e.ToString());
            }

            return false;
        }
    }
}