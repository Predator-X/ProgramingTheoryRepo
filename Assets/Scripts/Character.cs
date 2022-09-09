using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
  //  public CharacterController characterController;
    public float currentSpeed , speed = 5f , runSpeed = 15f;
    public GameObject head, gun, body;
  //  bool isPlayer = false;
    public bool isDead = false;

    //health
    public float currentHealth =10;

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
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(move * currentSpeed * Time.deltaTime, Space.Self);
        // characterController.Move(move * speed * Time.deltaTime);

        body.transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 5f);
                                                                                     // transform.rotation = body.transform.rotation;
        head.transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * 5f);         // this when using scirpt CameraFallow
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 5f);
                                                                                     //  gun.transform.rotation = head.transform.rotation;
        gun.transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * 5f);
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
 * 
 */

