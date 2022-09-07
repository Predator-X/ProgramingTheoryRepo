using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    public CharacterController characterController;


 
    // Start is called before the first frame update
    void Start()
    {
        characterController.GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {

        Move(head,gun,body);
     

    }



}





