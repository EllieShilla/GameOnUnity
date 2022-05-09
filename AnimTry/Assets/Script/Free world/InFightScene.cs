using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class InFightScene : MonoBehaviour
{
    public Cafe cafe;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Character"))
        {
            CafeForCooking.ChooseCafe.CafeName = this.gameObject.GetComponent<InFightScene>().cafe.CafeName;

            foreach (var character in GameObject.FindGameObjectsWithTag("Character"))
                CharacterFoodComand.teamMembers.Add(character.name);

                SceneManager.LoadScene("FightScene");
        }
    }
}



