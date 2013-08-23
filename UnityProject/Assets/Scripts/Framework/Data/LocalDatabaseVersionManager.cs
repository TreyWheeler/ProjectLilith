using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class LocalDatabaseVersionManager
{
    private const string DBVersionKey = "DBVersion";
    private const string ConnectionString = "URI=file:Daemon.db";
    
    public void EnsureUpToDate()
    {
        int targetVersion = GetTargetVersion();
        for(int dbVersion = GetDBVersion(); dbVersion < targetVersion; dbVersion++)
        {
            UpgradeDB(dbVersion);            
        }
    }
    
    public int GetTargetVersion()
    {        
        return 1;   
    }
    
    public int GetDBVersion()
    {
        if(PlayerPrefs.HasKey(DBVersionKey))
            return PlayerPrefs.GetInt(DBVersionKey);   
        
        return 0;
    }
    
    public void UpgradeDB(int dbVersion)
    {
        IDbConnection dbcon;
        dbcon = (IDbConnection)new SqliteConnection(ConnectionString);        
        dbcon.Open();
       
        IDbTransaction transaction = dbcon.BeginTransaction();
   
        try
        {
            switch(dbVersion)
            {
            case 0:
                using(IDbCommand dbcmd = dbcon.CreateCommand())
                {            
                    dbcmd.CommandText = @"
                CREATE TABLE Players 
                (
                    ID INTEGER NOT NULL,
                    Name varchar(32) NOT NULL,
                    Gold int NOT NULL,
                    PRIMARY KEY (ID)
                );

                CREATE TABLE PlayerCharacters 
                (
                    ID INTEGER NOT NULL,
                    Character_ID int NOT NULL,
                    Player_ID int NOT NULL,
                    PRIMARY KEY (ID),
                    CONSTRAINT fk_Players FOREIGN KEY (Player_ID) REFERENCES Players(ID) ON DELETE CASCADE
                );

                CREATE TABLE PlayerCharacterEXP 
                (
                    ID INTEGER NOT NULL,
                    PlayerCharacter_ID int NOT NULL,
                    EXPType_ID int NOT NULL,
                    Amount int NOT NULL,
                    PRIMARY KEY (ID),
                    CONSTRAINT fk_PlayerCharacters FOREIGN KEY (PlayerCharacter_ID) REFERENCES PlayerCharacters(ID) ON DELETE CASCADE
                );

                CREATE TABLE CompletedLevels 
                (
                    ID INTEGER NOT NULL,
                    Player_ID int NOT NULL,
                    Level_ID int NOT NULL,
                    PRIMARY KEY (ID), 
                    CONSTRAINT fk_Player FOREIGN KEY (Player_ID) REFERENCES Players(ID) ON DELETE CASCADE
                );

                CREATE TABLE PlayerItems 
                (
                    ID INTEGER NOT NULL,
                    Player_ID int NOT NULL,
                    Item_ID int NOT NULL,
                    Character_ID int,
                    PRIMARY KEY (ID), 
                    CONSTRAINT fk_Player FOREIGN KEY (Player_ID) REFERENCES Players(ID) ON DELETE CASCADE
                );

                CREATE TABLE CharacterSkills 
                (
                    ID INTEGER NOT NULL,
                    Character_ID int NOT NULL,
                    Skill_ID int NOT NULL,
                    IsUnlocked boolean NOT NULL,
                    PRIMARY KEY (ID), 
                    CONSTRAINT fk_PlayerCharacters FOREIGN KEY (Character_ID) REFERENCES PlayerCharacters(ID) ON DELETE CASCADE
                );";
            
                    dbcmd.ExecuteNonQuery();    
                }
                break;
            default:
                throw new Exception("Attempted to upgrade DB, with no upgrade script defined.");          
            }        
       
            transaction.Commit();      
       
        }
        finally
        {
            dbcon.Close();
        }
        
        PlayerPrefs.SetInt(DBVersionKey, dbVersion + 1);
    }
    
    
    
}