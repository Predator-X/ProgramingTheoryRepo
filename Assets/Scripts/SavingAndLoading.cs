using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
public class SavingAndLoading : MonoBehaviour
{
     GameObject[] onStartGameObjectsInScene;
    GameObject playerHolder;

    Scene scene;
    bool sceneIsLoaded = false;

    public static SavingAndLoading Instance;

    /// <summary>
    /// ///////////////////////////////////////////////////////////////////
    /// </summary>
    public GameObject loadingSceen;
    public Slider slider;
    public Text progressText;
    bool loadDone = false , isLoadingMenu=false , isLoading =false,isSceneFromSaveOrAreadyPlayed=false, isLoadingNextLevel=false;

  

    //If this scene is main menu and player has no save/ checkpoint, has not played the game, disactivate counitue button
    GameObject mainMenuContinueButton;

    /// PlayerScoreList
    /*
    
      public List<PlayersList> ListHolder = new List<PlayersList>();
   // public List<TPlayerList> ListT = new List<T>();

    public void SaveListHolder()
    {
        ScoreListData scoreListData = new ScoreListData();
        scoreListData.ScoreListofPlayersData = ListHolder.ToArray();
        for(int i=0; i == scoreListData.ScoreListofPlayersData.Length; i++)
        {
            if(scoreListData.ScoreListofPlayersData[i] == null)
            {
                scoreListData.ScoreListofPlayersData[i] = ListHolder[i];
            }
        }
       
    }
    
    */
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
          
