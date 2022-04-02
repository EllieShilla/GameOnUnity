using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cafe", menuName = "Cafe")]
public class Cafe : ScriptableObject
{
    public string CafeName;
    public List<Food> Menu;
    public int CountOfVisitors;

}
