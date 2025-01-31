using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public static class BinarySavingSystem
{
    public static void SavePlayer(AddInventoryToObj inventory, GameObject player)
    {
        PlayerData playerData = new PlayerData(inventory, player);
        PlayerData data;

        if (SceneManager.GetActiveScene().name.ToString().Equals("FightScene"))
        {
            data = BinarySavingSystem.LoadPlayer();
            playerData.sceneName = data.sceneName;
            playerData.position[0] = data.position[0];
            playerData.position[1] = data.position[1];
            playerData.position[2] = data.position[2];
        }

        data = BinarySavingSystem.LoadPlayer();

        if (data.booksName != null)
            for (int i = 0; i < data.booksName.Length; i++)
            {
                playerData.booksName[i] = data.booksName[i];
            }

        if (data.chests != null)
            for (int i = 0; i < data.chests.Length; i++)
            {
                playerData.chests[i] = data.chests[i];
            }

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string savePath = Application.persistentDataPath + "/data.save";
        FileStream stream = new FileStream(savePath, FileMode.Create);

        binaryFormatter.Serialize(stream, playerData);
        stream.Close();
    }

    //сохранение поднятых книг
    public static void SavePlayerBook(String bookTitle)
    {
        PlayerData playerData = BinarySavingSystem.LoadPlayer();

        for (int i = 0; i < playerData.booksName.Length; i++)
        {
            if (playerData.booksName[i].Split('_')[0].Equals(bookTitle))
                playerData.booksName[i] = bookTitle + "_" + true.ToString();
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string savePath = Application.persistentDataPath + "/data.save";
        FileStream stream = new FileStream(savePath, FileMode.Create);

        binaryFormatter.Serialize(stream, playerData);
        stream.Close();
    }

    //сохранение пройденных обучающих диалогов
    public static void SavePlayerLearnDialogue(int dialogueIndex)
    {
        PlayerData playerData = BinarySavingSystem.LoadPlayer();

        playerData.LearnDialogue[dialogueIndex][1] = true.ToString();

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string savePath = Application.persistentDataPath + "/data.save";
        FileStream stream = new FileStream(savePath, FileMode.Create);

        binaryFormatter.Serialize(stream, playerData);
        stream.Close();
    }

    //сохранение сундуков
    public static void SaveChests(string chest)
    {
        PlayerData playerData = BinarySavingSystem.LoadPlayer();

        playerData.chests.FirstOrDefault(i=>i[0].Equals(chest))[1]="true";
        string []q = playerData.chests.FirstOrDefault(i => i[0].Equals(chest));

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
