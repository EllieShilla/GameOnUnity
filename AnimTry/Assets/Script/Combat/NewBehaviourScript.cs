using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NewBehaviourScript
{
    public string fighter;
    public string Type;
    public GameObject FighterObj;
    public GameObject FighterTarget;

    public int foodListCount;
    public List<Food> foodList=new List<Food>();
}
