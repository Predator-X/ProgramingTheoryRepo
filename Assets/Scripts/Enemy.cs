using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
   public NavMeshAgent agent;
  public  GameObject player;
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
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        agent.GetComponent<NavMeshAgent>();    
    }

    private void Update()
    {//Check for Sight and Attack Range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, 6);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, 6);

        if (!playerInSightRange & !playerInAttackRange) ;
        
    }

    private void Patrolling()
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
        if(Physics.Raycast(walkPoint, -transform.up, 2f, 6))  walkPointSet = true; 


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
    {//Attack Code Here
        //
        //
        //Make sure enemy Does not move
        agent.SetDestination(transform.position);
        transform.LookAt(player.transform);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }
}
