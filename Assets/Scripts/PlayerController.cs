using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    public CharacterController characterController;

    //Shooting
    ShootWithRaycast attack;
    private float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        characterController.GetComponent<CharacterController>();
        attack = GetComponent<ShootWithRaycast>();

    }

    // Update is called once per frame
    void Update()
    {


        Move(head,gun,body);

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            attack.Shoot();
        }
      
     

    }



}





