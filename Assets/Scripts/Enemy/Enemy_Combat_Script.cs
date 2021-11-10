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

    void Start()
    {
        attackRange_Indicator_Image = attackRange_Indicator.GetComponent<Image>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        //enemyMoveScript = GetComponent<Enemy_Move_Script>();

        // This is defunct code to scale the image of the attack range properly
        //radius = enemyAttackRange / 2.0f / 2.0f / 2.0f / 1.25f;
    }

    void Update()
    {
        //CheckAttackRange();
        //attackRange_Indicator.transform.localScale = new Vector3(radius + offset, radius + offset, radius + offset);

        // Only check for targets when none
        if (targetedEnemy == null)
            CheckAggroRadius();

        MoveToEnemy();

        CheckEnemyLeaveRadius();
    }

    // If the targeted enemy leaves aggro radius, reset
    void CheckEnemyLeaveRadius()
    {
        if (targetedEnemy != null)
        {
            if (Vector3.Distance (targetedEnemy.transform.position, transform.position) > enemyAggroRange)
            {
                targetedEnemy = null;
                enemyWithinAttackRange = false;
                transforms.Clear();
            }
            else
            if (Vector3.Distance(targetedEnemy.transform.position, transform.position) > enemyAttackRange)
            {
                enemyWithinAttackRange = false;
            }
        }
    }

    public Transform[] targets;
    public List<Transform> transforms;

    // Get all gameobjects within aggro radius, pick nearest one
    void CheckAggroRadius()
    {
        Collider[] hitCollider = Physics.OverlapSphere(gameObject.transform.position, enemyAggroRange);

        foreach (Collider col in hitCollider)
        {
            if (col.gameObject.GetComponent<TargetableScript>() != null && transforms.Contains(col.transform) == false && col.gameObject != this.gameObject)
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
            targetedEnemy = closestTarget.gameObject;
    }

    // Move the NavMesh Agent after TargetedEnemy has been selected
    void MoveToEnemy()
    {
        if (targetedEnemy != null)
        {
            if (Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) < enemyAggroRange)
            {
                agent.SetDestination(targetedEnemy.transform.position);
                agent.stoppingDistance = enemyAttackRange;

                if (Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) > enemyAttackRange && targetedEnemy != null && enemyWithinAttackRange == false)
                {
                    //agent.SetDestination(targetedEnemy.transform.position);
                    //agent.stoppingDistance = enemyAttackRange;
                    enemyWithinAttackRange = true;
                }
                else if (enemyWithinAttackRange == true)
                {
                    agent.isStopped = true;
                }
            }
        }
    }

    /* Used for drawing radius in Scene View
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyAggroRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyAttackRange);
    }
    */

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
