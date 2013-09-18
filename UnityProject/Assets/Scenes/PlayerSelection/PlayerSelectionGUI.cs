using UnityEngine;
using System.Collections.Generic;

public class PlayerSelectionGUI : MonoBehaviour
{
    [IoC.Inject]
    public IPlayerRepository PlayerRepository { get; set; }

    List<Player> _players;
    
    void Start()
    {
        this.Inject();
        
        PlayerRepository.DeleteAll();
        _players = PlayerRepository.GetAll();
    }
 
    void OnGUI()
    {
        
        for(int i = 0; i < 3; i++)
        {            
            if(_players.Count > i)
            {
                if(PlaceButtonAtSlot(i, _players[i].Name))
                {
                    Helper.LoadScene<string>("TempScene", "HI");
                    //Application.LoadLevel("TempScene");                    
                }                    
            }
            else
            {
                if(PlaceButtonAtSlot(i, "New Player"))
                {
                    Player newPlayer = new Player();
                    newPlayer.Name = "Trey";
                    newPlayer.Gold = 500000;
                    
                    PlayerRepository.Save(newPlayer);
                    
                    _players = PlayerRepository.GetAll();
                } 
            }
        }
    
    }
    
    bool PlaceButtonAtSlot(int slot, string text)
    {
        float boxNumber = slot + 1;
        
        float boxHeight = Screen.height * .2f; // 20% of height
        float boxWidth = Screen.width * .7f;
        
        float margin = Screen.height * .1f;
        
        float slotCenterX = Screen.width / 2;        
        float slotCenterY = margin * boxNumber + boxHeight * boxNumber - boxHeight / 2;
        
        return GUI.Button(Helper.GetBoxCenteredOnPoint(slotCenterX, slotCenterY, boxHeight, boxWidth), text);
    }
}
