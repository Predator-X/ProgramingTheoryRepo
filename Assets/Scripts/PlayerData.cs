using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData 
{
    public float health;
    public int score;
    public float currntTime;
    public float[] position;
    public float[] rotation;
    public int sceneIndexx;
    public PlayerData(PlayerController player, int sceneIndex )
    {
        sceneIndexx = sceneIndex;
        health = player.currentHealth;
        score = player.score;
        currntTime = player.currentTime;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        rotation = new float[3];
        rotation[0] = player.transform.rotation.x;
        rotation[1] = player.transform.rotation.y;
        rotation[2] = player.transform.rotation.z;


    }


}


[System.Serializable]
public class EnemyData
{
    //public string[] enemyNames;
    public float heath;
    public bool isItActive;
    public string enemyName;
    public float[] position;

    public EnemyData(Enemy enemy)
    {
        heath = enemy.currentHealth;
        enemyName = enemy.gameObject.name;
        isItActive = enemy.isActiveAndEnabled;
        position = new float[3];
        position[0] = enemy.transform.position.x;
        position[1] = enemy.transform.position.y;
        position[2] = enemy.transform.position.z;
    }
}

[System.Serializable]
public class UserData
{
  public  string username, passport;
    PlayerController settingPlayerContrller;
    PlayerData playersData;
    public UserData(string userName,string passportt)
    {
        username = userName;
        passport = passportt;

       
    }
    public void setPlayerController(PlayerController playerSettings)
    {
        settingPlayerContrller = playerSettings;
    }

    public PlayerController getPlayerControllerSettings()
    {
        return settingPlayerContrller;
    }

    public void setPlayerData(PlayerData playerData)
    {
        playersData = playerData;
    }
    
    public PlayerData returnPlayerData()
    {
        return playersData;
    }
    
}

[System.Serializable]
public class EnemysLeftData
{
    public int enemyssLeft;

    public EnemysLeftData(int enemysLeft)
    {
        enemyssLeft = enemysLeft;
    }
}