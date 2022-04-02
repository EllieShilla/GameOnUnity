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
    private void Start()
    {
        gameData = GetComponent<GameData>();
        savePath = Application.persistentDataPath + "/gamesave.save";
        //Debug.Log(savePath);

        if (fromSampleScene)
        {
            print("fromSampleScene");
            LoadData();

            fromSampleScene = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
            if (SceneManager.GetActiveScene().name.Equals("SampleScene"))
            {
                SaveData();
                //SceneManager.LoadScene("DinnerScene");
            }
            else
            {
                //SceneManager.LoadScene("SampleScene");
                //LoadData();
                fromSampleScene = true;
            }

    }

    private void Update()
    {
        //if (fromSampleScene)
        //{
        //    print("fromSampleScene");
        //    LoadData();

        //    fromSampleScene = false;
        //}
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

        print(gameData.player.transform.position.x + " " +
            gameData.player.transform.position.y + " " +
            gameData.player.transform.position.z);

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (var filestream = File.Create(savePath))
        {
            binaryFormatter.Serialize(filestream, save);
        }

        Debug.Log("Data saved");
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

            gameData.player.transform.position = new Vector3(save.SavePositionX, save.SavePositionY, save.SavePositionZ);
            gameData.player.transform.rotation =  Quaternion.Euler(gameData.player.transform.rotation.x, gameData.player.transform.rotation.y+180f, gameData.player.transform.rotation.z);
            //gameData.player.transform.position = save.SaveVector3;
            //gameData.scene = save.SaveScene;


        }
    }
}
