using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBusDriver : MonoBehaviour
{
    private void Start()
    {
        InventoryObj inventoryObj = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>().inventoryObj;
        Quest quest = inventoryObj.quests.Find(i => i.name.Equals("WhereIsTheBusDriver"));
        string phrase = inventoryObj.questPhrases.Find(i => i.Equals("BusOnPlace"));
        if (quest != null)
            if (phrase != null || quest.isCompleted)
                this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}



