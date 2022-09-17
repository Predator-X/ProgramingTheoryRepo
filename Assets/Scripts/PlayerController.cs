using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Character
{
    public CharacterController characterController;

    //Shooting
    ShootWithRaycast attack;
    private float nextFire;
    Camera cam;
    public GameObject aimCamera, fallowCamera;
    public int score;

    //Timer
   public float currentTime = 0;
    string text;

    //get all Enemys in scene for Saving
    GameObject[] onStartGameObjectsInScene;
  

    //Effect on camera
    Cinemachine.CinemachineImpulseSource impulseSource;

    // Start is called before the first frame update
    void Start()
    {
       
        characterController.GetComponent<CharacterController>();
        attack = this.GetComponent<ShootWithRaycast>();

        cam = GameObject.FindGameObjectWithTag("CameraPlayer").GetComponent<Camera>();

        onStartGameObjectsInScene = GameObject.FindGameObjectsWithTag("Enemy");

    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            Move(head, gun, body);

            if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
            {
                attack.Shoot();
                impulseSource = GetComponent<Cinemachine.CinemachineImpulseSource>();
                impulseSource.GenerateImpulse(cam.transform.forward);
            }

            if (Input.GetButtonDown("Fire2") && fallowCamera.activeInHierarchy)
            {
                fallowCamera.SetActive(false);
                aimCamera.SetActive(true);
            }
            if (Input.GetButtonUp("Fire2") && aimCamera.activeInHierarchy)
            {
                aimCamera.SetActive(false);
                fallowCamera.SetActive(true);

            }

            if (Input.GetButtonDown("SpeedUp"))
            {
                currentSpeed = runSpeed;
            }
            if (Input.GetButtonUp("SpeedUp"))
            {
                currentSpeed = speed;
            }
        }
     
    }

  /*  
    public GameObject[] DetectEnemysOnStartInScene()
    {
        //      return GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject))
        {


        }

        
        //yield return null; 

    }

    */

    public void AddScore(int sc)
    {
        score += sc;
    }

    public int GetScore()
    {
        return score;
    }

    public string updateTimer()
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

       return string.Format("{0:00} : {1:00}", minutes, seconds);
    }


}



/*
 * 
 * 
 *    public override void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            healthBar.interactable = false;
            healthBar.size = 1;
          
            gameObject.SetActive(false);
        }

*/




