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

    public GameObject PanelForGroup;
    public GameObject GroupCharacterPanel;
    public Button showGroupButton;
    public static bool isCreate = false;
    List<Item> noDupesItem;

    void Update()
    {
        if (isCreate)
        {
            StartInventory();
        }
    }

    public void StartInventory()
    {
        useItem = gameObject.AddComponent<UseItem>();

        items = new List<Item>();
        if (GameObject.Find("Standing W_Briefcase Idle").GetComponent<Player>())
            items.AddRange(GameObject.Find("Standing W_Briefcase Idle").GetComponent<Player>().character.items);

        var orderedPeople = items.OrderBy(p => p.itemName);
        noDupesItem = orderedPeople.Distinct().ToList();

        allItemsPanel = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;


        ShowInventory();
        Description(items[0].itemName + "_Button");

        isCreate = false;
    }

    void ShowInventory()
    {
        for (int i = 0; i < noDupesItem.Count; i++)
        {
            GameObject panel = itemPanel;
            Image img = panel.transform.GetChild(0).gameObject.GetComponent<Image>();
            img.sprite = noDupesItem[i].itemArt;
            img.GetComponent<RectTransform>().sizeDelta = new Vector2(70, 80);

            Button button = panel.transform.GetChild(1).gameObject.GetComponent<Button>();
            button.name = "_Button";
            string ButtonName= noDupesItem[i].itemName + "_Button";

            int countItem = items.FindAll(itm => itm.itemName.Equals(noDupesItem[i].itemName)).Count;
            Text text = panel.transform.GetChild(2).gameObject.GetComponent<Text>();

            if (countItem > 1)
                text.text = "x" + countItem;
            else
                text.text = "";

            var newItemPanel = Instantiate(panel, new Vector3(allItemsPanel.transform.position.x, allItemsPanel.transform.position.y, allItemsPanel.transform.position.z), Quaternion.identity);
            newItemPanel.transform.parent = allItemsPanel.transform;
            newItemPanel.tag = "ItemInInventoryPanel";
            newItemPanel.name = ButtonName;

            Button btn = GameObject.FindGameObjectsWithTag("ItemInInventoryPanel").Where(obj => obj.name == ButtonName).Last().transform.GetChild(1).GetComponent<Button>();
            btn.onClick.AddListener(delegate { Description(ButtonName); });
        }
    }


    void Description(string buttonName)
    {
        PanelForGroup.SetActive(false);

        Item item = noDupesItem.Find(i => i.itemName.Equals(buttonName.Split('_')[0]));
        GameObject panel = descriptionItemPanel;
        panel.transform.GetChild(0).gameObject.GetComponent<Text>().text = item.itemName;
        panel.transform.GetChild(1).gameObject.GetComponent<Text>().text = item.description + "\n" + item.type + ": " + item.amount_of_recovery;

        //useItemButton.onClick.RemoveAllListeners();
        //useItemButton.onClick.AddListener(() => useItem.Use(item));
        showGroupButton.onClick.RemoveAllListeners();
        showGroupButton.onClick.AddListener(() => useItem.ShowGroup(PanelForGroup, GroupCharacterPanel, item));

    }


}

