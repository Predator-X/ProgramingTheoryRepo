
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadBase : MonoBehaviour
{
    //For Loading
    public GameObject loadingSceen;
    public Slider slider;
    public Text progressText;
    bool loadDone = false,loadfromSave=false;
    public int sceneIndex;
 
    public void Load(int sceneIndex)
    {
        loadingSceen.active = true;
        loadDone = false;
        StartCoroutine(LoadAsynchronously(sceneIndex,false));
    }

    public void LoadLastSaveScene()
    {
        loadingSceen.active = true;
        loadDone = false;
       StartCoroutine(LoadAsynchronously( SaveSystem.LoadPlayer().sceneIndexx,true));

    }

    IEnumerator LoadAsynchronously(int sceneIdnex,bool fromSave)
    {
        
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
}
