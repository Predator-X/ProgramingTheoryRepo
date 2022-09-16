using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    private void Start()
    {
        onStartGameObjectsInScene = GameObject.FindGameObjectsWithTag("Enemy");
       // loadLastCheckpointButton.onClick.AddListener(TaskOnClick);
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

    public virtual void SaveEnemys()
    {
        GameObject[] enemysInScene = onStartGameObjectsInScene; //GameObject.FindGameObjectsWithTag("Enemy");
        int enemysLeft = enemysInScene.Length;

     //   SaveSystem.SaveHowManyEnemysLeft(enemysLeft);
       // Enemy[] enemysInSceneToSave;
       if(enemysInScene.Length == 0)
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
