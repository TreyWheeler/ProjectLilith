using System;

public class PlayerItem : IRepositoryEntry
{       
    public int ID { get; set; }

    public int PlayerID { get; set; }

    public int ItemID { get; set; }
    
    public int? CharacterID { get; set; }
}


