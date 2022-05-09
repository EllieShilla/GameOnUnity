using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public int money;

    public string sceneName;
    public float[] position;

    public string[] itemNames;
    public string[] ingridients;
    public string[] characters;

    public PlayerData(AddInventoryToObj inventory, GameObject player)
    {
        money = inventory.inventoryObj.money;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        sceneName = SceneManager.GetActiveScene().name.ToString();

        itemNames = new string[inventory.inventoryObj.items.Count];
        ingridients = new string[inventory.inventoryObj.ingridients.Count];
        characters = new string[inventory.inventoryObj.group.Count];

        for (int i = 0; i < inventory.inventoryObj.items.Count; i++)
            itemNames[i] = inventory.inventoryObj.items[i].name;

        for (int i = 0; i < inventory.inventoryObj.ingridients.Count; i++)
            ingridients[i] = inventory.inventoryObj.ingridients[i].name;

        for (int i = 0; i < inventory.inventoryObj.group.Count; i++)
            characters[i] = inventory.inventoryObj.group[i].name;

    }

    public PlayerData()
    {
        money = 50;

        sceneName = "SampleScene";

        position = new float[3];
        position[0] = 12.03f;
        position[1] = 1.094f;
        position[2] = -4.327f;

        characters = new string[] { "MainCharacter", "Character2", "Character16" };

        itemNames = new string[0] { };
        ingridients = new string[0] { }; ;
    }
}


