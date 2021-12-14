using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Move_Script : MonoBehaviour
{
    public NavMeshAgent agent;
    private Enemy_Combat_Script enemyCombatScript;
    private Animator anim;

    float motionSmoothTime = .075f;
    public float rotateVelocity;

    // Optimization
    public GameObject targetedEnemyRef;
    public bool isEnemyAliveRef;


    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemyCombatScript = gameObject.GetComponent<Enemy_Combat_Script>();
        isEnemyAliveRef = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfNullOrDead();

        #region Old Code
        // Check if the player has a target
        /*if (enemyCombatScript.targetedEnemy != null)
        {
            if (enemyCombatScript.targetedEnemy.GetComponent<Enemy_Combat_Script>() != null)
            {
                if (enemyCombatScript.targetedEnemy.GetComponent<Enemy_Combat_Script>().isEnemyAlive != true)
                {
                    enemyCombatScript.targetedEnemy = null;
                }
            }
        }*/
        #endregion
    }

    private void CheckIfNullOrDead()
    {
        // Check if the player has a target
        if (enemyCombatScript.targetedEnemy != null)
        {
            if (targetedEnemyRef != null)
            {
                if (isEnemyAliveRef != true)
                {
                    enemyCombatScript.targetedEnemy = null;
                    agent.isStopped = true;
                    BoxCollider bc = GetComponent<BoxCollider>();
                    bc.enabled = false;
                }
            }
        }
    }

    public void GetNewTargetedEnemyRef(GameObject _targetedEnemyRef)
    {
        targetedEnemyRef = _targetedEnemyRef;
    }

    public void TellIfDead()
    {
        isEnemyAliveRef = false;
    }

    private void LateUpdate()
    {
        float speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);

        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);

        if (speed == 0 && enemyCombatScript.targetedEnemy != null)
        {
            //if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
                //transform.rotation = Quaternion.LookRotation(enemyCombatScript.targetedEnemy.transform.position);
        }
    }
}
