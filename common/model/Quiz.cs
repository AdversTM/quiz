using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using common.util;
using common.viewmodel;

namespace common.model {
    public class Quiz : ViewModelBase, ICloneable {
        private long _time;
        private string _name;

        private bool _hasChanged;

        public FullyObservableCollection<Question> Questions { get; set; }

        public long Time {
            get => _time;
            set {
                _time = value;
                OnPropertyChanged();
            }
        }

        public string Name {
            get => _name;
            set {
                _name = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public bool HasChanged {
            get { return _hasChanged || Questions.Any(a => a.HasChanged); }
            set {
                _hasChanged = value;
                if (value) return;

                foreach (var a in Questions)
                    a.HasChanged = false;
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
            return Regex.Replace(Name.Replace(" ", "_"), "[\\/:*?\"<>|]", "");
        }

        public override bool Equals(object obj) {
            if (obj is not Quiz o) return false;
            return Name == o.Name && Questions.SequenceEqual(o.Questions) && Time == o.Time;
        }

        public object Clone() {
            return new Quiz(Name, Questions.Clone(), Time);
        }

        public override string ToString() {
            return $"{Name} ({Questions.Count} pytań, {TextUtil.TimeToStr(Time)})";
        }

        public override void OnPropertyChanged([CallerMemberName] string name = null) {
            base.OnPropertyChanged(name);
            HasChanged = true;
        }
    }
}