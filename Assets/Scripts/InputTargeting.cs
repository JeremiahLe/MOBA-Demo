using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTargeting : MonoBehaviour
{
    [SerializeField] private GameObject selectedHero;
    [SerializeField] private bool heroPlayer;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        selectedHero = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Minion Targeting
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                // if minion is targetable
                if (hit.collider.GetComponent<TargetableScript>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<TargetableScript>().enemyType == TargetableScript.EnemyType.Minion)
                    {
                        selectedHero.GetComponent<HeroCombat>().targetedEnemy = hit.collider.gameObject;
                    }
                }

                else if (hit.collider.gameObject.GetComponent<TargetableScript>() == null)
                {
                    selectedHero.GetComponent<HeroCombat>().targetedEnemy = null;
                }
            }



        }
    }
}
