using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndSceneCollider : MonoBehaviour
{
    //For Loading
    /*
    public GameObject loadingSceen;
    public Slider slider;
    public Text progressText;
    bool loadDone = false, loadfromSave = false;
    public int sceneIndex;
    */
    // Start is called before the first frame update

    GameObject canvas2;


    private void OnTriggerEnter(Collider collision)
    {
        // Debug.Log("################" + collision.gameObject.name);  this.gameObject.tag!="EndSceneCollider" && this.gameObject.tag!="thisisCheckpoint"
        if (this.gameObject.tag=="NextSceneCollider" && collision.gameObject.name == "MainPlayer")
        {
            Scene scene = SceneManager.GetActiveScene();
            SaveSystem.SavePlayer(collision.gameObject.GetComponent<PlayerController>(),scene.buildIndex);
            GameObject.FindGameObjectWithTag("Canvas2").GetComponent<SavingAndLoading>().LoadNextScene();//.LoadSpecificScene(scene.buildIndex + 1 , false);
          //  Load(sceneIndex);
           
        }

        if(this.gameObject.tag==("EndSceneCollider") && collision.gameObject.name == "MainPlayer")
        {
             canvas2 = GameObject.FindGameObjectWithTag("Canvas2");
            // canvas2.GetComponent<PauseMenu>().Pause();
            canvas2.GetComponent<PauseMenu>().EndGamePause();

            canvas2.GetComponent<PauseMenu>().SavePlayer();
           // Scene scene = SceneManager.GetActiveScene();
           // SaveSystem.SavePlayer(collision.gameObject.GetComponent<PlayerController>(), scene.buildIndex);
            StartCoroutine(AfterCreditsLoadMainMenu());
         
        }
        if (this.gameObject.tag == "thisisCheckpoint" && collision.gameObject.name == "MainPlayer") 
        {
             Scene scene = SceneManager.GetActiveScene();
             SaveSystem.SavePlayer(collision.gameObject.GetComponent<PlayerController>(), scene.buildIndex);

            //>>> using method from PauseMenu as its saves to binary and  ((json) as it updates the score) 
            canvas2 = GameObject.FindGameObjectWithTag("Canvas2");
          //  canvas2.GetComponent<PauseMenu>().SavePlayer();
            GameObject obj = canvas2.GetComponent<PauseMenu>().GetCheckPointUI();
            // obj.SetActive(true);
            // StartCoroutine(activeAndDisActiveForXTimeGameObject(obj, 3f)); // play and show CheckPoint for 3 sec 
            canvas2.GetComponent<PauseMenu>().CheckPointReachedt();
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            this.gameObject.active = false;
        }
    }
    /*
    IEnumerator activeAndDisActiveForXTimeGameObject(GameObject obj, float forTime)
    {
        obj.active = true;
        yield return new WaitForSeconds(forTime);
        obj.active = false;
        Destroy(this.gameObject);
    }
    */

    IEnumerator AfterCreditsLoadMainMenu()
    {
        yield return new WaitForSeconds(10);
        canvas2.GetComponent<PauseMenu>().Resume();
        canvas2.GetComponent<PauseMenu>().SetMainMenuON();
        canvas2.GetComponent<SavingAndLoading>().LoadMenu();

    }
    /*
    public void Load(int sceneIndex)
    {
        loadingSceen.active = true;
        loadDone = false;
        StartCoroutine(LoadAsynchronously(sceneIndex, false));
    }

    public void LoadLastSaveScene()
    {
        loadingSceen.active = true;
        loadDone = false;
        StartCoroutine(LoadAsynchronously(SaveSystem.LoadPlayer().sceneIndexx, true));

    }

    IEnumerator LoadAsynchronously(int sceneIdnex, bool fromSave)
    {
        //Time.timeScale = 0f;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIdnex);
        loadingSceen.SetActive(true);


        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
        if (operation.isDone)
        {
            loadingSceen.SetActive(false);
            loadingSceen = GameObject.FindGameObjectWithTag("LoadScreen");
            if (fromSave)
            {
                this.GetComponent<SavingAndLoading>().FindEnemys();
                this.GetComponent<SavingAndLoading>().LoadPlayer();
            }


            loadingSceen.SetActive(false);



            Time.timeScale = 1.0f;

        }

    }
    */
}
