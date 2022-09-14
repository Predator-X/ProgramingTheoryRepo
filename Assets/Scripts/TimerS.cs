using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimerS : MonoBehaviour
{
    public GameObject player;
    private void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player");

    }

    void LateUpdate()
    {
        if (player.GetComponent<PlayerController>().isDead == false && PauseMenu.GameIsPaused==false)
        {
            GetComponent<TextMeshProUGUI>().SetText(player.GetComponent<PlayerController>().updateTimer());
        }

      


    }
}
