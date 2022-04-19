using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using common.model;

namespace common.data {
    public class JsonQuizStore : IQuizStore {
        private readonly ICipher _cipher;

        public JsonQuizStore(ICipher cipher = null) {
            _cipher = cipher;
        }

        public Quiz Load(string file) {
            if (!File.Exists(file)) return null;

            Quiz quiz = null;
            try {
                using var stream = File.OpenRead(file);
                if (_cipher == null)
                    quiz = JsonSerializer.Deserialize<Quiz>(stream);
                else {
                    using var cs = _cipher.DecryptStream(stream);
                    quiz = JsonSerializer.Deserialize<Quiz>(cs);
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
                    JsonSerializer.Serialize(stream, quiz);
                else {
                    using var cs = _cipher.EncryptStream(stream);
                    JsonSerializer.Serialize(cs, quiz);
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