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
    public bool moveToEnemy = false;

    public GameObject prevEnemyRef;

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
            if (Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) > heroAttackRange) // if otuside range, move inside range
            {
                if (moveToEnemy == true)
                {
                    moveScript.agent.SetDestination(targetedEnemy.transform.position);
                    moveScript.agent.stoppingDistance = heroAttackRange;
                }

            }
            else if (Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) < heroAttackRange && moveToEnemy == true) // once inside range, and can move
            {
                if (heroAttackType == HeroAttackType.Melee)
                {
                    FaceTarget();

                    if (performMeleeAttack)
                    {

                        //Debug.Log("Attack the minion");

                        // Start Courotine
                        //if (targetedEnemy.GetComponent<Enemy_Combat_Script>().isEnemyAlive)
                            StartCoroutine(MeleeAttackInterval());
                    }
                }
            }
        }
        else if (targetedEnemy == null)
        {
            anim.SetBool("Basic Attack", false); // fix ghost autos?
            performMeleeAttack = true;
        }
    }

    IEnumerator MeleeAttackInterval()
    {

        performMeleeAttack = false;
        anim.SetFloat("AttackAnimSpeed", heroClassScript.heroAttackSpeed);
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
            //if (targetedEnemy.GetComponent<TargetableScript>().enemyType == TargetableScript.EnemyType.Minion)
            //if (targetedEnemy == prevEnemyRef)
            //if (moveToEnemy)

            float damageCalc = heroClassScript.heroAttackDmg - (targetedEnemy.GetComponent<EnemyStatsScript>().enemyDef * 0.1f);
            damageCalc = Mathf.Round(damageCalc);

            if (damageCalc <= 1f)
            {
                targetedEnemy.GetComponent<EnemyStatsScript>().enemyHealth -= 1f;
            }
            else
            {
                targetedEnemy.GetComponent<EnemyStatsScript>().enemyHealth -= damageCalc;
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

    void FaceTarget()
    {
        // Rotation ??
        Quaternion rotationToLookAt = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
            rotationToLookAt.eulerAngles.y,
            ref moveScript.rotateVelocity,
            heroRotateSpeedForAttack * (Time.deltaTime * 5));

        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }

    public void ResetAutoAttack()
    {
        anim.SetBool("Basic Attack", false);
        performMeleeAttack = true;
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, heroAttackRange);
    //}
}
