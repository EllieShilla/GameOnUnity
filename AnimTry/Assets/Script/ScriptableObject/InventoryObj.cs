using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
public class InventoryObj : ScriptableObject
{
    public List<Item> items;
    public List<Ingridient> ingridients;
    public int money;
    public List<Character> group;
    public List<Quest> quests;
    public List<string> questPhrases;
}
