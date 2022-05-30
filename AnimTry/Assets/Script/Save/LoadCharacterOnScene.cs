using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacterOnScene : MonoBehaviour
{
    private GameObject MainCharacter;
    public void Start()
    {
        if (FromScene.loadPosition.x != 0)
        {
            MainCharacter = GameObject.Find("MainCharacter");
            MainCharacter.transform.position = FromScene.loadPosition;
            MainCharacter.transform.rotation = Quaternion.Euler(MainCharacter.transform.rotation.x, MainCharacter.transform.rotation.y + 180f, MainCharacter.transform.rotation.z);

            GameObject player = GameObject.Find("MainCharacter");
            AddInventoryToObj inventory = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>();
            BinarySavingSystem.SavePlayer(inventory, player);
        }
        else
        {
            LoadInformation();
        }
    }

    public void LoadInformation()
    {
        PlayerData data = BinarySavingSystem.LoadPlayer();

        GameObject player = GameObject.Find("MainCharacter");
        player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);

        AddInventoryToObj inventory = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>();
        inventory.inventoryObj.items.Clear();
        inventory.inventoryObj.ingridients.Clear();
        inventory.inventoryObj.group.Clear();
        inventory.inventoryObj.quests.Clear();
        inventory.inventoryObj.questPhrases.Clear();

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
            character.baseHero.Pressure = Convert.ToInt32(data.charactersStat[i][0]);
            character.baseHero.currentPressure = Convert.ToInt32(data.charactersStat[i][1]);
            character.baseHero.stamina = Convert.ToInt32(data.charactersStat[i][2]);
            character.baseHero.currentStamina = Convert.ToInt32(data.charactersStat[i][3]);
            character.baseHero.Confectioner = Convert.ToInt32(data.charactersStat[i][4]);
            character.baseHero.ColdShop = Convert.ToInt32(data.charactersStat[i][5]);
            character.baseHero.HotShop = Convert.ToInt32(data.charactersStat[i][6]);
            inventory.inventoryObj.group.Add(character);
        }

        for (int i = 0; i < data.quests.Length; i++)
        {
            Quest quest= Resources.Load<Quest>($"ScriptObj/Quests/{data.quests[i][0]}");
            quest.isTaken = Convert.ToBoolean(data.quests[i][1]);
            quest.isCompleted = Convert.ToBoolean(data.quests[i][2]);
            inventory.inventoryObj.quests.Add(quest);
        }

        LoadBook.LoadBooksOnScene();

        inventory.inventoryObj.money = data.money;

        for (int i = 0; i < data.questPhrases.Length; i++)
        {
            inventory.inventoryObj.questPhrases.Add(data.questPhrases[i]);
        }
    }
}
