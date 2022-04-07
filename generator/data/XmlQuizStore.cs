using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using generator.model;

namespace generator.data {
    public class XmlQuizStore : IQuizStore {
        private static readonly XmlSerializer QuizSerializer = new(typeof(Quiz));

        private readonly ICipher _cipher;

        public XmlQuizStore(ICipher cipher = null) {
            _cipher = cipher;
        }

        public Quiz Load(string file) {
            if (!File.Exists(file)) return null;

            Quiz quiz = null;
            try {
                using var stream = File.OpenRead(file);
                if (_cipher == null)
                    quiz = QuizSerializer.Deserialize(stream) as Quiz;
                else {
                    using var cs = _cipher.DecryptStream(stream);
                    quiz = QuizSerializer.Deserialize(cs) as Quiz;
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
                    QuizSerializer.Serialize(stream, quiz);
                else {
                    using var cs = _cipher.EncryptStream(stream);
                    QuizSerializer.Serialize(cs, quiz);
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