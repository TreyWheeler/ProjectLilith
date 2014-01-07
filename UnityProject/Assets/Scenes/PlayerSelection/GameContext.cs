using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameContext: UnityContext<ContainerContext>
{
    
}

public class ContainerContext : IContextRoot
{
    public IoC.IContainer container { get; private set; }
 
    public ContainerContext()
    {        
        // ENTRY POINT OF EACH SCENE
     
        SetupContainer();
        StartGame();
    }
    
    void SetupContainer()
    {
        container = new IoC.UnityContainer();
        container.Bind<CharacterTracker>().AsSingle();

        // ICharacterSkillRepository = SQLCharacterSkillRepository

        //container.Bind<ICharacterSkillRepository>().AsSingle<SQLCharacterSkillRepository>();
        //container.Bind<ICompletedLevelRepository>().AsSingle<SQLCompletedLevelRepository>();
        //container.Bind<IPlayerCharacterEXPRepository>().AsSingle<SQLPlayerCharacterEXPRepository>();
        //container.Bind<IPlayerCharacterRepository>().AsSingle<SQLPlayerCharacterRepository>();
        //container.Bind<IPlayerItemRepository>().AsSingle<SQLPlayerItemRepository>();
        //container.Bind<IPlayerRepository>().AsSingle<SQLPlayerRepository>();        
    }
 
    void StartGame()
    {

		
    }
}


