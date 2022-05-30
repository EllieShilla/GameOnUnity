using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName_ENG;
    public string itemName_RU;
    public string description_ENG;
    public string description_RU;
    public Sprite itemArt;

    public enum Type
    {
        Stamina,
        Pressure,
    }

    public Type type;
    public float amount_of_recovery;

    public List<Ingridient> ingridients;

}