            return;
        }
       
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        onStartGameObjectsInScene = GameObject.FindGameObjectsWithTag("Enemy");

        //If this scene is main menu and player has no save/ checkpoint, has not played the game, disactivate counitue button
        CheckIfIsMainManuAndSetUI();
    }


    //If this scene is main menu and player has no save/ checkpoint, has not played the game, disactivate counitue button
    public void CheckIfIsMainManuAndSetUI()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        if (thisScene.buildIndex == 1)
        {
            string path = Application.persistentDataPath + "/" + SaveSystem.getUserName() + "player.save";

            if (!File.Exists(path))
            {
                mainMenuContinueButton = GameObject.FindGameObjectWithTag("ContinueButton");
                mainMenuContinueButton.active = false;
            }
            else if (File.Exists(path))
            {
                if (mainMenuContinueButton != null)
                {

                    mainMenuContinueButton.gameObject.active = true;
                }
                else
                {
                    mainMenuContinueButton = GameObject.FindGameObjectWithTag("ContinueButton");
                    mainMenuContinueButton.gameObject.active = true;
                }

            }
        }
    }



    public void FindEnemys()
    {
            onStartGameObjectsInScene = GameObject.FindGameObjectsWithTag("Enemy");   
    }
    public virtual void SavePlayer()
    {
        scene = SceneManager.GetActiveScene();
        SaveSystem.SavePlayer(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(),  scene.buildIndex);
        SaveEnemys();
    }

   /* //checks if the scene is the same as the player last save 
    public virtual void CheckSceneBeforePlyerLoad()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        if (scene.buildIndex != data.sceneIndexx )
        {
            this.GetComponent<LoadLevel>().Load();//data.sceneIndexx);
            //sceneIsLoaded = true;


        }// else if(scene.buildIndex == data.sceneIndexx && sceneIsLoaded)
    }
   */
    public virtual void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

    
       
        
            playerHolder = GameObject.FindGameObjectWithTag("Player");
            playerHolder.GetComponent<PlayerController>().SetHealth(data.health);
            playerHolder.GetComponent<PlayerController>().SetScore( data.score);
            playerHolder.GetComponent<PlayerController>().SetTime(data.currntTime);

            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            playerHolder.transform.position = position;

            LoadEnemys();
        

       

        /*
        Vector3 rotation;
        rotation.x = data.rotation[0];
        rotation.y = data.rotation[1];
        rotation.z = data.rotation[2];
       playerHolder.transform.rotation.x = rotation; */
    }

    public virtual void SaveEnemys()
    {
        GameObject[] enemysInScene = onStartGameObjectsInScene; //GameObject.FindGameObjectsWithTag("Enemy");
        int enemysLeft = enemysInScene.Length;

        //   SaveSystem.SaveHowManyEnemysLeft(enemysLeft);
        // Enemy[] enemysInSceneToSave;
        if (enemysInScene.Length == 0)
        {
            Debug.Log("There was no enemys to save ----- PauseMenu c# ");

        }
        else
        {
            for (int i = 0; i != enemysInScene.Length; i++)
            {
                // enemysInSceneToSave[i] = 
                SaveSystem.SaveEnemys(enemysInScene[i].GetComponent<Enemy>(), i);
            }
        }
    }

    public virtual void LoadEnemys()
    {
        GameObject[] enemysInScene = onStartGameObjectsInScene; //GameObject.FindGameObjectsWithTag("Enemy");

        int enemysLeft = enemysInScene.Length;

        Vector3 position;
        // Enemy[] enemysInSceneToSave;
        if (enemysInScene.Length <= 0)
        {
            Debug.Log("There was no enemys to save ----- PauseMenu c# ");

        }
        else
        {
            for (int i = 0; i != enemysInScene.Length; i++)
            {
                enemysInScene[i].GetComponent<Enemy>().currentHealth = SaveSystem.LoadEnemys(i).heath;
                enemysInScene[i].SetActive(SaveSystem.LoadEnemys(i).isItActive);
                //  enemysInScene[i].GetComponent<Enemy>().currentHealth = SaveSystem.LoadEnemys(i).heath;
                position.x = SaveSystem.LoadEnemys(i).position[0];
                position.y = SaveSystem.LoadEnemys(i).position[1];
                position.z = SaveSystem.LoadEnemys(i).position[2];

                enemysInScene[i].transform.position = position;

            }
        }

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void Load()//(int sceneIndex)
    {
      ////////////////8  GameObject.FindGameObjectWithTag("Canvas2").GetComponent<PauseMenu>().SetLoadLevel();
        loadDone = false;
        //////////  // scene = SceneManager.GetActiveScene();
        //if(scene.buildIndex != sceneIndex)
        //{
        //    StartCoroutine(LoadAsynchronously(sceneIndex));
        //  }
        //  loadingSceen = GameObject.FindGameObjectWithTag("LoadScreen");
        //  loadingSceen.SetActive(true);
        isLoadingMenu = false;
        isSceneFromSaveOrAreadyPlayed = true;

        StartCoroutine(LoadAsynchronously(SaveSystem.LoadPlayer().sceneIndexx));
    }

    public void StartNewGame()
    {
        loadingSceen.active = true;
        loadDone = false;
        isLoadingNextLevel = false;

        isLoadingMenu = false;
        isSceneFromSaveOrAreadyPlayed = false;

        Scene scene = SceneManager.GetActiveScene();

       
        StartCoroutine(LoadAsynchronously(scene.buildIndex + 1));
    }

    public void LoadLastSave()
    {
        isLoadingMenu = false;
        isSceneFromSaveOrAreadyPlayed = true;
       
    }

    //LoadsNextSceneFromCurrentWone
    public void LoadNextScene()
    {
        loadingSceen.active = true;
        loadDone = false;

        Scene scene = SceneManager.GetActiveScene();

        isLoadingNextLevel = true;

        isLoadingMenu = false;
        isSceneFromSaveOrAreadyPlayed = false;
        StartCoroutine(LoadAsynchronously(scene.buildIndex + 1));
    }

    public void LoadSpecificScene(int sceneIndex,bool isSceneFromSave)
    {
        loadingSceen.active = true;
        loadDone = false;
        
        isSceneFromSaveOrAreadyPlayed = isSceneFromSave;
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

  
    public void LoadMenu()
    {
        /////////////8 GameObject.FindGameObjectWithTag("Canvas2").GetComponent<PauseMenu>().SetLoadLevel();
        loadingSceen.active = true;
        loadDone = false;
        isLoadingMenu = true;

        isLoadingNextLevel = false;
        isSceneFromSaveOrAreadyPlayed = false;
        StartCoroutine(LoadAsynchronously(1));
    }


    IEnumerator LoadAsynchronously(int sceneIdnex)
    {
        //Time.timeScale = 0f;
        // loadingSceen = GameObject.FindGameObjectWithTag("LoadScreen");

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIdnex);


        // 8 loadingSceen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
        if (operation.isDone)
        {
            //8   GameObject.FindGameObjectWithTag("Canvas2").GetComponent<PauseMenu>().SetLoadLevel();
          //  if (isLoadingMenu)
          //  {
             //   GameObject.FindGameObjectWithTag("Canvas2").transform.FindChild("MainMenu").gameObject.active = true;
        //    }

            //  GameObject.FindGameObjectWithTag("Canvas2").GetComponent<PauseMenu>().SetLoadLevel();
            if (!isLoadingMenu && isSceneFromSaveOrAreadyPlayed)
            {
                this.GetComponent<SavingAndLoading>().FindEnemys();
                this.GetComponent<SavingAndLoading>().LoadPlayer();

            }
            if(!isSceneFromSaveOrAreadyPlayed && !isLoadingMenu && isLoadingNextLevel)
            {
                PlayerController playerC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                PlayerData playerdata = SaveSystem.LoadPlayer();
                playerC.SetHealth(playerdata.health); playerC.SetTime(playerdata.currntTime); playerC.SetScore(playerdata.score);
            }
            if (!isSceneFromSaveOrAreadyPlayed && !isLoadingMenu && !isLoadingNextLevel)
            {
               if(GameObject.FindGameObjectWithTag("Player").gameObject ==null || GameObject.FindGameObjectWithTag("PlayerSpawnPoint").gameObject == null)
                {
                    Debug.LogError("Cannot find Player or PlayersSpawnPointOnSceneLoad ----SaveAndLoading c#/// Player.GameObject=" + GameObject.FindGameObjectWithTag("Player").gameObject.name +
                        "   ---  PlayerSpawnPoint = " + GameObject.FindGameObjectWithTag("PlayerSpawnPoint").gameObject.name);
                }
                else
                {
                    GameObject.FindGameObjectWithTag("Player").gameObject.transform.position = GameObject.FindGameObjectWithTag("PlayerSpawnPoint").transform.position;
                }
               
            }
            if (isLoadingMenu)
            {
                CheckIfIsMainManuAndSetUI();
                this.GetComponent<PauseMenu>().SetMainMenuON();
            }

          //8  loadingSceen = GameObject.FindGameObjectWithTag("LoadScreen");
         // 8  slider = GameObject.FindGameObjectWithTag("LoadSlider").GetComponent<Slider>();
          // 8 progressText = GameObject.FindGameObjectWithTag("LoadText").GetComponent<Text>();
            loadingSceen.SetActive(false);





            // this.GetComponent<SavingAndLoading>().LoadEnemys();

          
            Time.timeScale = 1.0f;

        }

    }
}


/*

public void LoadSpecificScene(int sceneIndex)
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




