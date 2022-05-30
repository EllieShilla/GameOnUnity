using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuyIngridient : MonoBehaviour
{
    public GameObject ShopPanel;
    GameObject panel;
    AddInventoryToObj inventory;
    List<Ingridient> ingridients;
    void Start()
    {
        inventory = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>();
        panel = GameObject.Find("ShopPanel");
        ingridients = new List<Ingridient>();
        ingridients.AddRange(GameObject.Find("Ingridients").GetComponent<AllIngridients>().ingridients);

        for (int i = 0; i < ShopPanel.transform.childCount; i++)
        {
            Transform IngridientPanel = ShopPanel.transform.GetChild(i);
            Button button = IngridientPanel.GetChild(3).GetComponent<Button>();
            button.onClick.AddListener(() => Buy(IngridientPanel, inventory, panel));
        }
    }

    //������� ������������ ��� �������� ����
    void Buy(Transform ingridientPanel, AddInventoryToObj addInventory, GameObject panelFormMoney)
    {
        int money = int.Parse(ingridientPanel.GetChild(2).GetComponent<Text>().text);
        //string ingTitle = ingridientPanel.GetChild(1).GetComponent<Text>().text;
        string ingTitle = ingridientPanel.name.Split('_')[0];
        
        if (inventory.inventoryObj.money >= money)
        {
            inventory.inventoryObj.money -= money;
            inventory.inventoryObj.ingridients.Add(ingridients.Find(ing => ing.name == ingTitle));
            panelFormMoney.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = inventory.inventoryObj.money.ToString();
        }
    }
}




