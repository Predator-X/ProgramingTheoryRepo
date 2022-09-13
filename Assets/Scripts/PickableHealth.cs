using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableHealth : MonoBehaviour
{
    public float healAmount = 7f;
    public GameObject holder;
 private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<Character>().Heal(healAmount);
            Destroy(holder);
        }
    }
}
