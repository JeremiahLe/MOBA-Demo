using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Base Hero Class where attributes like Name and Abilities are set
/// </summary>
/// 
public class HeroClass : MonoBehaviour
{
    public enum HeroAssigner { Cube, Jawn }
    public HeroAssigner heroAssigner;
    [DisplayWithoutEdit] public string HeroName;
    [DisplayWithoutEdit] public Sprite heroHUDSprite;

    [Header("Hero Stats")]
    [DisplayWithoutEdit] public int heroLevel;

    [SerializeField] public float heroHealth;
    [DisplayWithoutEdit] public float heroMaxHealth;

    [DisplayWithoutEdit] public float heroDef;
    [DisplayWithoutEdit] public float heroRes;

    [DisplayWithoutEdit] public float heroMana;
    [DisplayWithoutEdit] public float heroMaxMana;

    [DisplayWithoutEdit] public float heroAttackDmg;
    [DisplayWithoutEdit] public float heroAbilityDmg;

    [DisplayWithoutEdit] public float heroAttackSpeed;
    [DisplayWithoutEdit] public float heroAttackTime;
    [DisplayWithoutEdit] public float heroCritChance;

    [DisplayWithoutEdit] public float heroLifesteal;
    [DisplayWithoutEdit] public float heroVamp;

    [DisplayWithoutEdit] public float heroSpeed;
    [DisplayWithoutEdit] public float heroTenacity;

    [Header("Q Ability")]
    public AbilityClass Q_Ability;
    [SerializeField] private Sprite Q_Indicator;
    [SerializeField] private Image Q_HudIcon;
    [SerializeField] private Sprite Q_Range;

    [Header("W Ability")]
    public AbilityClass W_Ability;
    [SerializeField] private Sprite W_Indicator;
    [SerializeField] private Image W_HudIcon;
    [SerializeField] private Sprite W_Range;

    [Header("E Ability")]
    public AbilityClass E_Ability;
    [SerializeField] private Sprite E_Indicator;
    [SerializeField] private Image E_HudIcon;
    [SerializeField] private Sprite E_Range;

    [Header("R Ability")]
    public AbilityClass R_Ability;
    [SerializeField] private Sprite R_Indicator;
    [SerializeField] private Image R_HudIcon;
    [SerializeField] private Sprite R_Range;

    [Header("Misc. Components")]
    [SerializeField] GameObject GameControlScriptObject;

    GameControl_Script gsScript;
    HeroCombat heroCombatScript;

    private CharacterMovementScript moveScript;

    private KeyCode Q_Ability_Hotkey;
    private KeyCode W_Ability_Hotkey;
    private KeyCode E_Ability_Hotkey;
    private KeyCode R_Ability_Hotkey;

    HeroClass()
    {
        heroLevel = 1;

        heroHealth = 100f;
        heroMaxHealth = heroHealth;

        heroMana = 100f;
        heroMaxMana = heroMana;

        heroAttackDmg = 5f;
        heroAbilityDmg = 5f;

        heroAttackSpeed = 1;
        heroAttackTime = 1.4f;

        heroDef = 1f;
        heroRes = 1f;

        heroCritChance = 0f;
        heroLifesteal = 0f;
        heroVamp = 0f;

        heroSpeed = 5f;
        heroTenacity = 0f;
}

    void Start()
    {
        GetPlayerHotkeys();
        HeroName = heroAssigner.ToString();
        CallHeroInit(heroAssigner);

        moveScript = GetComponent<CharacterMovementScript>();
        heroCombatScript = GetComponent<HeroCombat>();
    }

    void Update()
    {
        heroSpeed = moveScript.agent.speed;
    }

    void GetPlayerHotkeys()
    {
        gsScript = GameControlScriptObject.GetComponent<GameControl_Script>();
        Q_Ability_Hotkey = gsScript.Q_Ability_Keycode;
        W_Ability_Hotkey = gsScript.W_Ability_Keycode;
        E_Ability_Hotkey = gsScript.E_Ability_Keycode;
        R_Ability_Hotkey = gsScript.R_Ability_Keycode;
    }

    void CallHeroInit(HeroAssigner hero)
    {
        switch (heroAssigner)
        {
            case HeroAssigner.Cube:
                Hero_Cube_Ability_Init();
                Debug.Log("Assigned Hero is " + HeroName);
                break;

            case HeroAssigner.Jawn:
                Debug.Log("Assigned Hero is " + HeroName);
                break;

            default:
                Debug.Log("No Hero Assigned!");
                break;
        }
    }

    void Load_HeroHUDSprite(HeroAssigner _assignedHero)
    {
        heroAssigner = _assignedHero;

        switch (heroAssigner)
        {
            case (HeroAssigner.Cube):
                heroHUDSprite = Resources.Load<Sprite>("HeroHUDIcons/Mina_HUD_Sprite");
                Debug.Log("Loaded " + heroAssigner.ToString() + " sprite");
                break;

            default:
                Debug.Log("No Hero found?!");
                break;
        }
    }

    // Hero inits 

