using System;

public class PlayerCharacterEXP : IRepositoryEntry
{       
    public int ID { get; set; }

    public int PlayerCharacterID { get; set; }

    public int EXPTypeID { get; set; }
    
    public int Amount { get; set; }
}


