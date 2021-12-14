using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyStatsScript : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] public string enemyName = "";
    [SerializeField] public Sprite enemyHUDIcon;

    [SerializeField] public float enemyHealth;
    [DisplayWithoutEdit] public float enemyMaxHealth = 10;

    [SerializeField] public float enemyMana;
    [DisplayWithoutEdit] public float enemyMaxMana = 0;

    [SerializeField] public float enemyAttackDmg;
    [SerializeField] public float enemyAbilityDmg;

    [SerializeField] public float enemyAttackSpeed;
    [SerializeField] public float enemyAttackTime;

    [SerializeField] public float enemyDef;
    [SerializeField] public float enemyRes;

    [DisplayWithoutEdit] public float enemySpeed;

    NavMeshAgent agent;

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();

        // Stat Init
        enemyMaxHealth = enemyHealth;
        agent.speed = enemySpeed;
    }

}

