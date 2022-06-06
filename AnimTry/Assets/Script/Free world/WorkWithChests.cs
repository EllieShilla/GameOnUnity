using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkWithChests : MonoBehaviour
{
    [SerializeField]
    public Chest chest;
    private bool playerInRange = false;
    [SerializeField]
    private GameObject IntoPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character" && !chest.isClear)
        {
            playerInRange = true;
            IntoPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            playerInRange = false;
            IntoPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (!chest.isClear)
        {
            this.gameObject.SetActive(true);

            if (playerInRange)
            {
                TextVariantLanguageInteractivePanel textVariantLanguage = new TextVariantLanguageInteractivePanel();

                IntoPanel.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = textVariantLanguage.PanelLoot();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    IntoPanel.SetActive(false);
                    playerInRange = false;

                    this.gameObject.transform.GetChild(0).gameObject.SetActive(true);

                    ClearChest();
                }
            }
        }
    }

    void ClearChest()
    {
        AddInventoryToObj inventory = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>();
        inventory.inventoryObj.ingridients.AddRange(chest.ingridients);
        chest.isClear = true;
        BinarySavingSystem.SaveChests(chest.name);
    }
}
