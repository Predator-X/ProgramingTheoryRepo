using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static string UserName;
    public static GameObject[] getEnemysOnStart;
    public static void SavePlayer(PlayerController player , int sceneIndex)//string username
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        string path = Application.persistentDataPath + "/"+getUserName()+"player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player , sceneIndex);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void setUserName(string userName)
    {
        UserName = userName;
    }

    public static string getUserName()
    {
        return UserName;
    }
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/" + getUserName()+ "player.save"; //"/player.save";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else { Debug.LogError("Save file not found in " + path); return null; }
    }

    public static void SaveUserData(string username, string passport)
    {
        Debug.Log("=============Save===UserData Name: " + username);
        string path = Application.persistentDataPath+"/" + username+ "UserDataLib.save"; 
        
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            UserData data = new UserData(username, passport);

            formatter.Serialize(stream, data);
            stream.Close();
    }

    public static UserData LoadUserData(string username)
    {
        Debug.Log("=============LoadUserData Name: " + username);
        string path = Application.persistentDataPath+"/" + username+"UserDataLib.save"; //+ username + ".Userdata.save";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            UserData data = formatter.Deserialize(stream) as UserData;
            stream.Close();
            return data;
        }
        else { Debug.LogError("Save file not found in " + path); return null; }
    
    }


    public static void SaveSceneData(string username,int currentSceneid, bool isScenefinisht, int lastchekpoint)
    {
        

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + username + ".SaveSceneData.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        SceneData data = new SceneData(currentSceneid,isScenefinisht, lastchekpoint);

        formatter.Serialize(stream, data);
        stream.Close();


    }

    static SceneData LoadSceneData(string username)
    {
        string path = Application.persistentDataPath + "/"+ username+ ".SaveSceneData.save";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SceneData data = formatter.Deserialize(stream) as SceneData;
            stream.Close();
            return data;
        }
        else { Debug.LogError("Save file not found in " + path); return null; }

    }



    public static void SaveEnemys(Enemy enemy , int id)
    {
        for(int i=0; i!= id; i++)
        {

        }
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/enemy"+id+".save";
        FileStream stream = new FileStream(path, FileMode.Create);

        EnemyData data = new EnemyData(enemy);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static EnemyData LoadEnemys(int id)
    {
        string path = Application.persistentDataPath + "/enemy"+id+".save";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            EnemyData data = formatter.Deserialize(stream) as EnemyData;
            stream.Close();
            return data;
        }
        else { Debug.LogError("Save file not found in " + path); return null; }
    }




}






/*
 * 
 * 
    public static void SaveHowManyEnemysLeft(int enemysleft)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/enemyLeft.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        EnemysLeftData data = new EnemysLeftData(enemysleft);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static EnemysLeftData LoadHowManyEnemysLeft()
    {
        string path = Application.persistentDataPath + "/enemyLeft.save";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            EnemysLeftData data = formatter.Deserialize(stream) as EnemysLeftData;
            stream.Close();
            return data;
        }
        else { Debug.LogError("Save file not found in " + path); return null; }
    }


    public static void SaveEnemy(Enemy enemy)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/enemy.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        EnemyData data = new EnemyData(enemy);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static EnemyData LoadEnemy()
    {
        string path = Application.persistentDataPath + "/enemy.save";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            EnemyData data = formatter.Deserialize(stream) as EnemyData;
            stream.Close();
            return data;
        }
        else { Debug.LogError("Save file not found in " + path); return null; }
    }
*/