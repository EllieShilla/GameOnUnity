using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private GameObject DialoguePanel;
    [SerializeField]
    private Button ChoiceButton;
    public Story story;
    [SerializeField]
    private GameObject ContinueDialogueButton;  
    [SerializeField]
    private GameObject ChoiceButtonPanel;


    static DialogueManager instance;
    public bool dialogueIsPlaying { get; private set; }

     void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        dialogueIsPlaying = false;
    }
    public void StartDialogue(TextAsset inkJSON)
    {
        DialoguePanel.SetActive(true);

        story = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        RefreshView();
    }

   public void RefreshView()
    {
        RemoveChoice();

        if (story.canContinue)
        {
            string text = story.Continue();
            text = text.Trim();
            CreateContextView(text);

            //print(text);

        }
        else
        {
            dialogueIsPlaying = false;
            DialoguePanel.SetActive(false);
            ChoiceButtonPanel.SetActive(false);
        }

        if (story.currentChoices.Count > 0)
        {
            ChoiceButtonPanel.SetActive(true);
            ContinueDialogueButton.SetActive(false);

            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                Button btn = CreateChoiceView(choice.text.Trim());

                btn.onClick.AddListener(delegate
                {
                    OnClickChoiceButton(choice);
                });
            }
        }
        else
            ContinueDialogueButton.SetActive(true);

    }

    void CreateContextView(string letters)
    {
        DialoguePanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = letters;
    }

    Button CreateChoiceView(string choiceText)
    {
        Button btn = ChoiceButton;
        if (choiceText.Split()[0].Equals("_PR_"))
            btn.transform.GetChild(0).gameObject.GetComponent<Text>().text = choiceText.Substring(4, choiceText.Length-4);
        else
            btn.transform.GetChild(0).gameObject.GetComponent<Text>().text = choiceText;
        btn.name = choiceText;
        var newChoice = Instantiate(btn, new Vector3(ChoiceButtonPanel.transform.position.x, ChoiceButtonPanel.transform.position.y, ChoiceButtonPanel.transform.position.z), Quaternion.identity);
        newChoice.transform.parent = ChoiceButtonPanel.transform;
        newChoice.tag = "ChoiceButtonDialogue";
        return newChoice;
    }

    void OnClickChoiceButton(Choice choice)
    {
        //—юда вставить открытие магазина!!!!!!!!!!!!!!!!!!
        print(choice.text);
        //if (choice.text.Split()[0].Equals("_PR_"))
        //print(choice.text.Split()[0]);
        story.ChooseChoiceIndex(choice.index);
        RefreshView();
    }
    void RemoveChoice()
    {
        foreach (var i in GameObject.FindGameObjectsWithTag("ChoiceButtonDialogue"))
            Destroy(i);

        ChoiceButtonPanel.SetActive(false);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }
}


