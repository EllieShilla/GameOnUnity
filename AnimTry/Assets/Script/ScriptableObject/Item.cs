using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public string description;
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
