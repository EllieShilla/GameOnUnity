using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadBook : MonoBehaviour
{
    private void Start()
    {
        LoadBooksOnScene();
    }

    public static void LoadBooksOnScene()
    {
        PlayerData data = BinarySavingSystem.LoadPlayer();

        if (data.booksName != null)
        {
            if (GameObject.Find("Books"))
            {
                GameObject book = GameObject.Find("Books");

                for (int i = 0; i < book.transform.childCount; i++)
                {
                    string bookName = data.booksName.FirstOrDefault(y => y.Split('_')[0].Equals(GameObject.Find("Books").transform.GetChild(i).name));
                    if (bookName != null)
                    {
                        BooksWithStats bookObj = GameObject.Find(bookName.Split('_')[0]).GetComponent<ItemToInventory>().book;
                        bookObj.isLoot = Convert.ToBoolean(bookName.Split('_')[1]);
                    }
                }
            }
        }
        else
        {
            if (GameObject.Find("Books"))
            {
                GameObject book = GameObject.Find("Books");

                for (int i = 0; i < book.transform.childCount; i++)
                {
                    BooksWithStats bookObj = book.transform.GetChild(i).GetComponent<ItemToInventory>().book;
                    bookObj.isLoot = false;
                }
            }
        }
    }
}

