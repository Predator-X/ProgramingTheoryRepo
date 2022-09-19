using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor; // original code to quit Unity player
#endif

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public Button loadLastCheckpointButton;
    GameObject playerHolder;

    GameObject[] onStartGameObjectsInScene;
    GameObject levelLoader;

    Scene scene;

    //MainMenu
    public GameObject mainMenu;

    //ScoreList
    ArrayList scoreListArrayList;
    private void Start()
    {
        // onStartGameObjectsInScene = GameObject.FindGameObjectsWithTag("Enemy");
        // loadLastCheckpointButton.onClick.AddListener(TaskOnClick);
        GetEnemysFromScene();


        List<PlayerAchivments> scoreList = new List<PlayerAchivments>();
        scoreList.Add(new PlayerAchivments("bob", 10, 100, 1000));
        scoreList.Add(new PlayerAchivments(SaveSystem.getUserName(), 10, 100, 1000));

        JsonHelper.SaveToJSON<PlayerAchivments>(scoreList, SaveSystem.getUserName());

      
        scoreList = JsonHelper.ReadListFromJSON<PlayerAchivments>("bob");

        for(int i=0;i<= scoreList.Count; i++)
        {
            if (SaveSystem.getUserName() == scoreList[i].Name)
            {
                scoreList.RemoveAt(i);
            }
        }
        
        scoreList.Add(new PlayerAchivments("bob", 10, 100, 1000));
        scoreList.Add(new PlayerAchivments(SaveSystem.getUserName(), 10, 100, 1000));

        JsonHelper.SaveToJSON<PlayerAchivments>(scoreList, SaveSystem.getUserName());
       
    }


  

    public void TaskOnClick()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader");
       
        if (levelLoader != null)
        {
            levelLoader = GameObject.FindGameObjectWithTag("LevelLoader");
            levelLoader.GetComponent<LoadLevel>().Load();//SaveSystem.LoadPlayer().sceneIndexx);
        }
        else if (levelLoader == null)
        {
            Debug.LogError("LevelLoader is Empty !! in ----PauseMenu");
        }
               
    }

    public void SetLoadLevel()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader");

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

   public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void SetMainMenuON()
    {
        mainMenu.active = true;
    }

    public void SetMainMenuOf()
    {
        mainMenu.active = false;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    public virtual void SavePlayer()
    {
        scene = SceneManager.GetActiveScene();
        SaveSystem.SavePlayer(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(),scene.buildIndex);
        
        SaveEnemys();
    }

    public virtual void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        playerHolder = GameObject.FindGameObjectWithTag("Player");
        playerHolder.GetComponent<PlayerController>().currentHealth = data.health;
        playerHolder.GetComponent<PlayerController>().score = data.score;
        playerHolder.GetComponent<PlayerController>().currentTime = data.currntTime;

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


    public virtual void GetEnemysFromScene()
    {
        int a = 0;
        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
        {
            a++;
            if (obj.tag == "Enemy")
            {
                onStartGameObjectsInScene[a] = obj;
            }


        }
    }


    public virtual void SaveEnemys()
    {
       

        GameObject[] enemysInScene = SaveSystem.getEnemysOnStart; //GameObject.FindGameObjectsWithTag("Enemy");
     // int enemysLeft = enemysInScene.Length;
    
     
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
                SaveSystem.SaveEnemys(enemysInScene[i].GetComponent<Enemy>(),i);
            }
        }
    }

    public virtual void LoadEnemys()
    {
        GameObject[] enemysInScene = onStartGameObjectsInScene; //GameObject.FindGameObjectsWithTag("Enemy");

        int enemysLeft = enemysInScene.Length;

        Vector3 position;
        // Enemy[] enemysInSceneToSave;
        if (enemysInScene.Length == 0)
        {
            Debug.Log("There was no enemys to save ----- PauseMenu c# ");

        }
        else
        {
            for (int i = 0; i != enemysInScene.Length; i++)
            {
                 enemysInScene[i].GetComponent<Enemy>().currentHealth =  SaveSystem.LoadEnemys(i).heath;
                enemysInScene[i].SetActive(SaveSystem.LoadEnemys(i).isItActive);
                //  enemysInScene[i].GetComponent<Enemy>().currentHealth = SaveSystem.LoadEnemys(i).heath;
                position.x = SaveSystem.LoadEnemys(i).position[0];
                position.y = SaveSystem.LoadEnemys(i).position[1];
                position.z = SaveSystem.LoadEnemys(i).position[2];

                enemysInScene[i].transform.position = position;

            }
        }
    }





    

}





/* 
    public void SavePlayersScoreListDataToJSON(PlayerAchivments playerAchivments)//string playerName, int score, float time, float totalScore)
{
        scoreListArrayList = new ArrayList();

        List<PlayerAchivments> pList = new List<PlayerAchivments>()
        {
            playerAchivments
    };
       

        scoreListArrayList.Add(pList);
        PlayersScoreListData data = new PlayersScoreListData(scoreListArrayList);
        string json = JsonUtility.ToJson(data.ToString());
       
       File.WriteAllText("G:/__JSONtest/PlayersScoreListData.json", json);// Application.persistentDataPath + "/PlayersScoreListData.json", json);
        /* PlayerAchivments playerAchivments = new PlayerAchivments();

         playerAchivments.Name = playerName;
         playerAchivments.Score = score;
         playerAchivments.Time = time;
         playerAchivments.TotalScore = totalScore;

         */
//   PlayersScoreListData playersScoreListData = new PlayersScoreListData(playerAchivments);
// playersScoreListData.playersScoreArrayList.Add(playerAchivments);
//   playersScoreListData(playerAchivments);
//PlayersScoreListData.playersScoreArraList.AddRange(playerAchivments.ToArray())

// playersScoreListData.playersScoreArrayList.Add(playerAchivments);
//  string json = JsonUtility.ToJson(playersScoreListData);
//   File.WriteAllText("G:/__JSONtest/PlayersScoreListData.json", json);// Application.persistentDataPath + "/PlayersScoreListData.json", json);
/*
Debug.Log("G:/__JSONtest/PlayersScoreListData.json");
    }

*/