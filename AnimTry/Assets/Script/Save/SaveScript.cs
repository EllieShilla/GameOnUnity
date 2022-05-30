using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameData))]
public class SaveScript : MonoBehaviour
{
    private GameData gameData;
    private string savePath;
    public static bool fromSampleScene = false;
    public static bool saveData = false;
    int e;
    private void Start()
    {
        if (FromScene.fromSampleScene)
        {
            LoadData();
            FromScene.fromSampleScene = false;
        }
    }
    private void Update()
    {
        if (saveData)
        {

            string sceneName = SceneManager.GetActiveScene().name.ToString();

            switch (sceneName)
            {
                case "SampleScene":
                    {
                        gameData = this.gameObject.GetComponent<GameData>();
                        SaveData();
                    }
                    break;
                case "Cafe":
                    {
                        ReserveSaveData(10.34815f, 1.094f, 17.68805f);
                    }
                    break;
                case "MainCharactersHomeScene":
                    {
                        ReserveSaveData(11.99346f, 1.166f, -4.077252f);
                    }
                    break;
            }
            saveData = false;
        }
    }

    void ReserveSaveData(float posX, float posY, float posZ)
    {
        savePath = Application.persistentDataPath + "/gamesave.save";

        var save = new Save()
        {
            SavePositionX = posX,
            SavePositionY = posY,
            SavePositionZ = posZ,
            SaveSceneName = "SampleScene"
        };

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (var filestream = File.Create(savePath))
        {
            binaryFormatter.Serialize(filestream, save);
        }
    }

    public void SaveData()
    {
        savePath = Application.persistentDataPath + "/gamesave.save";

        var save = new Save()
        {
            SavePositionX = gameData.player.transform.position.x,
            SavePositionY = gameData.player.transform.position.y,
            SavePositionZ = gameData.player.transform.position.z,
            SaveSceneName = SceneManager.GetActiveScene().name.ToString()
        };

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (var filestream = File.Create(savePath))
        {
            binaryFormatter.Serialize(filestream, save);
        }
    }

    public void LoadData()
    {
        GameObject player = GameObject.Find("MainCharacter");
        AddInventoryToObj inventory = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>();
        BinarySavingSystem.SavePlayer(inventory, player);

        gameData = GetComponent<GameData>();
        savePath = Application.persistentDataPath + "/gamesave.save";

        if (File.Exists(savePath))
        {
            Save save;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (var filestream = File.Open(savePath, FileMode.Open))
            {
                save = (Save)binaryFormatter.Deserialize(filestream);
            }

            gameData.player.transform.position = new Vector3(save.SavePositionX, save.SavePositionY, save.SavePositionZ);
            gameData.player.transform.rotation = Quaternion.Euler(gameData.player.transform.rotation.x, gameData.player.transform.rotation.y + 180f, gameData.player.transform.rotation.z);
            gameData.sceneName = save.SaveSceneName;
        }
        else
        {
            gameData.sceneName = "SampleScene";
            gameData.player.transform.position = new Vector3(11.99346f, 1.094f, -4.077252f);
            gameData.player.transform.rotation = Quaternion.Euler(gameData.player.transform.rotation.x, gameData.player.transform.rotation.y + 180f, gameData.player.transform.rotation.z);
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(gameData.sceneName));
    }

    public void SceneLoad()
    {
        gameData = GetComponent<GameData>();
        savePath = Application.persistentDataPath + "/gamesave.save";

        if (File.Exists(savePath))
        {
            Save save;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (var filestream = File.Open(savePath, FileMode.Open))
            {
                save = (Save)binaryFormatter.Deserialize(filestream);
            }

            gameData.sceneName = save.SaveSceneName.ToString();
        }
        else
        {
            gameData.sceneName = "SampleScene";
        }
        FromScene.fromSampleScene = true;
        SceneManager.LoadScene(gameData.sceneName);
    }
}
