using generator.model;

namespace generator.data {
    public interface IQuizStore {
        Quiz Load(string file);

        bool Save(Quiz quiz, string file);
    }
}