//using System.Linq;
//using System.Collections.Generic;

//public class SQLPlayerRepository : SQLRepository<Player>, IPlayerRepository
//{
//    protected override List<Column> LoadColumns()
//    {
//        List<Column> columns = new List<Column>();
        
//        columns.Add(new Column() {
//            IsPrimaryKey = true,
//            Name = "ID",
//            Property = "ID",
//            Type = DataType.Int
//        });
        
//        columns.Add(new Column() {
//            Name = "Name",
//            Property = "Name",
//            Type = DataType.String
//        });
        
//        columns.Add(new Column() {
//            Name = "Gold",
//            Property = "Gold",
//            Type = DataType.Int
//        });
        
//        return columns;
//    }
    
//    protected override string TableName
//    {
//        get
//        {
//            return "Players";
//        }
//    }
//}