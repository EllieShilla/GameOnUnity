using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    GameObject itemPanel;
    GameObject inventory;
    public void ChoosePage(int index, GameObject inventory)
    {
        this.inventory = inventory;

        switch (index)
        {
            case 0:
                {
                    inventory.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    Inventory.isCreate = true;

                    inventory.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    foreach (var z in GameObject.FindGameObjectsWithTag("CharacterListInInventory"))
                        Destroy(z);

                    ActiveFalse(1,2,0);

                }
                break;
            case 1:
                {
                    inventory.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    CharacterListInInventory.isCreate = true;
                    inventory.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.SetActive(true);

                    foreach (var z in GameObject.FindGameObjectsWithTag("ItemInInventoryPanel"))
                        Destroy(z);

                    ActiveFalse(0, 2,1);
                }
                break;
            case 2:
                {
                    inventory.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);

                    foreach (var z in GameObject.FindGameObjectsWithTag("CharacterListInInventory"))
                        Destroy(z);

                    foreach (var z in GameObject.FindGameObjectsWithTag("ItemInInventoryPanel"))
                        Destroy(z);

                    inventory.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.SetActive(false);

                    ActiveFalse(1, 0,2);
                }
                break;
        }
    }

    void ActiveFalse(int index1,int index2, int indexTrue)
    {

        inventory.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(indexTrue).gameObject.SetActive(true);
        inventory.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(index1).gameObject.SetActive(false);
        inventory.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(index2).gameObject.SetActive(false);

    }


}
