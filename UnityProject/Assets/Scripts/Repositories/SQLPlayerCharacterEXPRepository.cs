using System.Linq;
using System.Collections.Generic;

public class SQLPlayerCharacterEXPRepository : SQLRepository<PlayerCharacterEXP>, IPlayerCharacterEXPRepository
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
            Name = "PlayerCharacter_ID",
            Property = "PlayerCharacterID",
            Type = DataType.Int
        });
        
        columns.Add(new Column() {
            Name = "EXPType_ID",
            Property = "EXPTypeID",
            Type = DataType.Int
        });
        
        columns.Add(new Column() {
            Name = "Amount",
            Property = "Amount",
            Type = DataType.Int
        });
        
        return columns;
    }
    
    protected override string TableName
    {
        get
        {
            return "PlayerCharacterEXP";
        }
    }
}