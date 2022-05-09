using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public BaseHero baseHero;
    public Sprite characterPhoto;
    public bool inCommand;
    public List<Food> foods;
    public GameObject character;
    public float positionY;
}
