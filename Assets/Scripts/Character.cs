using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
  //  public CharacterController characterController;
    public float speed = 5f;
    public GameObject head, gun, body;

    //health
    public  int currentHealth = 3;



    public virtual void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

  protected virtual void Move(GameObject head ,GameObject gun, GameObject body)
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(move * speed * Time.deltaTime, Space.Self);
        // characterController.Move(move * speed * Time.deltaTime);

        body.transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 5f);
                                                                                     // transform.rotation = body.transform.rotation;
      //  head.transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * 5f); this when using scirpt CameraFallow
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
