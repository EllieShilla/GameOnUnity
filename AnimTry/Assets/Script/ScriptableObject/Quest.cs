using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest:ScriptableObject
{
    public string QuestTitle_ENG;
    public string QuestTitle_RUS;
    public string QuestDescription_ENG;
    public string QuestDescription_RUS;
    public Goal[] goals;
    public Reward[] rewards;
    public bool isTaken;
    public bool isCompleted;

    public enum TypeGoal
    {
        IntType,
        StringIntType,
        ItemType,
        IngridientType
    };

    public enum TypeRewards
    {
        Money,
        ColdShop,
        HotShop,
        Confectioner,
        itemData,
        ingridientData,
        message,
        none
    };

    public TypeGoal dataTypeGoal;
    public TypeRewards dataTypeRewards;
    public Character questGiver;
}
