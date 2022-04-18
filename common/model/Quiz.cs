using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using common.util;
using common.viewmodel;

namespace common.model {
    public class Quiz : ViewModelBase, ICloneable {
        private long _time;
        public string Name { get; set; }
        public FullyObservableCollection<Question> Questions { get; set; }

        public long Time {
            get => _time;
            set {
                _time = value;
                OnPropertyChanged();
            }
        }

        public Quiz() : this("", new List<Question>(), 60) {
        }

        public Quiz(string name, IEnumerable<Question> questions, long time) {
            Name = name;
            Questions = new FullyObservableCollection<Question>(questions);
            Time = time;
        }

        public string GetFileName() {
            return Regex.Replace(Name.Replace(" ", "_"), "\\/:*?\"<>|", "");
        }

        public override bool Equals(object obj) {
            if (obj is not Quiz o) return false;
            return Questions.SequenceEqual(o.Questions) && Time == o.Time;
        }

        public object Clone() {
            return new Quiz(Name, Questions.Clone(), Time);
        }

        public override string ToString() {
            return $"{Name} ({Questions.Count} pytań, {TextUtil.TimeToStr(Time)})";
        }
    }
}