//INHERITANCE - PlayerController Inherits From character class and POLYMORPHISM the move method
//This Class is used for manaching MainPlayer Character
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Character
{
    public CharacterController characterController;  //<-- characterController that is build in unity 

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


    //<<<>>> New Code for overriting move method 
    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
       
        characterController.GetComponent<CharacterController>();
        attack = this.GetComponent<ShootWithRaycast>();

        cam = GameObject.FindGameObjectWithTag("CameraPlayer").GetComponent<Camera>();

        onStartGameObjectsInScene = GameObject.FindGameObjectsWithTag("Enemy");

    }

    // POLYMORPHISM
    protected override void Move(GameObject head, GameObject gun, GameObject body)  //<<<|>>> New Code added_________________________________________________________ to Move() method in character class by base.Move();
    {                    

        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (characterController.isGrounded)    
        {
            velocity.y = - 1f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = jumpForce;
            }
        }
        else
        {
            velocity.y -= gravity * -2f * Time.deltaTime;     // <--to calculate gravity : y -= gravity * -2f * Time.deltatime; but calculate only on ground 
        }

        Vector3 moveVector = transform.TransformDirection(moveInput);

        characterController.Move(moveVector * CurrentSpeed * Time.deltaTime);
        characterController.Move(velocity * Time.deltaTime);
        base.Move(head, gun, body);                   //<-- POLYMORPHISM adding code to Move() method that inherits from character class 


 
    }


    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            Move(head, gun, body);
            attackAndCameraManagmentOnInput();
           
        }
     
    }


   // | ABSTRACTION
  //  V
    private void attackAndCameraManagmentOnInput()
    {
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
            CurrentSpeed = runSpeed;
        }
        if (Input.GetButtonUp("SpeedUp"))
        {
            CurrentSpeed = _speed;
        }
    }

    public string updateTimer()
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

       return string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    public void SetScore(int scoreset)
    {
        score = scoreset;
    }

    public void AddScore(int sc)
    {
        score += sc;
    }

 
    public int GetScore()
    {
        return score;
    }

    public void SetTime(float timeset)
    {
        currentTime = timeset;
    }
    public float GetTime()
    {
        return currentTime;
    }
}