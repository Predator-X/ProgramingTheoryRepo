using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingAndLoading : MonoBehaviour
{
     GameObject[] onStartGameObjectsInScene;
    GameObject playerHolder;

    public static SavingAndLoading Instance;

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
    }







    public void FindEnemys()
    {
            onStartGameObjectsInScene = GameObject.FindGameObjectsWithTag("Enemy");   
    }
    public virtual void SavePlayer()
    {
        SaveSystem.SavePlayer(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>());
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
        if (enemysInScene.Length == 0)
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
}
