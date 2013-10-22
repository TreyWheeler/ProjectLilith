using UnityEngine;
using System.Collections;

public class IntendedAction 
{
    public Ability Ability;
    public GameObject DestinationGameObject;

    public IntendedAction(Ability ability, GameObject destinationGameObject)
    {
        Ability = ability;
        DestinationGameObject = destinationGameObject;
    }
}
