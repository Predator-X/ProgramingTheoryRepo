//This Script manages Checkpiont, next scene or end game as
//player collides with it depends on TAG that have been set on game object with it 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndSceneCollider : MonoBehaviour
{


    GameObject gameManager;


    private void OnTriggerEnter(Collider collision)
    {
       
        if (this.gameObject.tag=="NextSceneCollider" && collision.gameObject.name == "MainPlayer")
        {
            Scene scene = SceneManager.GetActiveScene();
            SaveSystem.SavePlayer(collision.gameObject.GetComponent<PlayerController>(),scene.buildIndex);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<SavingAndLoading>().LoadNextScene();
         
           
        }

        if(this.gameObject.tag==("EndSceneCollider") && collision.gameObject.name == "MainPlayer")
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager");
            
            gameManager.GetComponent<PauseMenu>().EndGamePause();

            gameManager.GetComponent<PauseMenu>().SavePlayer();
          
            StartCoroutine(AfterCreditsLoadMainMenu());
         
        }
        if (this.gameObject.tag == "thisisCheckpoint" && collision.gameObject.name == "MainPlayer") 
        {
             //Scene scene = SceneManager.GetActiveScene();
            // SaveSystem.SavePlayer(collision.gameObject.GetComponent<PlayerController>(), scene.buildIndex);

            //>>> using method from PauseMenu as its saves to binary and  ((json) as it updates the score) 
            gameManager = GameObject.FindGameObjectWithTag("GameManager");
            gameManager.GetComponent<PauseMenu>().SavePlayer();
           
         //Seting CheckPoint UI
            GameObject obj = gameManager.GetComponent<PauseMenu>().GetCheckPointUI();
            
            gameManager.GetComponent<PauseMenu>().CheckPointReachedt();
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            this.gameObject.SetActive(false);
        }
    }
   

    IEnumerator AfterCreditsLoadMainMenu()
    {
        yield return new WaitForSeconds(10);
        gameManager.GetComponent<PauseMenu>().Resume();
        gameManager.GetComponent<PauseMenu>().SetMainMenuON();
        gameManager.GetComponent<SavingAndLoading>().LoadMenu();

    }
}
