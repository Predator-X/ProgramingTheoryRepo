//Character is a Parent class 
//contains health,take damage , heal, move and dead dellay 
// sets the basics for (inheritance) characters controller like playerController, enemy 
//>> Here you can set after what time dead dellay (game object gets destroyed from scene
// Move() method only using mouse to rotate as it might be inherited for other characters like tank 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{                                                            // | setting mouse sensityfity multiplication    |
                                                            //  v                                             v
    public float currentSpeed , speed = 5f , runSpeed = 15f,    mouseSensityvityX = 10f, mouseSensityvityY = 10f  ;
    public GameObject head, gun, body;

    public bool isDead = false;

    //health
    public float currentHealth =10;

 
    public float jumpForce;


    private void Awake()
    {
        currentSpeed = speed;
   
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

  

  protected virtual void Move(GameObject head ,GameObject gun, GameObject body)             //<-- here only rotate as it might be used for tank or other character
    {
                             
        body.transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensityvityX);                         
                                                                                    
        head.transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * mouseSensityvityY);         
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensityvityX);
                                                                                    
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
