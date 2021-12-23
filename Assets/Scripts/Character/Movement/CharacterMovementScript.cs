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

    public bool abilityCasting = false;

    float prevX;
    float prevZ;

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
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon && heroCombatScript.notCasting)
        {
            transform.localRotation = Quaternion.LookRotation(new Vector3(agent.velocity.normalized.x, 0, agent.velocity.normalized.z));
            prevX = agent.velocity.normalized.x;
            prevZ = agent.velocity.normalized.z;
        }
    }

    IEnumerator ResetStopMovement()
    {
        yield return new WaitForSeconds(0.2f);
        agent.isStopped = false;
        abilityCasting = false;
    }

    void CheckStopMovement()
    {
        if (Input.GetKeyDown(stopKeycode))
        {
            JustStopMovement(false);
        }
    }

    public void JustStopMovement(bool _abilityCasting)
    {
        //heroCombatScript.targetedEnemy = null;
        agent.isStopped = true;
        agent.SetDestination(transform.position);
        abilityCasting = _abilityCasting;

        StartCoroutine(ResetStopMovement());
    }
}
