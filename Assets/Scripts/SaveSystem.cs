using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static void SavePlayer(PlayerController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveUserData(string username, string passport)
    {
        string path = Application.persistentDataPath + "/"+username+".save";
        
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            UserData data = new UserData(username, passport);

            formatter.Serialize(stream, data);
            stream.Close();
    }

    public static UserData LoadUserData(string username)
    {
        string path = Application.persistentDataPath + "/" + username + ".save";

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
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.save";

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