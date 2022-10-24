using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
  //  public CharacterController characterController;          //setting mouse sensityfity multiplication
    public float currentSpeed , speed = 5f , runSpeed = 15f, mouseSensityvityX = 10f, mouseSensityvityY = 10f  ;
    public GameObject head, gun, body;
    //  bool isPlayer = false;
    public bool isDead = false;

    //health
    public float currentHealth =10;

 
    public float jumpForce;


    private void Awake()
    {
        currentSpeed = speed;
       //currentHealth = maxHealth;
   
    }


    public virtual void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Name: " + gameObject.name + " HasLife: " + currentHealth);

        if (currentHealth <= 0)
        {
        
            isDead = true;
            gameObject.SetActive(false);


        }

    }

  

  protected virtual void Move(GameObject head ,GameObject gun, GameObject body)
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(moveInput * currentSpeed * Time.deltaTime, Space.Self);
        // characterController.Move(move * speed * Time.deltaTime);

        body.transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensityvityX);
                                                                                     // transform.rotation = body.transform.rotation;
        head.transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * mouseSensityvityY);         // this when using scirpt CameraFallow
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensityvityX);
                                                                                     //  gun.transform.rotation = head.transform.rotation;
        gun.transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * mouseSensityvityY);
    }

 

    public virtual void Heal(float healAmount)
    {
        currentHealth += healAmount; 
    }

   public  IEnumerator DeadDellay(GameObject g)
    {
        

        yield return new WaitForSeconds(4);
        Destroy(g);
        Destroy(gameObject);
    }


    public void SetHealth(float healthset)
    {
        currentHealth = healthset;
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}



/*
     //CameraFallow
    public Camera playersCam;
    public Transform camtarget;
    public float pLerp = .02f,
        rLerp = .01f;

//CameraFallow
//   camtarget = transform.Find("Head").GetChild(0);
//  if (playersCam == null)
//  {
//      playersCam = GameObject.FindGameObjectWithTag("CameraPlayer").GetComponent<Camera>();
//    }



   void CameraFallow()
{
   playersCam.transform.position = Vector3.Lerp(playersCam.transform.position, camtarget.position, pLerp);
   transform.rotation = Quaternion.Lerp(playersCam.transform.rotation, camtarget.rotation, rLerp);
}
*/

/*
 *      if(this.tag == "Player")
    {
        isPlayer = true;
        Scrollbar healthBar = Scrollbar.FindObjectOfType<Scrollbar>();

    }
 * 
 * 
 * // detach head on dead 
           
            if (this.tag == "Enemy")
            {
                Transform head = transform.GetChild(1);
                head.parent = null;
                head.gameObject.SetActive(true);
                isDead = true;
                gameObject.SetActive(false);
            }
            else
            {
                isDead = true;
                gameObject.SetActive(false);
            }

 *      
                        if (this.tag == "Enemy")
                        {
                            Transform head = transform.GetChild(1);
                            head.parent = null;
                            head.gameObject.SetActive(true);
                          //  DeadDellay(head.gameObject);
                            head.gameObject.AddComponent<Rigidbody>();
                          //  head.gameObject.AddComponent < Enemy > ().DeadDellay(head.gameObject); 

                            isDead = true;
                           gameObject.SetActive(false);



                        }
                        else
                        {
                            isDead = true;
                            gameObject.SetActive(false);
                        }
                      









        if(this.tag == "Enemy")
            {
                Transform gunHolder = transform.Find("GunHolder");
                if (gunHolder != null)
                {
                    gunHolder.gameObject.AddComponent<Rigidbody>();
                    gunHolder.parent = null;
                }
                else { Debug.LogError("Did not find GunHolder In Character to detach on death--- Character c#"); }

                isDead = true;
                gameObject.SetActive(false);
            }
            else
            {
            }
 *
 */

