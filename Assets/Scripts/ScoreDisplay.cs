using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreDisplay : MonoBehaviour
{
   public TextMeshPro scoreText;
   public  GameObject player;


    private void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player");
 
    }

    void LateUpdate()
    {

        GetComponent<TextMeshProUGUI>().SetText("Score:" + player.GetComponent<PlayerController>().GetScore());


    }
}


