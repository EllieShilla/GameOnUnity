using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chest", menuName = "Chest")]
public class Chest : ScriptableObject
{
    public List<Ingridient> ingridients;
    public bool isClear = false;

}
