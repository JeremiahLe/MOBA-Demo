using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       

public class HeroCombat : MonoBehaviour
{
    // Hero Init
    public enum HeroAttackType { Melee, Ranged };
    public HeroAttackType heroAttackType;

    // Combat targeting and range
    public GameObject targetedEnemy;
    [SerializeField] private float heroAttackRange;
    [SerializeField] private float heroRotateSpeedForAttack;

    // Combat range visuals
    [SerializeField] private Image attackRange_Indicator;
    [SerializeField] private Canvas attackRange_Canvas;
    private float offset;
    [SerializeField] private KeyCode checkAttackRange;
    private float radius;
    Image attackRange_Indicator_Image;

    // Character Animator
    private CharacterMovementScript moveScript;
    private HeroClass heroClassScript;
    private Animator anim;

    // Combat Variables
    public bool basicAtkIdle = false;
    public bool isHeroAlive;
    public bool performMeleeAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<CharacterMovementScript>();
        heroClassScript = GetComponent<HeroClass>();
        anim = GetComponent<Animator>();

        attackRange_Indicator_Image = attackRange_Indicator.GetComponent<Image>();

        // This is defunct code to scale the image of the attack range properly
        radius = heroAttackRange / 2.0f / 2.0f / 2.0f / 1.25f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckAttackRange();

        CheckCombat();

        attackRange_Indicator.transform.localScale = new Vector3(radius + offset, radius + offset, radius + offset);
    }

    void CheckCombat()
    {
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

                        // Start Courotine
                        StartCoroutine(MeleeAttackInterval());
                    }
                }
            }
        }
    }

    IEnumerator MeleeAttackInterval()
    {
        performMeleeAttack = false;
        anim.SetBool("Basic Attack", true);

        yield return new WaitForSeconds(heroClassScript.heroAttackTime / ((100 + heroClassScript.heroAttackTime) * 0.01f));

        if (targetedEnemy == null)
        {
            anim.SetBool("Basic Attack", false);
            performMeleeAttack = true;
        }
    }

    public void MeleeAttack()
    {
        if (targetedEnemy != null)
        {
            if (targetedEnemy.GetComponent<TargetableScript>().enemyType == TargetableScript.EnemyType.Minion)
            {
                targetedEnemy.GetComponent<EnemyStatsScript>().enemyHealth -= heroClassScript.heroAttackDmg;
            }
        }

        performMeleeAttack = true;
    }

    void CheckAttackRange()
    {
        if (Input.GetKeyDown(checkAttackRange) && attackRange_Indicator.GetComponent<Image>().enabled == false)
            attackRange_Indicator_Image.enabled = true;
        else if (Input.GetKeyDown(checkAttackRange) && attackRange_Indicator.GetComponent<Image>().enabled == true)
            attackRange_Indicator_Image.enabled = false;
    }

    void CheckHealth()
    {
        if (heroClassScript.heroHealth <= 0)
        {
            Destroy(gameObject);
            targetedEnemy = null;
            performMeleeAttack = false;
        }
    }
}
