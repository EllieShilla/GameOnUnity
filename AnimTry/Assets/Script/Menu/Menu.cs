using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public void OpenPlaneBeforeExit()
    {
        GameObject.Find("MENU").transform.GetChild(2).gameObject.SetActive(true);
    }
    public void ExitFromGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        if (FromScene.isFirstOpen)
        {
            LoadGame();
            FromScene.isFirstOpen=false;
        }
        if (FromScene.isContinue)
        {
            ContinueGame();
            FromScene.isContinue = false;
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
            inventory.inventoryObj.group.Add(character);
        }

        inventory.inventoryObj.money = data.money;
    }
    public void LoadGame()
    {
        FromScene.isContinue=true;
        PlayerData data = BinarySavingSystem.LoadPlayer();
        SceneManager.LoadScene(data.sceneName);
    }
    Text TextInfo;

    public void SaveGame()
    {
        GameObject player = GameObject.Find("MainCharacter");
        AddInventoryToObj inventory = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>();
        BinarySavingSystem.SavePlayer(inventory, player);
        TextInfo = GameObject.Find("MENU").transform.GetChild(1).GetComponent<Text>();
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
        yield return new WaitForSeconds(2f);
        TextInfo = GameObject.Find("MENU").transform.GetChild(1).GetComponent<Text>();
        TextInfo.text = "";
    }
}
