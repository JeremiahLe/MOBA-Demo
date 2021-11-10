using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Move_Script : MonoBehaviour
{
    public NavMeshAgent agent;
    private Enemy_Combat_Script enemyCombatScript;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemyCombatScript = gameObject.GetComponent<Enemy_Combat_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player has a target
        if (enemyCombatScript.targetedEnemy != null)
        {
            if (enemyCombatScript.targetedEnemy.GetComponent<Enemy_Combat_Script>() != null)
            {
                if (!enemyCombatScript.targetedEnemy.GetComponent<Enemy_Combat_Script>().isEnemyAlive)
                {
                    enemyCombatScript.targetedEnemy = null;
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);

        float speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("speed", speed);

        if (speed == 0 && enemyCombatScript.targetedEnemy != null)
        {
            //if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
                //transform.rotation = Quaternion.LookRotation(enemyCombatScript.targetedEnemy.transform.position);
        }
    }
}
