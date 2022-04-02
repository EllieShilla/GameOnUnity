using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToInventory : MonoBehaviour
{
    [SerializeField]
    private GameObject Lotting;
    [SerializeField]
    private GameObject LottingPanel;
    bool itCanAdd = false;
    GameObject item;
    LevelUpWithBook levelUpWithBook;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            item = other.gameObject;
            Lotting.SetActive(true);
            itCanAdd = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            item = null;
            itCanAdd = false;
        }
    }

    void Update()
    {
        if (itCanAdd)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                levelUpWithBook = new LevelUpWithBook();
                levelUpWithBook.LevelUp(item);
                Destroy(item);
                Lotting.SetActive(false);
                itCanAdd = false;
                AddIfoToLuttingPanel();
            }
        }
    }


    void AddIfoToLuttingPanel()
    {
        BooksWithStats book = item.GetComponent<ItemToInventory>().books;

        LottingPanel.SetActive(true);
        Text text = LottingPanel.transform.GetChild(1).GetComponent<Text>();
        text.text = "";
        text.text += "Ты подобрал: "+ book.BookTitle+"\n"+"Навык: "+ book .type+ " твоей команды вырос на "+ book.count;
        Button button= LottingPanel.transform.GetChild(2).GetComponent<Button>();
        button.onClick.AddListener(delegate { LottingPanelSetActiveFalse(); });

    }

    void LottingPanelSetActiveFalse()
    {
        LottingPanel.SetActive(false);
    }




}




