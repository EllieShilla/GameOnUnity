using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Food", menuName = "Food")]
public class Food : ScriptableObject
{
    public string foodName;
    public string description;
    public Sprite foodArt;
    public int fullness;
    public int taste;

    public enum Type
    {
        Confectioner,
        ColdShop,
        HotShop
    }

    public Type typeOfFood;
    public int skill;
    public int price;
    public List<string> listOfEstablishments;

}
