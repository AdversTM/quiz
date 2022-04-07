using System.Collections.Generic;
using System.Linq;
using quiz.util;

namespace quiz.model {
    public class Quiz {
        public string Name { get; set; }
        public List<Question> Questions { get; set; }
        public long Time { get; set; }

        public Quiz() : this("", new List<Question>(), 60) {
        }

        public Quiz(string name, IEnumerable<Question> questions, long time) {
            Name = name;
            Questions = new List<Question>(questions);
            Time = time;
        }

        public override bool Equals(object obj) {
            if (obj is not Quiz o) return false;
            return Questions.SequenceEqual(o.Questions) && Time == o.Time;
        }

        public override string ToString() {
            return $"{Name} ({Questions.Count} pytań, {TextUtil.TimeToStr(Time)})";
        }
    }
}