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
            Debug.Log(hit.transform.gameObject);
            if (hit.transform.tag == "Wall")
            {
                obstacleInWay = true;
            }
            else if (hit.transform.tag != "Wall") { obstacleInWay = false; }
        }

    }


    public override void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Name: " + gameObject.name + " HasLife: " + currentHealth);

        if (currentHealth <= 0 && enemyIsHitOnHead)
        {
            GameObject b, g, h;
            b = this.transform.Find("Body").gameObject;
            b.GetComponent<DisActivateAfter>().enabled = true;
            b.AddComponent<Rigidbody>();
            b.transform.parent = null;
         

            g = this.transform.Find("GunHolder").gameObject;
            g.GetComponent<DisActivateAfter>().enabled = true;
            g.AddComponent<Rigidbody>();
            g.transform.parent = null;

            //h = this.transform.Find("Head").gameObject;  HeadShooted
            h = this.transform.Find("HeadShooted").gameObject;
            h.GetComponent<DisActivateAfter>().enabled = true;
            h.AddComponent<Rigidbody>();
            h.transform.parent = null;

           // PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
           
            //StartCoroutine(player.DeadDellay(b));
            //StartCoroutine(player.DeadDellay(h));

        
            
            

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


/*
 * 
 *        //Behafiour  when enemy standing in fornt of the wall do this ....
        
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 7))
        {
            Debug.Log(hit.transform.gameObject);
            if (hit.transform.tag == "Wall")
            {
                obstacleInWay = true;
                Patrolling();
                AttackPlayer();
            }
            else { obstacleInWay = false; }
        }
        if (attack.IsItShootingAtObstacle() && playerInSightRange)
        {
            obstacleInWay = true;
            Patrolling();
            ChasePlayer();
            //  Invoke("AttackPlayer", Time.deltaTime * 3f);
        }
        if (!attack.IsItShootingAtObstacle())
        {
            obstacleInWay = false;
        }
*/