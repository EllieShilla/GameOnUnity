using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotShopList : MonoBehaviour
{
    [SerializeField] List<Food> startFood = new List<Food>();
    List<Food> hotShopList = new List<Food>();
    void Start()
    {
        for (int i = 0; i < startFood.Count; i++)
        {
            AddFood(startFood[i]);
        }
    }

    void AddFood(Food food)
    {
        hotShopList.Add(food);
    }
}
