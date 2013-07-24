using System.Linq;

public class SQLPlayerRepository : SQLRepository<Player>, IPlayerRepository
{
    public Player GetByName (string name)
    {
        return this.GetBySQL("col = " + name).FirstOrDefault();
    }
}