using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpSlider2D_Script : MonoBehaviour
{
    [SerializeField] private Slider playerSlider3D;
    Slider playerSlider2D;

    HeroClass heroClassScript;
    [SerializeField] SkillpointCheck_Script skillpointCheck_Script;

    void Start()
    {
        heroClassScript = GameObject.FindGameObjectWithTag("MyPlayer").GetComponent<HeroClass>(); // in multiplayer, check if owned as well

        playerSlider2D = GetComponent<Slider>();

        playerSlider3D.maxValue = heroClassScript.heroExpToNext;
        playerSlider2D.maxValue = heroClassScript.heroExpToNext;

        //heroClassScript.heroHealth = heroClassScript.heroMaxHealth; // wtf is this?
    }

    void Update()
    {

        playerSlider3D.maxValue = heroClassScript.heroExpToNext;
        playerSlider2D.maxValue = heroClassScript.heroExpToNext;

        playerSlider2D.value = heroClassScript.heroExp;
        playerSlider3D.value = playerSlider2D.value;

        CheckLevelUp();
    }

    void CheckLevelUp()
    {
        // Check current exp
        if (heroClassScript.heroExp >= heroClassScript.heroExpToNext)
        {
            // TELL EVERYTHING THAT THE PLAYER LEVELED UP!!
            // Level up the player and his stats
            heroClassScript.heroLevel += 1;

            if (heroClassScript.heroLevel == 6 || heroClassScript.heroLevel == 11 || heroClassScript.heroLevel == 16)
            {
                heroClassScript.heroCanLevelUltimate = true;
            }

            heroClassScript.heroMaxHealth += heroClassScript.heroMaxHealthPerLevel;
            heroClassScript.heroMaxMana += heroClassScript.heroMaxManaPerLevel;

            heroClassScript.heroDef += heroClassScript.heroDefPerLevel;
            heroClassScript.heroRes += heroClassScript.heroResPerLevel;

            heroClassScript.heroAttackDmg += heroClassScript.heroAttackDmgPerLevel;
            heroClassScript.heroAttackSpeed += heroClassScript.heroAttackSpeedPerLevel;

            // Obtain any overlap exp
            heroClassScript.heroExp = heroClassScript.heroExpToNext - heroClassScript.heroExp;

            // Increase exp level requirement
            heroClassScript.heroExpToNext *= 2;

            // Get a skill point
            heroClassScript.heroSkillPoints += 1;

            // Update HUD UI
            skillpointCheck_Script.AlertObservers("Gained a skill point.");
        }
    }
}
