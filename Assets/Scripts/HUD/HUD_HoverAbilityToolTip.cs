using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class HUD_HoverAbilityToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image AbilityToolTipWindow;
    [SerializeField] private Sprite NoVisual_Sprite;
    [SerializeField] private Sprite Window_Sprite;
    [SerializeField] private GameObject playerHeroClass;

    [SerializeField] private TextMeshProUGUI abilityNameText;
    [SerializeField] private TextMeshProUGUI abilityCostText;
    [SerializeField] private TextMeshProUGUI abilityDescriptionText;
    [SerializeField] private TextMeshProUGUI abilityPerLevelText;

    [SerializeField] private string abilityInit;
    private HeroClass heroClass;

    public void Start()
    {
        heroClass = playerHeroClass.GetComponent<HeroClass>();

        AbilityToolTipWindow.sprite = NoVisual_Sprite;
        abilityNameText.text = "";
        abilityCostText.text = "";
        abilityDescriptionText.text = "";
        abilityPerLevelText.text = "";
    }

    public void UpdateAbilityTooltip()
    {
        switch (abilityInit)
        {
            case "Q":
                heroClass.GetUpdatedStats(heroClass.Q_Ability);
                abilityNameText.text = heroClass.Q_Ability.abilityName;
                abilityCostText.text = heroClass.Q_Ability.abilityCooldown.ToString() + " secs.\n" + heroClass.Q_Ability.abilityCost.ToString() + " Mana";
                abilityDescriptionText.text = heroClass.Q_Ability.abilityDescription;
                abilityPerLevelText.text = heroClass.Q_Ability.abilityPerLevel;
                break;
            case "W":
                heroClass.GetUpdatedStats(heroClass.W_Ability);
                abilityNameText.text = heroClass.W_Ability.abilityName;
                abilityCostText.text = heroClass.W_Ability.abilityCooldown.ToString() + " secs.\n" + heroClass.W_Ability.abilityCost.ToString() + " Mana";
                abilityDescriptionText.text = heroClass.W_Ability.abilityDescription;
                abilityPerLevelText.text = heroClass.W_Ability.abilityPerLevel;
                break;
            case "E":
                heroClass.GetUpdatedStats(heroClass.E_Ability);
                abilityNameText.text = heroClass.E_Ability.abilityName;
                abilityCostText.text = heroClass.E_Ability.abilityCooldown.ToString() + " secs.\n" + heroClass.E_Ability.abilityCost.ToString() + " Mana";
                abilityDescriptionText.text = heroClass.E_Ability.abilityDescription;
                abilityPerLevelText.text = heroClass.E_Ability.abilityPerLevel;
                break;
            case "R":
                heroClass.GetUpdatedStats(heroClass.R_Ability);
                abilityNameText.text = heroClass.R_Ability.abilityName;
                abilityCostText.text = heroClass.R_Ability.abilityCooldown.ToString() + " secs.\n" + heroClass.R_Ability.abilityCost.ToString() + " Mana";
                abilityDescriptionText.text = heroClass.R_Ability.abilityDescription;
                abilityPerLevelText.text = heroClass.R_Ability.abilityPerLevel;
                break;
            default:
                Debug.Log("Missing ability reference?");
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AbilityToolTipWindow.sprite = Window_Sprite;
        UpdateAbilityTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AbilityToolTipWindow.sprite = NoVisual_Sprite;
        abilityNameText.text = "";
        abilityCostText.text = "";
        abilityDescriptionText.text = "";
        abilityPerLevelText.text = "";
    }
}
