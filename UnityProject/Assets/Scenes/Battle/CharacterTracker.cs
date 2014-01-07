using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterTracker
{
    public readonly List<Character> AllCharacters = new List<Character>();

    public IEnumerable<Character> GetTeamCharactersFor(Character character)
    {
        foreach (var otherCharacter in AllCharacters.ToArray())
        {
            if (character.Team2 == otherCharacter.Team2)
                yield return otherCharacter;
        }
    }

    public IEnumerable<Character> GetEnemyCharactersFor(Character character)
    {
        foreach (var otherCharacter in AllCharacters.ToArray())
        {
            if (character.Team2 != otherCharacter.Team2)
                yield return otherCharacter;
        }
    }    
}
