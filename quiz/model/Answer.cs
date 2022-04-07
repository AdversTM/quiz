namespace quiz.model {
    public class Answer {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

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

        public override string ToString() {
            return $"{Text} ({(IsCorrect ? "Poprawna" : "Niepoprawna")})";
        }
    }
}