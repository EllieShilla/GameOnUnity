using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class BinarySavingSystem
{
    public static void SavePlayer(AddInventoryToObj inventory, GameObject player)
    {
        PlayerData playerData = new PlayerData(inventory, player);
        if (SceneManager.GetActiveScene().name.ToString().Equals("FightScene"))
        {
            PlayerData data = BinarySavingSystem.LoadPlayer();
            playerData.sceneName = data.sceneName;
            playerData.position[0] = data.position[0];
            playerData.position[1] = data.position[1];
            playerData.position[2] = data.position[2];
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string savePath = Application.persistentDataPath + "/data.save";
        FileStream stream = new FileStream(savePath, FileMode.Create);

        binaryFormatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static void SavePlayerBook(AddInventoryToObj inventory, GameObject player)
    {
        PlayerData playerData = new PlayerData(inventory, player);

        PlayerData data = BinarySavingSystem.LoadPlayer();
        playerData.sceneName = data.sceneName;
        playerData.position[0] = data.position[0];
        playerData.position[1] = data.position[1];
        playerData.position[2] = data.position[2];
        playerData.money = data.money;
        playerData.characters = data.characters;
        playerData.ingridients = data.ingridients;


        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string savePath = Application.persistentDataPath + "/data.save";
        FileStream stream = new FileStream(savePath, FileMode.Create);

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
            PlayerData playerData = binaryFormatter.Deserialize(stream) as PlayerData;
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
