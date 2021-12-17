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

    [Header("Stat Icons")]
    [SerializeField] private Image HeroAttackDamageIcon;
    [SerializeField] private Image HeroAbilityDamageIcon;
    [SerializeField] private Sprite HeroAttackDamageSprite;
    [SerializeField] private Sprite HeroAbilityDamageSprite;

    [SerializeField] private Image HeroDefenseIcon;
    [SerializeField] private Image HeroResistanceIcon;
    [SerializeField] private Sprite HeroDefenseSprite;
    [SerializeField] private Sprite HeroResistanceSprite;

    [SerializeField] private Image HeroCritChanceIcon;
    [SerializeField] private Image HeroAttackSpeedIcon;
    [SerializeField] private Sprite HeroCritChanceSprite;
    [SerializeField] private Sprite HeroAttackSpeedSprite;

    [SerializeField] private Image HeroLifeStealIcon;
    [SerializeField] private Image HeroTenacityIcon;
    [SerializeField] private Sprite HeroLifeStealSprite;
    [SerializeField] private Sprite HeroTenacitySprite;

    [SerializeField] private Image HeroCDRIcon;
    [SerializeField] private Image HeroMoveSpeedIcon;
    [SerializeField] private Sprite HeroCDRSprite;
    [SerializeField] private Sprite HeroMoveSpeedSprite;

    public List<Image> StatIconImages;
    public List<Sprite> StatIconSprites;

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
                    "    {1}             {2}\n" +
                    "    {3}              {4}\n" +
                    "    {5}%           {6}\n" +
                    "    {7}%           {8}%\n" +
                    "    {9}              {10}"
                    , hero.heroLevel
                    , hero.heroAttackDmg, hero.heroAbilityDmg
                    , hero.heroDef, hero.heroRes
                    , hero.heroCritChance, hero.heroAttackSpeed
                    , hero.heroLifesteal, hero.heroTenacity
                    , hero.heroCooldownReduction, hero.heroSpeed);

        #region Adding Icons & Sprites to Lists
        StatIconImages.Add(HeroAttackDamageIcon); // attack damage
        StatIconImages.Add(HeroAbilityDamageIcon); // ability damage
        StatIconImages.Add(HeroDefenseIcon); // defense
        StatIconImages.Add(HeroResistanceIcon); // resistance
        StatIconImages.Add(HeroCritChanceIcon); // crit chance
        StatIconImages.Add(HeroAttackSpeedIcon); // attack speed
        StatIconImages.Add(HeroLifeStealIcon); // lifesteal
        StatIconImages.Add(HeroTenacityIcon); // tenacity
        StatIconImages.Add(HeroCDRIcon); // cooldown reduction
        StatIconImages.Add(HeroMoveSpeedIcon); // movespeed

        StatIconSprites.Add(HeroAttackDamageSprite); // attack damage
        StatIconSprites.Add(HeroAbilityDamageSprite); // ability damage
        StatIconSprites.Add(HeroDefenseSprite); // defense
        StatIconSprites.Add(HeroResistanceSprite); // resistance
        StatIconSprites.Add(HeroCritChanceSprite); // crit chance
        StatIconSprites.Add(HeroAttackSpeedSprite); // attack speed
        StatIconSprites.Add(HeroLifeStealSprite); // lifesteal
        StatIconSprites.Add(HeroTenacitySprite); // tenacity
        StatIconSprites.Add(HeroCDRSprite); // cooldown reduction
        StatIconSprites.Add(HeroMoveSpeedSprite); // movespeed
        #endregion
    }

    void CallNonUpdatedStats(bool toggleWindow)
    {
        if (HeroHudIcon.sprite == null && HeroHudName.text == "")
        {
            Debug.Log("Called Load Fix");
            HeroHudIcon.sprite = hero.heroHUDSprite;
            HeroHudName.text = hero.HeroName;

            int index = 0;
            foreach (Image statIcon in StatIconImages)
            {
                statIcon.sprite = StatIconSprites[index];
                index++;
            }
        }
        else if (toggleWindow)
        {
            HeroHudIcon.sprite = hero.heroHUDSprite;
            HeroHudName.text = hero.HeroName;
            HUDWindow.sprite = HUDWindowSprite;

            int index = 0;
            foreach (Image statIcon in StatIconImages)
            {
                statIcon.sprite = StatIconSprites[index];
                index++;
            }
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

            foreach (Image statIcon in StatIconImages)
            {
                statIcon.sprite = NoVisual_Sprite;
            }
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
                    "    {1}          {2}\n" +
                    "    {3}            {4}\n" +
                    "    {5}%         {6}\n" +
                    "    {7}%         {8}%\n" +
                    "    {9}%         {10}"
                    , hero.heroLevel
                    , hero.heroAttackDmg, hero.heroAbilityDmg
                    , hero.heroDef, hero.heroRes
                    , hero.heroCritChance, hero.heroAttackSpeed
                    , hero.heroLifesteal, hero.heroTenacity
                    , hero.heroCooldownReduction, hero.heroSpeed);
            }
        }
    }
}
