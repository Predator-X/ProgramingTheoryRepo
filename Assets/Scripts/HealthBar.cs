using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Scrollbar healthBar;
   GameObject player;
    // Update is called once per frame
    private void Awake()
    {
        healthBar = this.GetComponent<Scrollbar>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void LateUpdate()
    {
        healthBar.size = player.GetComponent<PlayerController>().currentHealth / 20;
        if (player.activeInHierarchy == false)
        {
            healthBar.interactable = false;
            // healthBar.enabled = false;
            healthBar.size = 1;
        }

    }
}
