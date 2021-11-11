using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTargeting : MonoBehaviour
{
    [SerializeField] private GameObject selectedHero;
    [SerializeField] private bool heroPlayer;

    RaycastHit hit;

    GameObject targetedEnemyRef;
    GameObject prevEnemyRef;
    TrackEnemyInfo_HUDWindow HUDWindow;

    [SerializeField] private Image HudTargetWindow;

    // Start is called before the first frame update
    void Start()
    {
        selectedHero = GameObject.FindGameObjectWithTag("MyPlayer");
        HUDWindow = HudTargetWindow.GetComponent<TrackEnemyInfo_HUDWindow>();
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
                        selectedHero.GetComponent<HeroCombat>().moveToEnemy = true;

                        if (prevEnemyRef == null)
                        {
                            targetedEnemyRef = selectedHero.GetComponent<HeroCombat>().targetedEnemy;
                            prevEnemyRef = targetedEnemyRef;
                            selectedHero.GetComponent<HeroCombat>().targetedEnemy.GetComponent<Outline>().enabled = true;

                            HUDWindow.GetTargetedEnemy(targetedEnemyRef);
                        }
                        else
                        {
                            prevEnemyRef.GetComponent<Outline>().enabled = false;
                            targetedEnemyRef = selectedHero.GetComponent<HeroCombat>().targetedEnemy;
                            prevEnemyRef = targetedEnemyRef;
                            selectedHero.GetComponent<HeroCombat>().targetedEnemy.GetComponent<Outline>().enabled = true;

                            HUDWindow.GetTargetedEnemy(targetedEnemyRef);
                        }
                    }
                }

                else if (hit.collider.gameObject.GetComponent<TargetableScript>() == null)
                {
                    if (targetedEnemyRef != null)
                        targetedEnemyRef.GetComponent<Outline>().enabled = false;

                    prevEnemyRef = null;
                    targetedEnemyRef = null;
                }
            }
        }

        // Minion Targeting left-click no move
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                // if minion is targetable
                if (hit.collider.GetComponent<TargetableScript>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<TargetableScript>().enemyType == TargetableScript.EnemyType.Minion)
                    {
                        if (hit.collider.gameObject != selectedHero.GetComponent<HeroCombat>().targetedEnemy)
                        {
                            selectedHero.GetComponent<HeroCombat>().ResetAutoAttack(); // fix auto infinite range  ?
                        }

                        selectedHero.GetComponent<HeroCombat>().targetedEnemy = hit.collider.gameObject;

                        selectedHero.GetComponent<HeroCombat>().moveToEnemy = false;

                        if (prevEnemyRef == null)
                        {
                            targetedEnemyRef = selectedHero.GetComponent<HeroCombat>().targetedEnemy;
                            prevEnemyRef = targetedEnemyRef;
                            selectedHero.GetComponent<HeroCombat>().targetedEnemy.GetComponent<Outline>().enabled = true;

                            HUDWindow.GetTargetedEnemy(targetedEnemyRef);
                        }
                        else
                        {
                            prevEnemyRef.GetComponent<Outline>().enabled = false;
                            targetedEnemyRef = selectedHero.GetComponent<HeroCombat>().targetedEnemy;
                            prevEnemyRef = targetedEnemyRef;
                            selectedHero.GetComponent<HeroCombat>().targetedEnemy.GetComponent<Outline>().enabled = true;

                            HUDWindow.GetTargetedEnemy(targetedEnemyRef);
                        }
                    }
                }

                else if (hit.collider.gameObject.GetComponent<TargetableScript>() == null)
                {
                    if (targetedEnemyRef != null)
                        targetedEnemyRef.GetComponent<Outline>().enabled = false;

                    prevEnemyRef = null;
                    targetedEnemyRef = null;
                    HUDWindow.RemoveTargetedEnemy();
                }
            }
        }
    }
}
