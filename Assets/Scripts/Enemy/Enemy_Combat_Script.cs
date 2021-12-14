using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

[System.Serializable]
public class Enemy_Combat_Script : MonoBehaviour
{
    // Enemy Init
    public enum EnemyAttackType { Melee, Ranged };
    public EnemyAttackType enemyAttackType;
    NavMeshAgent agent;

    // Combat targeting and range
    public GameObject targetedEnemy;
    [SerializeField] private float enemyAttackRange;
    [SerializeField] private float enemyRotateSpeedForAttack;

    [SerializeField] private float enemyAggroRange;

    // Combat range visuals
    [SerializeField] private Image attackRange_Indicator;
    [SerializeField] private Canvas attackRange_Canvas;
    private float offset;
    [SerializeField] private KeyCode checkAttackRange;
    private float radius;
    Image attackRange_Indicator_Image;

    // Character Animator
    private Enemy_Move_Script enemyMoveScript;
    private EnemyStatsScript enemyStatsScript;
    private Animator anim;

    // Combat Variables
    public bool basicAtkIdle = false;
    public bool isEnemyAlive;
    public bool performMeleeAttack = true;
    public bool enemyWithinAttackRange = false;

    // Not sure why this is needed
    [SerializeField] Slider PlayerHealthHUD;
    HealthSlider2D_Script healthCallRef;

    void Start()
    {
        attackRange_Indicator_Image = attackRange_Indicator.GetComponent<Image>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        enemyStatsScript = gameObject.GetComponent<EnemyStatsScript>();
        anim = gameObject.GetComponent<Animator>();
        enemyMoveScript = GetComponent<Enemy_Move_Script>();

        // This is defunct code to scale the image of the attack range properly
        //radius = enemyAttackRange / 2.0f / 2.0f / 2.0f / 1.25f;

        healthCallRef = PlayerHealthHUD.GetComponent<HealthSlider2D_Script>();
    }

    void Update()
    {
        //CheckAttackRange();
        //attackRange_Indicator.transform.localScale = new Vector3(radius + offset, radius + offset, radius + offset);

        // Only check for targets when none
        if (targetedEnemy == null && isEnemyAlive)
            CheckAggroRadius();

        if (isEnemyAlive)
        {
            MoveToEnemy();
            CheckEnemyLeaveRadius();
        }

        CheckDead();
    }

    void CheckCombat()
    {
        if (targetedEnemy != null)
        {
            if (enemyAttackType == EnemyAttackType.Melee)
            {
                FaceTarget();

                if (performMeleeAttack && enemyWithinAttackRange)
                {

                    //Debug.Log("Attack the player");

                    // Start Courotine
                    //if (targetedEnemy.GetComponent<Enemy_Combat_Script>().isEnemyAlive)
                    StartCoroutine(MeleeAttackInterval());
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
        anim.SetFloat("AttackAnimSpeed", enemyStatsScript.enemyAttackSpeed);
        anim.SetBool("Basic Attack", true);

        yield return new WaitForSeconds(enemyStatsScript.enemyAttackTime / ((100 + enemyStatsScript.enemyAttackTime) * 0.01f));

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
            float damageCalc = enemyStatsScript.enemyAttackDmg - (targetedEnemy.GetComponent<HeroClass>().heroDef * 0.1f);
            //damageCalc = Mathf.Round(damageCalc * 100f) / 100f;
            damageCalc = Mathf.Round(damageCalc);
            if (damageCalc <= 1f)
            {
                targetedEnemy.GetComponent<HeroClass>().heroHealth -= 1f;
                healthCallRef.CallHealthTrigger(targetedEnemy);
            }
            else
            {
                targetedEnemy.GetComponent<HeroClass>().heroHealth -= damageCalc;
                healthCallRef.CallHealthTrigger(targetedEnemy);
            }
        }

        performMeleeAttack = true;
    }

    void CheckDead()
    {
        if (enemyStatsScript.enemyHealth <= 0 && isEnemyAlive == true)
        {
            StartCoroutine(CallDeadAnim());
            isEnemyAlive = false;
            if (enemyMoveScript != null)
                enemyMoveScript.isEnemyAliveRef = false;
        }
    }

    IEnumerator CallDeadAnim()
    {
        //anim.SetBool("isDead", true);
        yield return new WaitForSeconds(0.01f);
        anim.SetBool("isDead", true);
    }

    public void OnDeadAnimEnd()
    {
        Destroy(gameObject);
    } 

    // If the targeted enemy leaves aggro radius, reset
    void CheckEnemyLeaveRadius()
    {
        if (targetedEnemy != null)
        {
            if (Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) > enemyAggroRange)
            {
                targetedEnemy = null;
                transforms.Clear();
                enemyWithinAttackRange = false;
                //Debug.Log("Outside of aggro range");
            }
            else
            if (Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) > enemyAttackRange)
            {
                enemyWithinAttackRange = false;
                ResetAutoAttack();
                //Debug.Log("Outside of attack range");
            }
        }
        else
            ResetAutoAttack();
    }

