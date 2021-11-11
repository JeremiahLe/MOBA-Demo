using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovementScript : MonoBehaviour
{
    public NavMeshAgent agent;
    private HeroCombat heroCombatScript;

    [SerializeField] private KeyCode stopKeycode;

    public float rotateSpeedMovement = 0.075f;
    public float rotateVelocity;

    // Start is called before the first frame update
    void Start()
    {
        // GetComponents Initialization
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        heroCombatScript = GetComponent<HeroCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckStopMovement();

        // Check if the player has a target
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
                    // Move player NavAgent to raycast
                    agent.SetDestination(hit.point);
                    heroCombatScript.targetedEnemy = null;
                    agent.stoppingDistance = 0;
                }
                
                // Old Player NavAgent Rotation Script
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

    // Adjusting player NavAgent rotation
    private void LateUpdate()
    {
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
    }

    IEnumerator ResetStopMovement()
    {
        yield return new WaitForSeconds(0.3f);
        agent.isStopped = false;
    }

    void CheckStopMovement()
    {
        if (Input.GetKeyDown(stopKeycode))
        {
            agent.updateRotation = false;
            heroCombatScript.targetedEnemy = null;

            agent.isStopped = true;
            agent.SetDestination(transform.position);

            StartCoroutine(ResetStopMovement());
        }
    }

    public void JustStopMovement()
    {
        StartCoroutine(ResetStopMovement());
    }
}
