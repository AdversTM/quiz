namespace common.data {
    public interface IStore<T> {
        T Load(string file);
        bool Save(T quiz, string file);
    }
}