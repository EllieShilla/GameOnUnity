using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public List<Item> items;
    GameObject allItemsPanel;
    public GameObject itemPanel;
    public GameObject descriptionItemPanel;
    public Button useItemButton;
    UseItem useItem;

    //public GameObject PanelForGroup;
    public GameObject ScrollPanelForGroup;
    public GameObject GroupCharacterPanel;
    public Button showGroupButton;
    public static bool isCreate = false;
    List<Item> noDupesItem;
    Text CoinPanelText;

    void Update()
    {
        if (isCreate)
        {
            if (GameObject.FindGameObjectsWithTag("ItemInInventoryPanel").Length > 0)
            {
                GameObject[] ItemInInventoryPanel = GameObject.FindGameObjectsWithTag("ItemInInventoryPanel");
                foreach (GameObject item in ItemInInventoryPanel)
                {
                    Destroy(item);
                }
            }

            StartInventory();
        }
    }

    public void StartInventory()
    {
        useItem = gameObject.AddComponent<UseItem>();

        items = new List<Item>();
        if (GameObject.Find("InventoryGameObject"))
            items.AddRange(GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>().inventoryObj.items);

        var OrderByItem = items.OrderBy(p => p.itemName);
        noDupesItem = OrderByItem.Distinct().ToList();

        allItemsPanel = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        CoinPanelText = this.gameObject.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>();

        ShowInventory();
        Description(items[0].itemName + "_Button");

        isCreate = false;
    }

    //вывод items в инвентаре
    void ShowInventory()
    {
        CoinPanelText.text = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>().inventoryObj.money.ToString();

        for (int i = 0; i < noDupesItem.Count; i++)
        {
            GameObject panel = itemPanel;
            Image img = panel.transform.GetChild(0).gameObject.GetComponent<Image>();
            img.sprite = noDupesItem[i].itemArt;
            //img.GetComponent<RectTransform>().sizeDelta = new Vector2(70, 80);

            Button button = panel.transform.GetChild(2).gameObject.GetComponent<Button>();
            button.name = "_Button";
            string ButtonName= noDupesItem[i].itemName + "_Button";

            int countItem = items.FindAll(itm => itm.itemName.Equals(noDupesItem[i].itemName)).Count;
            Text text = panel.transform.GetChild(1).gameObject.GetComponent<Text>();

            if (countItem > 1)
                text.text = "x" + countItem;
            else
                text.text = "";

            var newItemPanel = Instantiate(panel, new Vector3(allItemsPanel.transform.position.x, allItemsPanel.transform.position.y, allItemsPanel.transform.position.z), Quaternion.identity);
            newItemPanel.transform.parent = allItemsPanel.transform;
            newItemPanel.tag = "ItemInInventoryPanel";
            newItemPanel.name = ButtonName;

            Button btn = GameObject.FindGameObjectsWithTag("ItemInInventoryPanel").Where(obj => obj.name == ButtonName).Last().transform.GetChild(2).GetComponent<Button>();
            btn.onClick.AddListener(delegate { Description(ButtonName); });
        }
    }

    public GameObject ingridientPanel;
    //вывод описания выбраного item 
    void Description(string buttonName)
    {
        ScrollPanelForGroup.SetActive(false);

        Item item = noDupesItem.Find(i => i.itemName.Equals(buttonName.Split('_')[0]));
        GameObject panel = descriptionItemPanel;
        panel.transform.GetChild(0).gameObject.GetComponent<Text>().text = item.itemName;
        panel.transform.GetChild(1).gameObject.GetComponent<Text>().text = item.description + "\n" + item.type + ": " + item.amount_of_recovery;

        if (GameObject.FindGameObjectsWithTag("IngridientsForItem").Length > 0)
        {
            GameObject[] ItemInInventoryPanel = GameObject.FindGameObjectsWithTag("IngridientsForItem");
            foreach (GameObject itm in ItemInInventoryPanel)
            {
                Destroy(itm);
            }
        }

        foreach (var ing in item.ingridients)
        {
            ingridientPanel.transform.GetChild(0).GetComponent<Image>().sprite = ing.itemArt;
            ingridientPanel.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = new Vector2(75, 75);
            var newIngridientPanel = Instantiate(ingridientPanel, new Vector3(panel.transform.GetChild(2).transform.position.x, panel.transform.GetChild(2).transform.position.y, panel.transform.GetChild(2).transform.position.z), Quaternion.identity);
            newIngridientPanel.transform.parent = panel.transform.GetChild(2).transform;
            newIngridientPanel.tag = "IngridientsForItem";

            newIngridientPanel.name = "IngridientName_"+ing.Title;
            newIngridientPanel.AddComponent<TooltipShow>();
        }

        //очистка кнопки от прослушки и привязка функции использования item к кнопке
        showGroupButton.onClick.RemoveAllListeners();
        showGroupButton.onClick.AddListener(() => useItem.ShowGroup(ScrollPanelForGroup, GroupCharacterPanel, item));

    }


}

