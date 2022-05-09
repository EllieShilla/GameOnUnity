using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class BinarySavingSystem 
{
    public static void SavePlayer(AddInventoryToObj inventory, GameObject player)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string savePath = Application.persistentDataPath + "/data.save";
        FileStream stream=new FileStream(savePath, FileMode.Create);
        PlayerData playerData = new PlayerData(inventory, player);
        binaryFormatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string savePath = Application.persistentDataPath + "/data.save";
        if (File.Exists(savePath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(savePath, FileMode.Open);
            PlayerData playerData=binaryFormatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return playerData;
        }
        else
        {
            PlayerData playerData = new PlayerData();

            return playerData;
        }
    }
}