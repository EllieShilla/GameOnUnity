using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public string characterName;

    [Space(10)]

    public float maxHealth;
    public float curHealth;

    [Space(10)]

    public CharacterState characterState;
    public CharacterTeam characterTeam;

}

public enum CharacterTeam
{
    Friendly,
    Enemy
}

public enum CharacterState
{
    Loading,
    Idle,
    Ready,
    Attacked,
    Attacking
}
