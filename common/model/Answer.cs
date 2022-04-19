using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace common.model {
    public class Answer : ICloneable {
        private string _text;
        private bool _isCorrect;

        public string Text {
            get => _text;
            set {
                _text = value;
                HasChanged = true;
            }
        }

        public bool IsCorrect {
            get => _isCorrect;
            set {
                _isCorrect = value;
                HasChanged = true;
            }
        }

        [XmlIgnore] [JsonIgnore] public bool HasChanged { get; set; }

        public Answer() {
        }

        public Answer(string text, bool isCorrect) {
            Text = text;
            IsCorrect = isCorrect;
        }

        public override bool Equals(object obj) {
            if (obj is not Answer o) return false;
            return Text == o.Text && IsCorrect == o.IsCorrect;
        }

        public object Clone() {
            return new Answer(Text, IsCorrect);
        }

        public override string ToString() {
            return $"{Text} ({(IsCorrect ? "Poprawna" : "Niepoprawna")})";
        }
    }
}