public interface IRepository<T>  {
     T GetByID();
    void Save(T instance);
}
