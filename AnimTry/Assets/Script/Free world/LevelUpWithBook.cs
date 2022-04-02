using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpWithBook : MonoBehaviour
{
   public void LevelUp(GameObject item)
    {
        List<Character> group = new List<Character>();
        group.AddRange(GameObject.Find("Standing W_Briefcase Idle").GetComponent<Player>().group);
        BooksWithStats book = item.GetComponent<ItemToInventory>().books;

        foreach (var character in group)
        {
            switch (book.type)
            {
                case BooksWithStats.TypeOfStat.ColdShop:
                    {
                        character.baseHero.ColdShop += book.count;
                    }
                    break;
                case BooksWithStats.TypeOfStat.HotShop:
                    {
                        character.baseHero.HotShop += book.count;
                    }
                    break;
                case BooksWithStats.TypeOfStat.Confectioner:
                    {
                        character.baseHero.Confectioner += book.count;
                    }
                    break;
            }
        }
    }
}
