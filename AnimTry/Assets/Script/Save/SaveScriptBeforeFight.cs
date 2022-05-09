using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveScriptBeforeFight : MonoBehaviour
{
    private GameData gameData;
    private string savePath;
    public static bool saveFight = false;

    void Start()
    {
        if (FromScene.fromFightScene)
        {
            //gameData = GetComponent<GameData>();
            //savePath = Application.persistentDataPath + "/BeforeFightsave.save";

            LoadData();
            FromScene.fromFightScene = false;
        }
    }

    private void Update()
    {
        if (saveFight)
        {
            //gameData = GetComponent<GameData>();
            //savePath = Application.persistentDataPath + "/BeforeFightsave.save";

            if (!SceneManager.GetActiveScene().name.Equals("FightScene"))
            {
                SaveData();
            }
            saveFight = false;
        }
    }


    public void SaveData()
    {
        //var save = new Save()
        //{
        //    SavePositionX = gameData.player.transform.position.x,
        //    SavePositionY = gameData.player.transform.position.y,
        //    SavePositionZ = gameData.player.transform.position.z,
        //    SaveSceneName = SceneManager.GetActiveScene().name.ToString()
        //};

        //BinaryFormatter binaryFormatter = new BinaryFormatter();
        //using (var filestream = File.Create(savePath))
        //{
        //    binaryFormatter.Serialize(filestream, save);
        //}

        GameObject player = GameObject.Find("MainCharacter");
        AddInventoryToObj inventory = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>();
        BinarySavingSystem.SavePlayer(inventory, player);
    }

    public void LoadData()
    {
        //if (File.Exists(savePath))
        //{
        //    Save save;
        //    BinaryFormatter binaryFormatter = new BinaryFormatter();
        //    using (var filestream = File.Open(savePath, FileMode.Open))
        //    {
        //        save = (Save)binaryFormatter.Deserialize(filestream);
        //    }

        //    gameData.player.transform.position = new Vector3(save.SavePositionX, save.SavePositionY, save.SavePositionZ);
        //    gameData.player.transform.rotation = Quaternion.Euler(gameData.player.transform.rotation.x, gameData.player.transform.rotation.y, gameData.player.transform.rotation.z);
        //    gameData.sceneName = save.SaveSceneName.ToString();
        //}

        PlayerData data = BinarySavingSystem.LoadPlayer();

        GameObject player = GameObject.Find("MainCharacter");
        player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);

        AddInventoryToObj inventory = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>();
        inventory.inventoryObj.items.Clear();
        inventory.inventoryObj.ingridients.Clear();
        inventory.inventoryObj.group.Clear();

        for (int i = 0; i < data.itemNames.Length; i++)
        {
            Item item = Resources.Load<Item>($"ScriptObj/Items/{data.itemNames[i]}");
            inventory.inventoryObj.items.Add(item);
        }

        for (int i = 0; i < data.ingridients.Length; i++)
        {
            Ingridient ingridient = Resources.Load<Ingridient>($"ScriptObj/Ingridients/{data.ingridients[i]}");
            inventory.inventoryObj.ingridients.Add(ingridient);
        }

        for (int i = 0; i < data.characters.Length; i++)
        {
            Character character = Resources.Load<Character>($"ScriptObj/Character/{data.characters[i]}");
            inventory.inventoryObj.group.Add(character);
        }

        inventory.inventoryObj.money = data.money;
    }

    public void SceneLoad()
    {
        //gameData = GetComponent<GameData>();
        //savePath = Application.persistentDataPath + "/BeforeFightsave.save";

        //if (File.Exists(savePath))
        //{
        //    Save save;
        //    BinaryFormatter binaryFormatter = new BinaryFormatter();
        //    using (var filestream = File.Open(savePath, FileMode.Open))
        //    {
        //        save = (Save)binaryFormatter.Deserialize(filestream);
        //    }

        //    gameData.sceneName = save.SaveSceneName.ToString();
        //}
        FromScene.fromFightScene = true;
        //SceneManager.LoadScene(gameData.sceneName);

        PlayerData data = BinarySavingSystem.LoadPlayer();
        SceneManager.LoadScene(data.sceneName);
    }
}
//InventoryOpenCube