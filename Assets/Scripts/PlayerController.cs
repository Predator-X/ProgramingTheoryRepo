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

    //**************CharacterController Updating Method if not works please delete this
    protected override void Move(GameObject head, GameObject gun, GameObject body)
    {
        

        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        //<<<>>>! This Is Deleted becouse of new Code -->  transform.Translate(moveInput * currentSpeed * Time.deltaTime, Space.Self);
        // characterController.Move(move * speed * Time.deltaTime);

        //<<<>>> New Code |_______________________________________________________________
        //                  V

        // to calculate gravity : y -= gravity * -2f * Time.deltatime; but calculate only on ground

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
            velocity.y -= gravity * -2f * Time.deltaTime;
        }

        Vector3 MoveVector = transform.TransformDirection(moveInput);

        characterController.Move(MoveVector * currentSpeed * Time.deltaTime);
        characterController.Move(velocity * Time.deltaTime);

      //____________________End Of new Code_________________________________________!!!

        body.transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensityvityX);
        // transform.rotation = body.transform.rotation;
        head.transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * mouseSensityvityY);         // this when using scirpt CameraFallow
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensityvityX);
        //  gun.transform.rotation = head.transform.rotation;
        gun.transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * mouseSensityvityY);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            Move(head, gun, body);// MoveThatWorked
           // MoveByRigidbody();
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




