using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovementScript : MonoBehaviour
{
    public NavMeshAgent agent;

    private HeroCombat heroCombatScript;

    //public float rotateSpeedMovement = 0.075f;
    //float rotateVelocity;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        heroCombatScript = GetComponent<HeroCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (heroCombatScript.targetedEnemy != null)
        {
            if (heroCombatScript.targetedEnemy.GetComponent<HeroCombat>() != null)
            {
                if (!heroCombatScript.targetedEnemy.GetComponent<HeroCombat>().isHeroAlive)
                {
                    heroCombatScript.targetedEnemy = null;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Floor")
                {
                    // move player to raycast
                    agent.SetDestination(hit.point);
                    heroCombatScript.targetedEnemy = null;
                    agent.stoppingDistance = 0;
                }
                

                //rotation
                //Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                //float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                    //rotationToLookAt.eulerAngles.y,
                    //ref rotateVelocity,
                    //rotateSpeedMovement * (Time.deltaTime * 5));

                //transform.eulerAngles = new Vector3(0, rotationY, 0);
            }
        }
    }

    private void LateUpdate()
    {
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
    }
}
