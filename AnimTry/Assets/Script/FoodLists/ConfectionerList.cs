using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfectionerList : MonoBehaviour
{
    [SerializeField] List<Food> startFood;
    public List<Food> confectionerFood = new List<Food>();
    void Start()
    {

            foreach (var food in startFood)
                AddFood(food);

    }

    void AddFood(Food food)
    {
        confectionerFood.Add(food);
    }
}



