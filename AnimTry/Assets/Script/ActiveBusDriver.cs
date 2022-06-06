using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ActiveBusDriver : MonoBehaviour
{
    private void Start()
    {
        InventoryObj inventoryObj = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>().inventoryObj;

        PlayerData data = BinarySavingSystem.LoadPlayer();

        if (data.quests.FirstOrDefault(i => i[0].Equals("WhereIsTheBusDriver")) != null)
        {
            bool questComplited = Convert.ToBoolean(data.quests.FirstOrDefault(i => i[0].Equals("WhereIsTheBusDriver"))[2]);
            bool questIsTaken = Convert.ToBoolean(data.quests.FirstOrDefault(i => i[0].Equals("WhereIsTheBusDriver"))[1]);

            //Quest quest = inventoryObj.quests.Find(i => i.name.Equals("WhereIsTheBusDriver"));
            string phrase = inventoryObj.questPhrases.Find(i => i.Equals("BusOnPlace"));

            if (questIsTaken)
                if (phrase!=null || questComplited)
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

    }
}