    public Transform[] targets;
    public List<Transform> transforms;

    // Get all gameobjects within aggro radius, pick nearest one
    void CheckAggroRadius()
    {
        Collider[] hitCollider = Physics.OverlapSphere(gameObject.transform.position, enemyAggroRange);

        foreach (Collider col in hitCollider)
        {
            if (col.gameObject.GetComponent<TargetableScript>() != null && transforms.Contains(col.transform) == false && col.gameObject != this.gameObject && col.gameObject.tag != "Enemy")
                transforms.Add(col.transform);
        }

        transforms.AddRange(targets);

        Transform closestTarget = null;
        float closestTargetDistance = float.MaxValue;
        NavMeshPath path = new NavMeshPath();

        for (int i = 0; i < transforms.Count; i++)
        {
            if (transforms[i] == null)
                continue;

            if (NavMesh.CalculatePath(transform.position, transforms[i].position, agent.areaMask, path))
            {
                float distance = Vector3.Distance(transform.position, path.corners[0]);

                for (int j = 1; j < path.corners.Length; j++)
                {
                    distance += Vector3.Distance(path.corners[j - 1], path.corners[j]);
                }

                if (distance < closestTargetDistance)
                {
                    closestTargetDistance = distance;
                    closestTarget = transforms[i];
                }
            }
        }

        if (transforms.Count != 0)
        {
            targetedEnemy = closestTarget.gameObject;
            if (enemyMoveScript != null)
                enemyMoveScript.GetNewTargetedEnemyRef(targetedEnemy);
        }
    }

    // Move the NavMesh Agent after TargetedEnemy has been selected
    void MoveToEnemy()
    {
        if (targetedEnemy != null)
        {
            if (Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) < enemyAggroRange)
            {
                agent.SetDestination(targetedEnemy.transform.position);
                agent.stoppingDistance = enemyAttackRange - 0.55f;

                if (Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) < enemyAttackRange && targetedEnemy != null && enemyWithinAttackRange == false)
                {
                    //agent.SetDestination(targetedEnemy.transform.position);
                    //agent.stoppingDistance = enemyAttackRange;
                    enemyWithinAttackRange = true;
                }
                else if (enemyWithinAttackRange == true)
                {
                    //Debug.Log("Enemy entered Attack Range");
                    CheckCombat();
                    //agent.isStopped = true;
                }
            }
        }
    }

    // Face Target
    void FaceTarget()
    {
        // Rotation ??
        Quaternion rotationToLookAt = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);
        float rotationQ = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y,
            ref enemyMoveScript.rotateVelocity,
            enemyRotateSpeedForAttack * (Time.deltaTime * 5));

        transform.eulerAngles = new Vector3(0, rotationQ, 0);
    }

    public void ResetAutoAttack()
    {
        anim.SetBool("Basic Attack", false);
        performMeleeAttack = true;
    }

    // Used for drawing radius in Scene View
    /*
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyAggroRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyAttackRange);
    }
    */

    private void OnDestroy()
    {
        transforms.Clear();
    }

    /* Used for debugging range indicator in game
    void CheckAttackRange()
    {
        if (Input.GetKeyDown(checkAttackRange) && attackRange_Indicator.GetComponent<Image>().enabled == false)
            attackRange_Indicator_Image.enabled = true;
        else if (Input.GetKeyDown(checkAttackRange) && attackRange_Indicator.GetComponent<Image>().enabled == true)
            attackRange_Indicator_Image.enabled = false;
    }
    */
}
