using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleResultShow : MonoBehaviour  //вывод панели с результатами
{
    public static bool GoodEnd = false;
    public static bool EndBattle = false;
    SaveScriptBeforeFight ReturnFromFightScene;
    void Start()
    {
        if (GoodEnd)
        {
            this.transform.GetChild(0).GetComponent<Text>().text = "MONEY: " + GroupCoin.coin;

            AddInventoryToObj inventorySctiptObj = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>();
            inventorySctiptObj.inventoryObj.money += GroupCoin.coin;
            EndBattle = false;
        }
        else
        {
            this.transform.GetChild(0).GetComponent<Text>().text = "You couldn't handle the hungry customers.";
            EndBattle = false;
        }

        ReturnFromFightScene = GameObject.Find("ReturnFromFightScene").GetComponent<SaveScriptBeforeFight>();
        ReturnFromFightScene.SaveData();
    }

    public void CloseScene()
    {
        ShowFoodList.workDrawFood = false;

        SaveScriptBeforeFight ReturnFromFightScene = GameObject.Find("ReturnFromFightScene").GetComponent<SaveScriptBeforeFight>();

        ReturnFromFightScene.SceneLoad();
        SceneManager.UnloadSceneAsync("FightScene");
    }

}

