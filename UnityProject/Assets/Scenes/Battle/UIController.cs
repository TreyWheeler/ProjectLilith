using UnityEngine;
using System.Collections;
using System.Linq;

public class UIController : MonoBehaviour
{

    public BattleScene BattleScene;
    public CharacterIconsWheel PlayerIconsWheel;
    public AbilityIconsWheel AbilityIconsWheel;
    public CharacterIconsWheel TargetIconWheel;
    public GameObject IconSlider;

    private Ability selectedAbility;
    private Character selectedPlayerCharacter;

    // Use this for initialization
    void Start()
    {
        PlayerIconsWheel.ClearAndAdd(BattleScene.Allies);
        PlayerIconsWheel.OnCharacterSelection += PlayerIconsWheel_OnCharacterSelection;
        AbilityIconsWheel.OnAbilitySelection += AbilityIconsWheel_OnAbilitySelection;
        TargetIconWheel.OnCharacterSelection += EnemyIconsWheel_OnCharacterSelection;
        AbilityIconsWheel.gameObject.SetActive(false);
        TargetIconWheel.gameObject.SetActive(false);
    }


    void PlayerIconsWheel_OnCharacterSelection(Character character)
    {
        selectedPlayerCharacter = character;
        AbilityIconsWheel.ClearAndAdd(character.MyAbilities);
        AbilityIconsWheel.gameObject.SetActive(true);
        TargetIconWheel.gameObject.SetActive(false);

        AbilityIconsWheel.NoteSelected(character);
    }

    void AbilityIconsWheel_OnAbilitySelection(Ability ability)
    {
        selectedAbility = ability;
        AbilityIconsWheel.gameObject.SetActive(false);
        TargetIconWheel.gameObject.SetActive(true);
        if (ability.IsFriendly)
            TargetIconWheel.ClearAndAdd(BattleScene.Allies);
        else
            TargetIconWheel.ClearAndAdd(BattleScene.Enemies);
        TargetIconWheel.NoteSelected(ability);
    }

    void EnemyIconsWheel_OnCharacterSelection(Character obj)
    {
        selectedPlayerCharacter.QueueAbility(new IntendedAction(selectedAbility, obj.gameObject));
        TargetIconWheel.gameObject.SetActive(false);
        AbilityIconsWheel.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
