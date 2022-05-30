using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    private int[] intData;
    private string[] stringData;
    private Dictionary<Item, int> itemDictionary;
    private Dictionary<Ingridient,int> ingridientDictionary;
    public bool QuetsSearch(Quest quest)
    {
        bool allHave = false;
        InventoryObj inventory = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>().inventoryObj;

        switch (quest.dataTypeGoal)
        {
            case "int":
                {

                    intData = new int[quest.goals.Length];
                    for (int i = 0; i < quest.goals.Length; i++)
                        intData[i] = quest.goals[i].intData;


                    foreach (int item in intData)
                    {
                        if(inventory.money>=item)
                            allHave = true;
                        else
                            allHave = false;
                    }
                }break;
            case "string":
                {
                    if(inventory.questPhrases.FirstOrDefault(i=>i.Equals(quest.goals[0].stringData))!=null)
                        allHave = true;
                    else
                        allHave = false;
                }
                break;
            case "Item":
                {
                    itemDictionary = new Dictionary<Item, int>();

                    for (int i = 0; i < quest.goals.Length; i++)
                    {
                        if (!itemDictionary.ContainsKey(quest.goals[i].itemData))
                            itemDictionary.Add(quest.goals[i].itemData, 1);
                        else
                        {
                            itemDictionary[quest.goals[i].itemData] += 1;
                        }
                    }

                    foreach (var item in itemDictionary)
                    {
                        //if (inventory.items.FindAll(i => i.itemName.Equals(item.Key.itemName)).Count >= item.Value)
                        if (inventory.items.FindAll(i => i.name.Equals(item.Key.name)).Count >= item.Value)
                            allHave = true;
                        else
                            allHave = false;
                    }
                }
                break;
            case "Ingridient":
                {
                    ingridientDictionary = new Dictionary<Ingridient, int>();

                    for (int i = 0; i < quest.goals.Length; i++)
                    {
                        if (!ingridientDictionary.ContainsKey(quest.goals[i].ingridientData))
                            ingridientDictionary.Add(quest.goals[i].ingridientData, 1);
                        else
                        {
                            ingridientDictionary[quest.goals[i].ingridientData] += 1;
                        }
                    }

                    foreach (var item in ingridientDictionary)
                    {
                        if (inventory.ingridients.FindAll(i => i.name.Equals(item.Key.name)).Count >= item.Value)
                            allHave = true;
                        else
                            allHave = false;
                    }
                }
                break;
        }

        return allHave;
    }

    public void QuestContinue(Quest quest)
    {
        InventoryObj inventory = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>().inventoryObj;
        inventory.questPhrases.Add(quest.goals[0].stringData);
    }
    public void EndQuest(Quest quest)
    {
        InventoryObj inventory = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>().inventoryObj;

        switch (quest.dataTypeGoal)
        {
            case "int":
                {
                    inventory.money -= intData[0];
                }
                break;
            case "string":
                {
                    int index = inventory.questPhrases.FindIndex(i => i.Equals(quest.goals[0].stringData));
                    inventory.questPhrases.RemoveAt(index);

                }
                break;
            case "Item":
                {
                    foreach (var item in itemDictionary.Keys.ToList())
                    {
                        if (inventory.items.FindAll(i => i.name.Equals(item.name)).Count >= 0)
                        {
                            while (itemDictionary[item] > 0)
                            {
                                inventory.items.Remove(item);
                                itemDictionary[item] -= 1;
                            }
                        }
                    }
                }
                break;
            case "Ingridient":
                {
                    foreach (var item in ingridientDictionary.Keys.ToList())
                    {
                        if (inventory.ingridients.FindAll(i => i.name.Equals(item.name)).Count >= 0)
                        {
                            while (ingridientDictionary[item] > 0)
                            {
                                inventory.ingridients.Remove(item);
                                ingridientDictionary[item] -= 1;
                            }
                        }
                    }
                }
                break;
        }

    }

    public void Reward(Quest quest)
    {
        InventoryObj inventory = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>().inventoryObj;

        switch (quest.dataTypeRewards)
        {
            case "money":
                {
                    inventory.money += quest.rewards[0].money;
                }
                break;
            case "ColdShop":
                {
                        foreach(Character group in inventory.group)
                        {
                            group.baseHero.ColdShop += quest.rewards[0].ColdShop;
                        }
                }
                break;
            case "HotShop":
                {

                        foreach (Character group in inventory.group)
                        {
                            group.baseHero.HotShop += quest.rewards[0].HotShop;
                        }
                }
                break;
            case "Confectioner":
                {
                        foreach (Character group in inventory.group)
                        {
                            group.baseHero.Confectioner += quest.rewards[0].Confectioner;
                        }
                }
                break;
            case "itemData":
                {
                    foreach(var item in quest.rewards)
                    {
                        inventory.items.Add(item.itemData);
                    }
                }
                break;
            case "ingridientData":
                {
                    foreach (var item in quest.rewards)
                    {
                        inventory.ingridients.Add(item.ingridientData);
                    }
                }
                break;
            case "message":
                {

                }
                break;
        }

    }
}
