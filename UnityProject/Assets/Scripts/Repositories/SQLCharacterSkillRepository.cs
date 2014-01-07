//using System.Linq;
//using System.Collections.Generic;

//public class SQLCharacterSkillRepository : SQLRepository<CharacterSkill>, ICharacterSkillRepository
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
//            Name = "Character_ID",
//            Property = "CharacterID",
//            Type = DataType.Int
//        });
        
//        columns.Add(new Column() {
//            Name = "Skill_ID",
//            Property = "SkillID",
//            Type = DataType.Int
//        });
        
//        columns.Add(new Column() {
//            Name = "IsUnlocked",
//            Property = "IsUnlocked",
//            Type = DataType.Boolean
//        });
        
//        return columns;
//    }
    
//    protected override string TableName
//    {
//        get
//        {
//            return "CharacterSkills";
//        }
//    }

//    public List<CharacterSkill> GetAllFor(int characterID)
//    {
//        return GetBySQL(string.Format("WHERE Character_ID = {0}", characterID));
//    }



//}