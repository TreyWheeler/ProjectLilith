using System.Collections.Generic;

public interface IRepository<T>
where T : IRepositoryEntry
{
    T GetByID(int id);
    
    List<T> GetAll();
    
    void DeleteAll();

    void Save(T instance);
}
