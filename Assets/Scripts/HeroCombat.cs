using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       

public class HeroCombat : MonoBehaviour
{
    public enum HeroAttackType { Melee, Ranged };
    public HeroAttackType heroAttackType;

    public GameObject targetedEnemy;
    [SerializeField] private float heroAttackRange;
    [SerializeField] private float heroRotateSpeedForAttack;

    [SerializeField] private Image attackRange_Indicator;
    [SerializeField] private Canvas attackRange_Canvas;
    [SerializeField] private float offset;
    private float radius;

    [SerializeField] private KeyCode checkAttackRange;

    private CharacterMovementScript moveScript;

    public bool basicAtkIdle = false;
    public bool isHeroAlive;
    public bool performMeleeAttack = true;

    Renderer r;
    [SerializeField] private Material mat;
    [SerializeField] private Material startMat;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<CharacterMovementScript>();
        radius = heroAttackRange / 2.0f / 2.0f / 2.0f / 1.25f;

        r = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAttackRange();

        attackRange_Indicator.transform.localScale = new Vector3(radius + offset, radius + offset, radius + offset);

        if (targetedEnemy != null)
        {
            if (Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) > heroAttackRange)
            {
                moveScript.agent.SetDestination(targetedEnemy.transform.position);
                moveScript.agent.stoppingDistance = heroAttackRange;
            }
            else
            {
                if (heroAttackType == HeroAttackType.Melee)
                {
                    if (performMeleeAttack)
                    {
                        Debug.Log("Attack the minion");
                        r.material = mat;
                        // Start Courotine
                    }
                }
            }
        }
        else
            r.material = startMat;
    }

    void CheckAttackRange()
    {
        if (Input.GetKeyDown(checkAttackRange) && attackRange_Indicator.GetComponent<Image>().enabled == false)
            attackRange_Indicator.GetComponent<Image>().enabled = true;
        else if (Input.GetKeyDown(checkAttackRange) && attackRange_Indicator.GetComponent<Image>().enabled == true)
            attackRange_Indicator.GetComponent<Image>().enabled = false;
    }
}
