using System;

public class CharacterSkill : IRepositoryEntry
{       
    public int ID { get; set; }

    public int CharacterID { get; set; }

    public int SkillID { get; set; }

    public bool IsUnlocked { get; set; }
}


