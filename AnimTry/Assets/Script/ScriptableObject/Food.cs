using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Food", menuName = "Food")]
public class Food : ScriptableObject
{
    public string foodName_ENG;
    public string foodName_RUS;
    public string description_ENG;
    public string description_RUS;
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
    public List<Cafe> listOfEstablishments;

}
