namespace common.data {
    public interface IStore<T> {
        T Load(string file);
        bool Save(T t, string file);
    }
}