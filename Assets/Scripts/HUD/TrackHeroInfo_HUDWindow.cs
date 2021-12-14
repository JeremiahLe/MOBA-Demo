using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrackHeroInfo_HUDWindow : MonoBehaviour
{
    [SerializeField] private Image HeroHudIcon;
    [SerializeField] private Image HUDWindow;
    [SerializeField] private Sprite HUDWindowSprite;
    [SerializeField] private Sprite NoVisual_Sprite;

    [SerializeField] private TextMeshProUGUI HeroHudName;
    [SerializeField] private TextMeshProUGUI HeroHudStats;

    bool showHUDDetails = true;

    GameObject playerRef;

    HeroCombat heroCombat;
    HeroClass hero;

    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("MyPlayer");
        heroCombat = playerRef.GetComponent<HeroCombat>();
        hero = playerRef.GetComponent<HeroClass>();

        HeroHudIcon.sprite = hero.heroHUDSprite;
        HeroHudName.text = hero.HeroName;

        HeroHudStats.text = string.Format(
                    "Lvl {0}\n" +
                    "AD - {1}   AP - {2}\n" +
                    "Def - {3}  Res - {4}\n" +
                    "Crit - {5}     AS - {6}\n" +
                    "Lfstl - {7}    Ten - {8}\n" +
                    "Spd - {9}"
                    , hero.heroLevel
                    , hero.heroAttackDmg, hero.heroAbilityDmg
                    , hero.heroDef, hero.heroRes
                    , hero.heroCritChance, hero.heroAttackSpeed
                    , hero.heroLifesteal, hero.heroTenacity
                    , hero.heroSpeed);
    }

    void CallNonUpdatedStats(bool toggleWindow)
    {
        if (HeroHudIcon.sprite == null && HeroHudName.text == "")
        {
            Debug.Log("Called Load Fix");
            HeroHudIcon.sprite = hero.heroHUDSprite;
            HeroHudName.text = hero.HeroName;
        }
        else if (toggleWindow)
        {
            HeroHudIcon.sprite = hero.heroHUDSprite;
            HeroHudName.text = hero.HeroName;
            HUDWindow.sprite = HUDWindowSprite;
        }
    }

    public void TurnOffWindow()
    {
        if (showHUDDetails)
        {
            showHUDDetails = false;
            HeroHudStats.text = "";
            HeroHudName.text = "";
            HeroHudIcon.sprite = null;
            HeroHudIcon.sprite = NoVisual_Sprite;
            HUDWindow.sprite = NoVisual_Sprite;
        }
        else if (!showHUDDetails) {
            showHUDDetails = true;
            CallNonUpdatedStats(true);
        }
    }

    private void LateUpdate()
    {
        CallNonUpdatedStats(false);

        if (playerRef != null)
        {
            if (showHUDDetails)
            {
                HeroHudStats.text = string.Format(
                    "Lvl {0}\n" +
                    "AD - {1}   AP - {2}\n" +
                    "Def - {3}  Res - {4}\n" +
                    "Crit - {5}  AS - {6}\n" +
                    "Lfstl - {7}  Ten - {8}\n" +
                    "Spd - {9}"
                    , hero.heroLevel
                    , hero.heroAttackDmg, hero.heroAbilityDmg
                    , hero.heroDef, hero.heroRes
                    , hero.heroCritChance, hero.heroAttackSpeed
                    , hero.heroLifesteal, hero.heroTenacity
                    , hero.heroSpeed);
            }
        }
    }
}
