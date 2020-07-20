using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class save
{
    public static void SaveData(player Player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/PlayerData.xd";
        FileStream stream = new FileStream(path, FileMode.Create);
        data data = new data(Player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveAchievementData(achievements Achievement)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/AchievementsData.xd";
        FileStream stream = new FileStream(path, FileMode.Create);
        data data = new data(Achievement);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static data LoadData()
    {
        string path = Application.persistentDataPath + "/PlayerData.xd";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            data data = formatter.Deserialize(stream) as data;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }

    }

    public static data LoadAchievementData()
    {
        string path = Application.persistentDataPath + "/AchievementsData.xd";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            data data = formatter.Deserialize(stream) as data;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }

    }
}
