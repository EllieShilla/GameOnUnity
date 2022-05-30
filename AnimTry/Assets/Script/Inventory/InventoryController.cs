using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    GameObject itemPanel;
    GameObject inventory;

    //Изменение страниц инвентаря: если одна страница активна - остальные нет. 
    public void ChoosePage(int index, GameObject inventory)
    {
        this.inventory = inventory;



        switch (index)
        {
            case 0:
                {
                    inventory.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    inventory.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    inventory.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.SetActive(false);

                    //InventoryScrollPanelShowAndHide(0,1,2,3);
                    Inventory.isCreate = true;

                    foreach (var z in GameObject.FindGameObjectsWithTag("CharacterListInInventory"))
                        Destroy(z);


                    foreach (var z in GameObject.FindGameObjectsWithTag("IngridientInInventoryPanel"))
                        Destroy(z);

                    ActiveFalse(1,2,0,3);

                }
                break;
            case 1:
                {
                    inventory.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    inventory.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.SetActive(true);
                    inventory.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.SetActive(false);

                    CharacterListInInventory.isCreate = true;

                    foreach (var z in GameObject.FindGameObjectsWithTag("ItemInInventoryPanel"))
                        Destroy(z);


                    foreach (var z in GameObject.FindGameObjectsWithTag("IngridientInInventoryPanel"))
                        Destroy(z);

                    ActiveFalse(0, 2,1,3);
                }
                break;
            case 2:
                {
                    if(GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>().inventoryObj.quests.Count>0)
                    inventory.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.SetActive(true);
                    inventory.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    inventory.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    QuestListInventory.isCreate = true;

                    foreach (var z in GameObject.FindGameObjectsWithTag("CharacterListInInventory"))
                        Destroy(z);

                    foreach (var z in GameObject.FindGameObjectsWithTag("ItemInInventoryPanel"))
                        Destroy(z);

                    foreach (var z in GameObject.FindGameObjectsWithTag("IngridientInInventoryPanel"))
                        Destroy(z);


                    ActiveFalse(1, 0,2,3);
                }
                break;
            case 3:
                {
                    inventory.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    inventory.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    inventory.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.SetActive(false);
                    inventory.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);

                    IngridientListInventory.isCreate = true;

                    foreach (var z in GameObject.FindGameObjectsWithTag("CharacterListInInventory"))
                        Destroy(z);

                    foreach (var z in GameObject.FindGameObjectsWithTag("ItemInInventoryPanel"))
                        Destroy(z);

                    ActiveFalse(1, 0, 3,2);

                }
                break;
        }
    }

    void ActiveFalse(int index1,int index2, int indexTrue, int index3)
    {

        inventory.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(indexTrue).gameObject.SetActive(true);
        inventory.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(index1).gameObject.SetActive(false);
        inventory.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(index2).gameObject.SetActive(false);
        inventory.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(index3).gameObject.SetActive(false);
    }

    void InventoryScrollPanelShowAndHide(int indexShow, int indexHide1, int indexHide2, int indexHide3)
    {

    }

}
