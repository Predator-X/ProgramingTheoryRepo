//INHERITANCE - Enemy Inherits From character class 
//Manages Enemy
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{   //For Targeting Player
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
    public bool playerInSightRange, playerInAttackRange,alreadyAttacked , obstacleInWay=false;

    //Shooting
    ShootWithRaycast attack;
    private float nextFire;

    //Dead effect
  public bool  enemyIsHitOnHead=false;

    private void Awake()
    {//Find Player
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.GetComponent<NavMeshAgent>();

        attack = this.GetComponent<ShootWithRaycast>();

        SaveSystem.getEnemysOnStart = GameObject.FindGameObjectsWithTag("Enemy");

    }

    void Update()
    {//Check for Sight and Attack Range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);

       

        if (!playerInSightRange & !playerInAttackRange ) Patrolling();
        if (playerInSightRange || obstacleInWay & !playerInAttackRange  ) ChasePlayer();
        if (playerInSightRange && playerInAttackRange & !obstacleInWay) AttackPlayer();


        //Behafiour  when enemy standing in fornt of the wall do this ....
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, attackRange ))
        {

            if (hit.transform.tag == "Wall")
            {
                obstacleInWay = true;  //<-- this will make go back in update to chase the player
            }
            else if (hit.transform.tag != "Wall") { obstacleInWay = false; }
        }

    }


    public override void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
     

        if (currentHealth <= 0 && enemyIsHitOnHead)
        {
            GameObject body, gun, head;
            body = this.transform.Find("Body").gameObject;
            body.GetComponent<DisActivateAfter>().enabled = true;
            body.AddComponent<Rigidbody>();
            body.transform.parent = null;
         

            gun = this.transform.Find("GunHolder").gameObject;
            gun.GetComponent<DisActivateAfter>().enabled = true;
            gun.AddComponent<Rigidbody>();
            gun.transform.parent = null;

           
            head = this.transform.Find("HeadShooted").gameObject;
            head.GetComponent<DisActivateAfter>().enabled = true;
            head.AddComponent<Rigidbody>();
            head.transform.parent = null;

            isDead = true;
            gameObject.SetActive(false);


        }
        else if(currentHealth <=0 && enemyIsHitOnHead == false)
        {
            isDead = true;
            gameObject.SetActive(false);
        }

    }

    public virtual void EnemyIsHitOnHead(bool gotHit)
    {
        enemyIsHitOnHead = gotHit;
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
            if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false; obstacleInWay = false;
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
            Transform pointGun = transform.Find("GunHolder");
            pointGun.transform.LookAt(player.transform); 

            attack.Shoot();

            //
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }
}