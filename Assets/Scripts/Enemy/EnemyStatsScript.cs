using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsScript : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] public float enemyHealth;
    [DisplayWithoutEdit] public float enemyMaxHealth = 10;

    private void Start()
    {
        enemyMaxHealth = enemyHealth;
    }

    private void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}

