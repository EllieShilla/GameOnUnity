using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public void OpenPlaneBeforeExit()
    {
        GameObject.Find("MENU").transform.GetChild(1).gameObject.SetActive(true);
    }
    public void ExitFromGame()
    {
        string savePath = Application.persistentDataPath + "/data.save";
        if (!File.Exists(savePath))
        {
            string path = Application.persistentDataPath + "/gamesave.save";
            if (File.Exists(path))
                File.Delete(path);
        }

        Application.Quit();
    }

    private void Start()
    {
        if (FromScene.isContinue)
        {
            ContinueGame();
            FromScene.isContinue = false;
        }

        if (FromScene.isFirstOpen)
        {
            LoadGame();
            FromScene.isFirstOpen = false;
        }

    }
    void ContinueGame()
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
            character.baseHero.Pressure = Convert.ToInt32(data.charactersStat[i][0]);
            character.baseHero.currentPressure = Convert.ToInt32(data.charactersStat[i][1]);
            character.baseHero.stamina = Convert.ToInt32(data.charactersStat[i][2]);
            character.baseHero.currentStamina = Convert.ToInt32(data.charactersStat[i][3]);
            character.baseHero.Confectioner = Convert.ToInt32(data.charactersStat[i][4]);
            character.baseHero.ColdShop = Convert.ToInt32(data.charactersStat[i][5]);
            character.baseHero.HotShop = Convert.ToInt32(data.charactersStat[i][6]);
            inventory.inventoryObj.group.Add(character);
        }

        //LoadBook.LoadBooksOnScene();

        inventory.inventoryObj.money = data.money;
    }
    public void LoadGame()
    {
        FromScene.isContinue = true;
        PlayerData data = BinarySavingSystem.LoadPlayer();
        SceneManager.LoadScene(data.sceneName);
    }
    Text TextInfo;

    public void SaveGame()
    {
        GameObject player = GameObject.Find("MainCharacter");
        AddInventoryToObj inventory = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>();
        BinarySavingSystem.SavePlayer(inventory, player);
        TextInfo = GameObject.Find("InfoMenu").GetComponent<Text>();
        TextInfo.text = "Data saved";
        StartCoroutine(MenuInfoClose());
    }

    public void SaveBeforeExit()
    {
        SaveGame();
        ExitFromGame();
    }

    IEnumerator MenuInfoClose()
    {
        yield return new WaitForSeconds(1f);
        TextInfo = GameObject.Find("InfoMenu").GetComponent<Text>();
        TextInfo.text = "";
    }

    public void ContinueFromPause()
    {
        FromScene.isMenuActive = true;
    }

    public void ClosePanel()
    {
        GameObject.Find("MENU").transform.GetChild(1).gameObject.SetActive(false);

    }
}

