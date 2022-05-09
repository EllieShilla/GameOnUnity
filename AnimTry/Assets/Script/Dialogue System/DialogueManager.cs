using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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

    private GameObject ShopPanel;
    private Cafe cafe;


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
    public void StartDialogue(TextAsset inkJSON, GameObject ShopPanel, Cafe cafe)
    {
        this.ShopPanel = ShopPanel;
        this.cafe = cafe;
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
            string charName = "";
            string text = story.Continue();
            if (text.Contains("["))
            {
                charName = text.Remove(0, 1);
                charName = charName.Remove(text.Length - 3, 2);
                text = story.Continue();
            }

            text = text.Trim();
            CreateContextView(text,charName);


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

    void CreateContextView(string letters, string charName)
    {
        Character character = GameObject.Find(charName).transform.GetComponent<Player>().character;
        DialoguePanel.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>().text = character.baseHero.heroName;
        DialoguePanel.transform.GetChild(3).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = character.characterPhoto;
        DialoguePanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = letters;
    }

    Button CreateChoiceView(string choiceText)
    {
        Button btn = ChoiceButton;
        if (choiceText.Split()[0].Equals("_PR_"))
            btn.transform.GetChild(0).gameObject.GetComponent<Text>().text = choiceText.Substring(4, choiceText.Length - 4);
        else if (choiceText.Split()[0].Equals("_FIGHT_"))
            btn.transform.GetChild(0).gameObject.GetComponent<Text>().text = choiceText.Substring(7, choiceText.Length - 7);
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
        if (choice.text.Split()[0].Equals("_PR_"))
        {
            ShopPanel.SetActive(true);
            ShopPanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>().inventoryObj.money.ToString();
        }
        else if (choice.text.Split()[0].Equals("_FIGHT_"))
        {
            SaveScriptBeforeFight.saveFight = true;
            CafeForCooking.ChooseCafe = cafe;
            SceneManager.LoadScene("FightScene");
        }

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


