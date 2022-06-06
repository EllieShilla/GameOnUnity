using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.SimpleLocalization;
using System.Linq;

public class LearningDialogue : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField]
    public TextAsset inkJSON;
    [SerializeField]
    public TextAsset inkJSON_ENG;
    bool inFirstTime = false;
    int indexDialogue = 0;
    public static bool isStartDialogue = false;
    private void Awake()
    {
        isStartDialogue = true;
    }

    private void Update()
    {
        if (isStartDialogue)
        {
            ShowDialogue();
        }
    }

    void ShowDialogue()
    {

        inkJSON = this.gameObject.GetComponent<LearningDialogue>().inkJSON;
        inkJSON_ENG = this.gameObject.GetComponent<LearningDialogue>().inkJSON_ENG;

        for (int i = 0; i < LearnDialogBool.dialogueInfo.Length; i++)
        {
            if (LearnDialogBool.dialogueInfo[i][0].Equals(inkJSON.name) && LearnDialogBool.dialogueInfo[i][1].Equals("false"))
            {
                inFirstTime = true;
                indexDialogue = i;
                break;
            }
        }

        if (inFirstTime)
            {
                if (LocalizationManager.Language.Equals("English"))
                    DialogueManager.GetInstance().StartDialogue(inkJSON_ENG, null, null);
                else
                    DialogueManager.GetInstance().StartDialogue(inkJSON, null, null);

                LearnDialogBool.dialogueInfo[indexDialogue][1] = "true";

                BinarySavingSystem.SavePlayerLearnDialogue(indexDialogue);
            isStartDialogue = false;

            this.gameObject.GetComponent<LearningDialogue>().enabled = false;
        }
    }
}

