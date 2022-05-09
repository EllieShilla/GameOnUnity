using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ShowItemToCreate : MonoBehaviour
{
    public List<Item> items;
    GameObject scrollItemsPanel;
    public GameObject panelForItem;
    public GameObject panelForInformantion;
    public GameObject panelForIngridients;
    public static bool startCreate = false;
    AddInventoryToObj InventoryGameObject;
    void Update()
    {
        if (startCreate)
        {
            DestroyItem();

            PrepareItemSkrollPanel();
            startCreate = false;
        }
    }

    void DestroyItem()
    {
        if (GameObject.FindGameObjectsWithTag("Item").Length > 0)
        {
            GameObject[] ItemScroll = GameObject.FindGameObjectsWithTag("Item");
            foreach (GameObject item in ItemScroll)
            {
                Destroy(item);
            }
        }
    }

    public void PrepareItemSkrollPanel()
    {
        InventoryGameObject = GameObject.Find("InventoryGameObject").GetComponent< AddInventoryToObj>();
        items = new List<Item>();

        if (GameObject.Find("ItemsList"))
            items.AddRange(GameObject.Find("ItemsList").GetComponent<ItemList>().items);

        scrollItemsPanel = this.gameObject;
        ShowScrollItemPanel();
        Description(items[0], ExaminationIngridient(items[0]));

    }
    void ShowScrollItemPanel()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Item itemBuff = items[i];
            GameObject panel = panelForItem;
            panel.name= "Item_" + itemBuff.itemName;
            Image img = panel.transform.GetChild(0).gameObject.GetComponent<Image>();
            img.sprite = itemBuff.itemArt;

            Text text = panel.transform.GetChild(1).gameObject.GetComponent<Text>();
            text.text = itemBuff.itemName;

            GameObject newItemPanel = Instantiate(panel, new Vector3(scrollItemsPanel.transform.position.x, scrollItemsPanel.transform.position.y, scrollItemsPanel.transform.position.z), Quaternion.identity);
            newItemPanel.transform.parent = scrollItemsPanel.transform;
            newItemPanel.tag = "Item";

            //если у героя отстутствуют нужные игридиенты для создания блюда, тогда item будет окрашен в серый цвет
            ExaminationIngridient(itemBuff);
            if (!ExaminationIngridient(itemBuff))
                newItemPanel.GetComponent<Image>().color = new Color32(0, 0, 0, 130);

            Button btn = GameObject.FindGameObjectsWithTag("Item").Where(obj => obj.name == panel.name + "(Clone)").Last().GetComponent<Button>();
            btn.onClick.AddListener(delegate { Description(itemBuff, ExaminationIngridient(itemBuff)); });
        }
    }

    //проверка наличия необходимых игридиентов
    bool ExaminationIngridient(Item ItemIngridient)
    {
       bool isHaveAllIng = true;

        foreach (var allIng in ItemIngridient.ingridients)
        {
            if (!InventoryGameObject.inventoryObj.ingridients.Find(ing => ing.Title.Equals(allIng.Title)))
            {
                isHaveAllIng = false;
                break;
            }
        }
        return isHaveAllIng;
    }

    void Description(Item item, bool activeButton)
    {
        Text title = panelForInformantion.transform.GetChild(0).GetComponent<Text>();
        title.text = item.itemName;
        Text description = panelForInformantion.transform.GetChild(1).GetComponent<Text>();
        description.text = item.description;
        Button button= panelForInformantion.transform.GetChild(3).GetComponent<Button>();
      
        if (!activeButton)
            button.interactable = false;
        else
            button.interactable = true;

        if (GameObject.FindGameObjectsWithTag("IngridientsForItem").Length > 0)
        {
            GameObject[] ItemInInventoryPanel = GameObject.FindGameObjectsWithTag("IngridientsForItem");
            foreach (GameObject itm in ItemInInventoryPanel)
            {
                Destroy(itm);
            }
        }

        for (int i = 0; i < item.ingridients.Count; i++)
        {
            GameObject panelForIngridient_ = panelForIngridients;
            panelForIngridient_.transform.GetChild(0).GetComponent<Image>().sprite = item.ingridients[i].itemArt;
            GameObject panel = panelForInformantion.transform.GetChild(2).gameObject;
            GameObject newIngridientPanel = Instantiate(panelForIngridient_, new Vector3(panel.transform.position.x, panel.transform.position.y, panel.transform.position.z), Quaternion.identity);
            newIngridientPanel.transform.parent = panel.transform;
            newIngridientPanel.tag = "IngridientsForItem";

            newIngridientPanel.name = "IngridientName_" + item.ingridients[i].Title;
            newIngridientPanel.AddComponent<TooltipShow>();

            //окрашивает в серый цвет отсутствующие ингридиенты
            if (!InventoryGameObject.inventoryObj.ingridients.Find(ing => ing.Title.Equals(item.ingridients[i].Title)))
                newIngridientPanel.GetComponent<Image>().color = new Color32(0, 0, 0, 130);
        }
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate { StartCreateFoodItem(item); });
    }

    void StartCreateFoodItem(Item item)
    {
        CreateFoodForInventory.ReZero();

        CreateFoodForInventory.CurrentCountofButton = Random.Range(3, 7);
        StartCreateItem.isStartCreate = true;
        Move.canMove = false;
        StartCoroutine(waitingFor_QTE_Results(item));
    }

    IEnumerator waitingFor_QTE_Results(Item item)
    {
        while (!CreateFoodForInventory.verificationOfResults())
        {
            yield return new WaitForSeconds(0.5f);
        }

        //если QTE выполнено в инвентарь добавляется блюдо
        bool goodCook = CreateFoodForInventory.ReturnResult();
        if (goodCook)
        {
            print("ADD");
            InventoryGameObject.inventoryObj.items.Add(item);
        }

        //после создания блюда ингридиенты удаляются
        foreach (var ingridient in item.ingridients)
        {
            if (InventoryGameObject.inventoryObj.ingridients.Find(ing => ing.Title.Equals(ingridient.Title)))
                InventoryGameObject.inventoryObj.ingridients.Remove(ingridient);
        }

        DestroyItem();
        PrepareItemSkrollPanel();
    }




}


