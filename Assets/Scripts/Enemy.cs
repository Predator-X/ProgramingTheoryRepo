using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
   public NavMeshAgent agent;
  public  Transform player;
    public LayerMask WhatIsGround, WhatIsPlayer;
    //Patrolling
    Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    //Attacking
    public float timeBetweenAttacks;
    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange,alreadyAttacked;

    private void Awake()
    {//Find Player
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.GetComponent<NavMeshAgent>();    
    }

    void Update()
    {//Check for Sight and Attack Range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);
        
        if (!playerInSightRange & !playerInAttackRange) Patrolling();
        if (playerInSightRange & !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
        
    }

    void Patrolling()
    {
        if (!walkPointSet) SearchWalkingPoint();
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
          
            //Calculate Distance to walkpoint
            Vector3 distanceToWalkPoint = transform.position - walkPoint;
            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
        }
    }

    void SearchWalkingPoint()
    {//Calculate random point Range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //Check if Walking Point is not outside of the map
        if(Physics.Raycast(walkPoint, -transform.up, 2f, WhatIsGround))  walkPointSet = true; 


    }

    void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
  
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void AttackPlayer()
    {
        //Make sure enemy Does not move
        agent.SetDestination(transform.position);
        transform.LookAt(player.transform);

        if (!alreadyAttacked)
        {//Attack Code Here
         //
         //
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }
}
