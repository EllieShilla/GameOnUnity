using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Book", menuName = "Book")]
public class BooksWithStats : ScriptableObject
{
    public string BookTitle;
   public enum TypeOfStat
    {
        Confectioner,
        ColdShop,
        HotShop
    }

    public TypeOfStat type;
    public int count;
    public bool isLoot;
}
