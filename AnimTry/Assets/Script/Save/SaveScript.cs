using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    private void Start()
    {
        if (FromScene.fromSampleScene)
        {
            gameData = GetComponent<GameData>();
            savePath = Application.persistentDataPath + "/gamesave.save";

            LoadData();
            FromScene.fromSampleScene = false;
        }
    }
    private void Update()
    {
        if (saveData)
        {
            gameData = GetComponent<GameData>();
            savePath = Application.persistentDataPath + "/gamesave.save";

            if (SceneManager.GetActiveScene().name.ToString().Equals("SampleScene"))
            {
                SaveData();
            }else if (SceneManager.GetActiveScene().name.ToString().Equals("Cafe"))
            {
                ReserveSaveData();
            }
                saveData = false;
        }
    }

    void ReserveSaveData()
    {
        var save = new Save()
        {
            SavePositionX = 10.34815f,
            SavePositionY = 1.094f,
            SavePositionZ = 17.68805f,
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
        if (File.Exists(savePath))
        {
            Save save;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (var filestream = File.Open(savePath, FileMode.Open))
            {
                save = (Save)binaryFormatter.Deserialize(filestream);
            }

            gameData.player.transform.position = new Vector3(save.SavePositionX, save.SavePositionY, save.SavePositionZ - 0.5f);
            gameData.player.transform.rotation = Quaternion.Euler(gameData.player.transform.rotation.x, gameData.player.transform.rotation.y + 180f, gameData.player.transform.rotation.z);
            gameData.sceneName = save.SaveSceneName;
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
        FromScene.fromSampleScene = true;
        SceneManager.LoadScene(gameData.sceneName);
    }
}
