using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{
    private static string SavePath => Application.persistentDataPath + "/savegame.dat"; // satu tempat, tidak bisa typo lagi

    public static void Save<T>(T saveData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(SavePath, FileMode.Create);
        bf.Serialize(file, saveData);
        file.Close();
        Debug.Log("Save Success!");
    }

    public static T Load<T>()
    {
        if (File.Exists(SavePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(SavePath, FileMode.Open);
            T loaded = (T)bf.Deserialize(file);
            file.Close();
            Debug.Log("Load Success!");
            return loaded;
        }

        Debug.LogWarning("Save file not found, loading defaults.");
        return default(T);
    }
}
