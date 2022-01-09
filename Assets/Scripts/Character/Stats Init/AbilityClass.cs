using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AbilityClass
{
    public enum TypeOfAbilityCast { Skillshot, Targeted, AOE, AOESkillShot, SelfBuff, TargetedBuff };
    public TypeOfAbilityCast typeOfAbilityCast;

    public enum TypeOfAbilityCC { None, Slow, Stun, Blind };
    public TypeOfAbilityCC typeOfAbilityCC;

    public enum TypeOfAbilityDamage { AttackDamage, AbilityDamage, TrueDamage, NoDamage };
    public TypeOfAbilityDamage typeOfAbilityDamage;

    public string abilityName;

    public int abilityLevel;
    public int abilityMaxLevel;
    public float abilityCost;

    public float abilityCooldown;
    public float abilityDuration;

    public float abilityBuffPercentage;

    public float abilityRangeNum;
    public float abilitySpeed;
    public float abilityCastTime;

    public float abilityBaseDamage;
    public string abilityDescription;
    public string abilityPerLevel;

    public float abilityScaling;

    public float[] abilityDamagePerLevel;
    public float[] abilityCostPerLevel;
    public float[] abilityCooldownPerLevel;

    public float[] abilityBuffPercentagePerLevel;
    public float[] abilityDurationPerLevel;

    public bool isCooldown;
    public bool isUltimate;

    public Sprite Indicator;
    public Sprite Range;
    //public Sprite abilitySprite;
    public Image HUDIcon;

    public KeyCode abilityKeyCode;
}
