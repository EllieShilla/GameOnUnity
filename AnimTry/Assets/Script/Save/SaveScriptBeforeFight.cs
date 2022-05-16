using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveScriptBeforeFight : MonoBehaviour
{
    public static bool saveFight = false;

    void Start()
    {
        if (FromScene.fromFightScene)
        {
            LoadData();
            FromScene.fromFightScene = false;
        }
    }

    private void Update()
    {
        if (saveFight)
        {
            if (!SceneManager.GetActiveScene().name.Equals("FightScene"))
            {
                SaveData();
            }
            saveFight = false;
        }
    }


    public void SaveData()
    {
        GameObject player = GameObject.Find("MainCharacter");
        AddInventoryToObj inventory = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>();
        BinarySavingSystem.SavePlayer(inventory, player);
    }

    public void LoadData()
    {
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
        FromScene.fromFightScene = true;
        PlayerData data = BinarySavingSystem.LoadPlayer();
        SceneManager.LoadScene(data.sceneName);
    }
}

