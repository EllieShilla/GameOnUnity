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
    public string[][] charactersStat;
    public string[][] quests;
    public string[] booksName;
    public string[] questPhrases;
    
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
        charactersStat = new string[inventory.inventoryObj.group.Count][];
        quests = new string[inventory.inventoryObj.quests.Count][];
        booksName = new string[Resources.LoadAll<BooksWithStats>("ScriptObj/BooksWithStats").Length];
        questPhrases = new string[inventory.inventoryObj.questPhrases.Count];

        for (int i = 0; i < inventory.inventoryObj.items.Count; i++)
            itemNames[i] = inventory.inventoryObj.items[i].name;

        for (int i = 0; i < inventory.inventoryObj.ingridients.Count; i++)
            ingridients[i] = inventory.inventoryObj.ingridients[i].name;

        for (int i = 0; i < inventory.inventoryObj.group.Count; i++)
            characters[i] = inventory.inventoryObj.group[i].name;

        for (int i = 0; i < charactersStat.Length; i++)
        {
                charactersStat[i] = new string[7];
                charactersStat[i][0] = inventory.inventoryObj.group[i].baseHero.Pressure.ToString();
                charactersStat[i][1] = inventory.inventoryObj.group[i].baseHero.currentPressure.ToString();
                charactersStat[i][2] = inventory.inventoryObj.group[i].baseHero.stamina.ToString();
                charactersStat[i][3] = inventory.inventoryObj.group[i].baseHero.currentStamina.ToString();
                charactersStat[i][4] = inventory.inventoryObj.group[i].baseHero.Confectioner.ToString();
                charactersStat[i][5] = inventory.inventoryObj.group[i].baseHero.ColdShop.ToString();
                charactersStat[i][6] = inventory.inventoryObj.group[i].baseHero.HotShop.ToString();
        }

        for (int i = 0; i < booksName.Length; i++)
        {
            BooksWithStats book = Resources.LoadAll<BooksWithStats>("ScriptObj/BooksWithStats")[i];
            booksName[i] = book.name + "_" + book.isLoot.ToString();
        }
        for (int i = 0; i < inventory.inventoryObj.quests.Count; i++)
        {
            quests[i] = new string[3];
            quests[i][0] = inventory.inventoryObj.quests[i].name;
            quests[i][1] = inventory.inventoryObj.quests[i].isTaken.ToString();
            quests[i][2] = inventory.inventoryObj.quests[i].isCompleted.ToString();
        }

        for (int i = 0; i < inventory.inventoryObj.questPhrases.Count; i++)
            questPhrases[i] = inventory.inventoryObj.questPhrases[i];
    }

    public PlayerData()
    {
        money = 100;
        sceneName = "MainCharactersHomeScene";

        position = new float[3];

        position[0] = 13.885f;
        position[1] = 0.19f;
        position[2] = -1.571f;

        characters = new string[] { "MainCharacter", "Character2", "Character16" };
        charactersStat = new string[characters.Length][];

        for (int i = 0; i < charactersStat.Length; i++)
        {
            charactersStat[i] = new string[7];
            Character character = Resources.Load<Character>($"ScriptObj/Character/{characters[i]}");

            charactersStat[i][0] = character.baseHero.StartPressure.ToString();
            charactersStat[i][1] = "0";
            charactersStat[i][2] = character.baseHero.StartStamina.ToString();
            charactersStat[i][3] = character.baseHero.StartStamina.ToString();
            charactersStat[i][4] = character.baseHero.StartConfectioner.ToString();
            charactersStat[i][5] = character.baseHero.StartColdShop.ToString();
            charactersStat[i][6] = character.baseHero.StartHotShop.ToString();
        }

        itemNames = new string[0] { };
        ingridients = new string[0] { };
        quests = new string[0][] { };

        int lenth = Resources.LoadAll<Quest>("ScriptObj/Quests").Length;

        for(int i=0;i< Resources.LoadAll<Quest>("ScriptObj/Quests").Length; i++)
        {
            Resources.LoadAll<Quest>("ScriptObj/Quests")[i].isTaken = false;
            Resources.LoadAll<Quest>("ScriptObj/Quests")[i].isCompleted = false;
        }

        //foreach (Quest quest_ in Resources.LoadAll<Quest>("ScriptObj/Quests") as Quest[])
        //{
        //    quest_.isTaken = false;
        //    quest_.isCompleted = false;
        //}

        questPhrases=new string[0] { };

    }
}


