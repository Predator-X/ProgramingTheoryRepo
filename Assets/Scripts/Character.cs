using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 5f;
    public GameObject head , gun, body;

   //



    // Start is called before the first frame update
    void Start()
    {
        characterController.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(move * speed * Time.deltaTime, Space.Self);
       // characterController.Move(move * speed * Time.deltaTime);

        body.transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 5f);
       // transform.rotation = body.transform.rotation;
        head.transform.Rotate(Vector3.left  * Input.GetAxis("Mouse Y") * 5f);
        //  gun.transform.rotation = head.transform.rotation;
        gun.transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * 5f);


    }

  
}
