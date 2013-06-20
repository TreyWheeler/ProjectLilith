using UnityEngine;
using System.Collections;

public class Ability
{
    private string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    private GUITexture _picture;
    public GUITexture Picture
    {
        get { return _picture; }
        set { _picture = value; }
    }


    private string _displayName;
    public string DisplayName
    {
        get { return _displayName; }
        set { _displayName = value; }
    }

    public void Do(CombatEntity actor, CombatEntity target)
    {

    }
}

