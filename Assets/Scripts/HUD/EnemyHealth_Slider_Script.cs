using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth_Slider_Script : MonoBehaviour
{
    Slider enemySlider3D;

    EnemyStatsScript enemyStatsScript;

    // Start is called before the first frame update
    void Start()
    {
        //enemyStatsScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStatsScript>(); // in multiplayer, check if owned as well

        enemyStatsScript = GetComponentInParent<EnemyStatsScript>();

        enemySlider3D = GetComponentInChildren<Slider>();

        //enemySlider3D.maxValue = enemyStatsScript.enemyMaxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyStatsScript != null) {
            enemySlider3D.value = enemyStatsScript.enemyHealth;
            enemySlider3D.maxValue = enemyStatsScript.enemyMaxHealth;
        }

    }
}

