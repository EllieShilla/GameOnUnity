using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FirstDialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField]
    TextAsset inkJSON;
    [SerializeField]
    TextAsset inkJSON_ENG;

    bool FirstDialogueStart = false;
    private void Update()
    {

        if (!File.Exists(Application.persistentDataPath + "/data.save") && !FirstDialogueStart)
        {
            FirstDialogueStart = true;

            if (LocalizationManager.Language.Equals("English"))
                DialogueManager.GetInstance().StartDialogue(inkJSON_ENG, null, null);
            else
                DialogueManager.GetInstance().StartDialogue(inkJSON, null, null);

            StartCoroutine(save());
        }
    }

    IEnumerator save()
    {
        yield return new WaitForSeconds(1f);
        GameObject player = GameObject.Find("MainCharacter");
        AddInventoryToObj inventory = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>();
        BinarySavingSystem.SavePlayer(inventory, player);
    }
}
