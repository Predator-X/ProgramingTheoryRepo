//Canvas2 is GameManager
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
    // Start is called before the first frame update

    /*
    public void Start()
    {
        SavingAndLoading s = GetComponent<SavingAndLoading>();
            s.LoadPlayer();
            s.LoadEnemys();
    }
    */
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
}
