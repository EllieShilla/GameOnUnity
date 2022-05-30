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
    public string dataTypeGoal;
    public string dataTypeRewards;
    public Character questGiver;
}
