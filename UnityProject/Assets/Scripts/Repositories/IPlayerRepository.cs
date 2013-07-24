
public interface IPlayerRepository : IRepository<Player>
{
    // Shity Example
    Player GetByName(string name);        
}