using System.Linq;
using System.Collections.Generic;

public class SQLPlayerItemRepository : SQLRepository<PlayerItem>, IPlayerItemRepository
{
    protected override List<Column> LoadColumns()
    {
        List<Column> columns = new List<Column>();
        
        columns.Add(new Column() {
            IsPrimaryKey = true,
            Name = "ID",
            Property = "ID",
            Type = DataType.Int
        });
        
        columns.Add(new Column() {
            Name = "Player_ID",
            Property = "PlayerID",
            Type = DataType.Int
        });
        
        columns.Add(new Column() {
            Name = "Item_ID",
            Property = "ItemID",
            Type = DataType.Int
        });
        
           columns.Add(new Column() {
            Name = "Character_ID",
            Property = "CharacterID",
            Type = DataType.Int
        });
        
        return columns;
    }
    
    protected override string TableName
    {
        get
        {
            return "PlayerItems";
        }
    }
}