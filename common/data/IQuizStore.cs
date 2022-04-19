using common.model;

namespace common.data {
    public interface IQuizStore {
        Quiz Load(string file);

        bool Save(Quiz quiz, string file);
    }
}