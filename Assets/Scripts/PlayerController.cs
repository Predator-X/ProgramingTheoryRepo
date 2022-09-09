using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    public CharacterController characterController;

    //Shooting
    ShootWithRaycast attack;
    private float nextFire;
    Camera cam;
    public GameObject aimCamera, fallowCamera;
    //Effect on camera
    Cinemachine.CinemachineImpulseSource impulseSource;

    // Start is called before the first frame update
    void Start()
    {
        
        characterController.GetComponent<CharacterController>();
        attack = GetComponent<ShootWithRaycast>();

        cam = GameObject.FindGameObjectWithTag("CameraPlayer").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {


        Move(head,gun,body);

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            attack.Shoot();
            impulseSource = GetComponent<Cinemachine.CinemachineImpulseSource>();
            impulseSource.GenerateImpulse(cam.transform.forward);
        }

        if(Input.GetButtonDown("Fire2") && fallowCamera.activeInHierarchy)
        {
            fallowCamera.SetActive(false);
            aimCamera.SetActive(true);
        }
        if(Input.GetButtonUp("Fire2") && aimCamera.activeInHierarchy)
        {
            aimCamera.SetActive(false);
            fallowCamera.SetActive(true);

        }
      
     

    }



}





