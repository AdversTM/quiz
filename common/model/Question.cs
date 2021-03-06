using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using common.util;
using common.viewmodel;

namespace common.model {
    public class Question : ViewModelBase, IComparable<Question>, ICloneable {
        private string _text;
        private Answer[] _answers;

        private bool _hasChanged;

        public string Text {
            get => _text;
            set {
                _text = value;
                OnPropertyChanged();
            }
        }

        public Answer[] Answers {
            get => _answers;
            set {
                _answers = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public bool HasChanged {
            get { return _hasChanged || _answers.Any(a => a.HasChanged); }
            set {
                _hasChanged = value;
                if (value) return;

                foreach (var a in _answers)
                    a.HasChanged = false;
            }
        }

        public int CorrectAnswers => _answers.Count(e => e.IsCorrect);

        public Question() : this("", new Answer[4]) {
            for (var i = 0; i < Answers.Length; i++)
                Answers[i] = new Answer();
        }

        public Question(string text, Answer[] answers) {
            Text = text;
            Answers = answers;
        }

        public override bool Equals(object obj) {
            if (obj is not Question o) return false;
            return Text == o.Text && Answers.SequenceEqual(o.Answers);
        }

        public object Clone() {
            return new Question(Text, Answers.CloneArray());
        }

        public int CompareTo(Question other) {
            return string.CompareOrdinal(Text, other.Text);
        }

        public override string ToString() {
            return Text;
        }

        public override void OnPropertyChanged([CallerMemberName] string name = null) {
            base.OnPropertyChanged(name);
            HasChanged = true;
        }
    }
}