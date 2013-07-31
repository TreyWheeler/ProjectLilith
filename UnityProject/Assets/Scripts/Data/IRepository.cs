public interface IRepository<T>
where T : IRepositoryEntry
{
    T GetByID(int id);

    void Save(T instance);
}
