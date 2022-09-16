using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public GameObject loadingSceen;
    public Slider slider;
    public Text progressText;
    PauseMenu pauseMenuScript;

    bool loadDone = false;

    Scene scene;

    public static LoadLevel Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
            GameObject.FindGameObjectWithTag("Canvas2").GetComponent<PauseMenu>().SetLoadLevel();
            Instance = this;
            DontDestroyOnLoad(gameObject);  
    }
    private void Start()
    {

        loadingSceen = GameObject.FindGameObjectWithTag("LoadScreen");
        slider = GameObject.FindGameObjectWithTag("LoadSlider").GetComponent<Slider>();
        progressText = GameObject.FindGameObjectWithTag("LoadText").GetComponent<Text>();
        loadingSceen.SetActive(false);
    }

    public void Load()//(int sceneIndex)
    {
        loadDone = false;
     //////////  // scene = SceneManager.GetActiveScene();
        //if(scene.buildIndex != sceneIndex)
        //{
        //    StartCoroutine(LoadAsynchronously(sceneIndex));
      //  }
        StartCoroutine(LoadAsynchronously(SaveSystem.LoadPlayer().sceneIndexx));
    }

    IEnumerator LoadAsynchronously(int sceneIdnex)
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
             GameObject.FindGameObjectWithTag("Canvas2").GetComponent<PauseMenu>().SetLoadLevel();
            
            
              //  GameObject.FindGameObjectWithTag("Canvas2").GetComponent<PauseMenu>().SetLoadLevel();

                loadingSceen = GameObject.FindGameObjectWithTag("LoadScreen");
                slider = GameObject.FindGameObjectWithTag("LoadSlider").GetComponent<Slider>();
                progressText = GameObject.FindGameObjectWithTag("LoadText").GetComponent<Text>();
                loadingSceen.SetActive(false);
            
                    
           

           
           // this.GetComponent<SavingAndLoading>().LoadEnemys();
        
            this.GetComponent<SavingAndLoading>().FindEnemys();
            this.GetComponent<SavingAndLoading>().LoadPlayer();
            Time.timeScale = 1.0f;

        }
      
    }
}




/*
 * 
 * 
 *             GameObject g;
            g = GameObject.FindGameObjectWithTag("Player").gameObject;
            g.gameObject.active = false;
            g.gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = true;
         //   loadDone = true;
*/