    void Hero_Cube_Ability_Init()
    {
        ///
        /// Hero - Cube Stat Init
        /// 

        heroLevel = 1;
        Load_HeroHUDSprite(HeroAssigner.Cube);

        heroHealth = 450f;
        heroMaxHealth = heroHealth;

        heroMana = 100f;
        heroMaxMana = heroMana;

        heroAttackDmg = 25f;
        heroAbilityDmg = 0f;

        heroAttackSpeed = .85f;
        heroAttackTime = 1.4f;

        heroDef = 1f;
        heroRes = 1f;

        heroCritChance = 0f;
        heroLifesteal = 0f;
        heroVamp = 0f;

        heroSpeed = 5f; // this needs to be set in the NavMesh Agent
        heroTenacity = 0f;

        ///
        /// Q Abililty Init
        ///

        /// Ability Summary:
        /// Shoots a medium range projectile in a line, dealing ability damage to first enemy hit.

        Q_Ability = new AbilityClass();
        Q_Ability.typeOfAbilityCast = AbilityClass.TypeOfAbilityCast.Skillshot;
        Q_Ability.typeOfAbilityCC = AbilityClass.TypeOfAbilityCC.None;
        Q_Ability.typeOfAbilityDamage = AbilityClass.TypeOfAbilityDamage.AttackDamage;

        Q_Ability.abilityName = "Cube Shot";
        Q_Ability.abilityCooldown = 2f;
        Q_Ability.HUDIcon = Q_HudIcon;
        Q_Ability.Indicator = Q_Indicator;
        Q_Ability.Range = Q_Range;
        Q_Ability.abilityKeyCode = Q_Ability_Hotkey;

        Q_Ability.abilityRangeNum = 6.5f;

        Q_Ability.abilityDuration = 0f;
        Q_Ability.abilityBuffPercentage = 0f;

        Q_Ability.abilitySpeed = 600f; // average speed
        Q_Ability.abilityCastTime = 0f;

        Q_Ability.abilityBaseDamage = 60f;

        // may change this
        //Q_Ability.abilitySprite = Resources.Load<Sprite>("ProjectilePrefabSprites/Fireball_Sprite");

        ///
        /// W Abililty Init
        ///

        /// Ability Summary:
        /// Cast a short range bomb at a target location within a certain range, dealing aoe damage.
        /// 

        W_Ability = new AbilityClass();
        W_Ability.typeOfAbilityCast = AbilityClass.TypeOfAbilityCast.AOESkillShot;
        W_Ability.typeOfAbilityCC = AbilityClass.TypeOfAbilityCC.None;
        W_Ability.typeOfAbilityDamage = AbilityClass.TypeOfAbilityDamage.AbilityDamage;

        W_Ability.abilityName = "Cube Bomb";
        W_Ability.abilityCooldown = 2f;
        W_Ability.HUDIcon = W_HudIcon;
        W_Ability.Indicator = W_Indicator;
        W_Ability.Range = W_Range;
        W_Ability.abilityKeyCode = W_Ability_Hotkey;

        W_Ability.abilityRangeNum = 4.7f;

        W_Ability.abilityDuration = 0f;
        W_Ability.abilityBuffPercentage = 0f;

        W_Ability.abilitySpeed = 0f; // it doesn't move
        W_Ability.abilityCastTime = 0f;

        W_Ability.abilityBaseDamage = 110f;

        ///
        /// E Abililty Init
        ///

        /// Ability Summary:
        /// Cast to gain a burst of movement speed for a few seconds.
        /// 

        E_Ability = new AbilityClass();
        E_Ability.typeOfAbilityCast = AbilityClass.TypeOfAbilityCast.SelfBuff;
        E_Ability.typeOfAbilityCC = AbilityClass.TypeOfAbilityCC.None;
        E_Ability.typeOfAbilityDamage = AbilityClass.TypeOfAbilityDamage.NoDamage;

        E_Ability.abilityName = "Cube Rush";
        E_Ability.abilityCooldown = 4f;
        E_Ability.HUDIcon = E_HudIcon;
        E_Ability.Indicator = E_Indicator;
        E_Ability.Range = E_Range;
        E_Ability.abilityKeyCode = E_Ability_Hotkey;

        E_Ability.abilityRangeNum = 1f;

        E_Ability.abilityDuration = 1.5f;
        E_Ability.abilityBuffPercentage = .85f;

        ///
        /// R Abililty Init
        ///

        /// Ability Summary:
        /// Cast a targeted missile at an enemy.
        /// 

        R_Ability = new AbilityClass();
        R_Ability.typeOfAbilityCast = AbilityClass.TypeOfAbilityCast.Targeted;
        R_Ability.typeOfAbilityCC = AbilityClass.TypeOfAbilityCC.Slow;
        R_Ability.typeOfAbilityDamage = AbilityClass.TypeOfAbilityDamage.AbilityDamage;

        R_Ability.abilityName = "Cube Missile";
        R_Ability.abilityCooldown = 60f;
        R_Ability.HUDIcon = R_HudIcon;
        R_Ability.Indicator = R_Indicator;
        R_Ability.Range = R_Range;
        R_Ability.abilityKeyCode = R_Ability_Hotkey;

        R_Ability.abilityRangeNum = 9f;

        R_Ability.abilityDuration = 0f;
        R_Ability.abilityBuffPercentage = 0f;
    }
}
