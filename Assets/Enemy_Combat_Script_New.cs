using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Combat_Script_New : MonoBehaviour
{
    // Enemy init
    public enum HeroAttackType { Melee, Ranged };
    public HeroAttackType heroAttackType;

    // Other init
    public NavMeshAgent agent;
    public Transform targetPos;
    public LayerMask whatIsGround, whatIsPlayer;

    // Pathfinding
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    public float sightRange, attackRange;
    public bool targetInSightRange, targetInAttackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
