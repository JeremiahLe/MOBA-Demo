using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrackEnemyInfo_HUDWindow : MonoBehaviour
{
    [SerializeField] private Image EnemyHudIcon;
    [SerializeField] private Image HUDWindow;
    [SerializeField] private Sprite HUDWindowSprite;
    [SerializeField] private Sprite NoVisual_Sprite;

    [SerializeField] private TextMeshProUGUI EnemyHudName;
    [SerializeField] private TextMeshProUGUI EnemyHudStats;

    GameObject playerRef;
    GameObject targetedEnemyRef;

    HeroCombat heroCombat;
    EnemyStatsScript enemyStats;

    void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("MyPlayer");
        heroCombat = playerRef.GetComponent<HeroCombat>();

        EnemyHudName.text = "";
        EnemyHudStats.text = "";
    }

    private void LateUpdate()
    {
        if (enemyStats != null)
        {
            if (targetedEnemyRef != null)
            {
                EnemyHudStats.text = string.Format(
                    "HP - {0}/{1}\n" +
                    "MP - {2}/{3}\n" +
                    "AD - {4}   AP - {5}\n" +
                    "Def - {6}  Res - {7}\n" +
                    "Spd - {8}"
                    , enemyStats.enemyHealth, enemyStats.enemyMaxHealth
                    , enemyStats.enemyMana, enemyStats.enemyMaxMana
                    , enemyStats.enemyAttackDmg, enemyStats.enemyAbilityDmg
                    , enemyStats.enemyDef, enemyStats.enemyRes
                    , enemyStats.enemySpeed);
            }
        }
    }

    public void GetTargetedEnemy(GameObject _targetedEnemyRef)
    {
        targetedEnemyRef = _targetedEnemyRef;
        enemyStats = targetedEnemyRef.GetComponent<EnemyStatsScript>();

        EnemyHudName.text = enemyStats.enemyName + " Lvl " + enemyStats.enemyLevel;
        HUDWindow.sprite = HUDWindowSprite;
        EnemyHudIcon.sprite = enemyStats.enemyHUDIcon;
    }

    public void RemoveTargetedEnemy()
    {
        targetedEnemyRef = null;
        enemyStats = null;
        EnemyHudName.text = "";
        EnemyHudStats.text = "";
        HUDWindow.sprite = NoVisual_Sprite;
        EnemyHudIcon.sprite = NoVisual_Sprite;
    }

}
