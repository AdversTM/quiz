using System;
using System.Linq;

namespace quiz.model {
    public class Question : IComparable<Question> {
        public string Text { get; set; }

        public Answer[] Answers { get; set; }

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

        public int CompareTo(Question other) {
            return string.CompareOrdinal(Text, other.Text);
        }

        public override string ToString() {
            return Text;
        }
    }
}