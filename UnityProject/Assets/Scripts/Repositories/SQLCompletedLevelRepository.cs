//using System.Linq;
//using System.Collections.Generic;

//public class SQLCompletedLevelRepository : SQLRepository<CompletedLevel>, ICompletedLevelRepository
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
//            Name = "Player_ID",
//            Property = "PlayerID",
//            Type = DataType.Int
//        });
        
//        columns.Add(new Column() {
//            Name = "Level_ID",
//            Property = "LevelID",
//            Type = DataType.Int
//        });
        
//        return columns;
//    }
    
//    protected override string TableName
//    {
//        get
//        {
//            return "CompletedLevels";
//        }
//    }
//}