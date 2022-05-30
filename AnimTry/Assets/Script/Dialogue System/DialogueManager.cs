using System;
using System.Linq;
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

    string charName = "";

    static DialogueManager instance;

    QuestSystem questSystem;
    public bool dialogueIsPlaying { get; private set; }

    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        dialogueIsPlaying = false;
        questSystem = new QuestSystem();
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
            string text = story.Continue();
            if (text.Contains("["))
            {
                charName = text.Remove(0, 1);
                charName = charName.Remove(text.Length - 3, 2);
                text = story.Continue();
            }

            text = text.Trim();
            CreateContextView(text, charName);

            Move.canMove = false;
        }
        else
        {
            dialogueIsPlaying = false;
            DialoguePanel.SetActive(false);
            ChoiceButtonPanel.SetActive(false);
            Move.canMove = true;
        }

        if (story.currentChoices.Count > 0)
        {
            ChoiceButtonPanel.SetActive(true);
            ContinueDialogueButton.SetActive(false);

            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
               
                switch (choice.text.Trim().Split()[0])
                {
                    case "_QUEST_":
                        {
                            Quest quest_ = Resources.LoadAll<Quest>("ScriptObj/Quests").FirstOrDefault(i => i.name.Equals(choice.text.Split()[1]));
                            InventoryObj inventoryObj = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>().inventoryObj;

                            if (inventoryObj.quests.FirstOrDefault(i=>i.name.Equals(quest_.name)) == null)
                            {
                                CreateButtonChoice(choice);
                            }
                        }
                        break;
                    case "_QUESTCONTINUE_":
                        {
                            Quest quest_ = Resources.LoadAll<Quest>("ScriptObj/Quests").FirstOrDefault(i => i.name.Equals(choice.text.Split()[1]));
                            InventoryObj inventoryObj = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>().inventoryObj;

                            if (inventoryObj.quests.FirstOrDefault(i => i.name.Equals(quest_.name)))
                            {
                                CreateButtonChoice(choice);
                            }
                        }
                        break;
                    case "_QUESTDONE_":
                        {
                            if (questSystem.QuetsSearch(GameObject.Find(charName).GetComponent<Player>().quest) && !GameObject.Find(charName).GetComponent<Player>().quest.isCompleted)
                            {
                                CreateButtonChoice(choice);
                            }
                        }
                        break;
                    default:
                        {
                            CreateButtonChoice(choice);
                        }
                        break;
                }
            }
        }
        else
            ContinueDialogueButton.SetActive(true);

    }

    void CreateButtonChoice(Choice options)
    {
        Button btn = CreateChoiceView(options.text.Trim());

        btn.onClick.AddListener(delegate
        {
            OnClickChoiceButton(options);
        });
    }

    void CreateContextView(string letters, string charName)
    {
        TextVariantLanguageScriptObject textVariantLanguage = new TextVariantLanguageScriptObject();
        Character character = GameObject.Find(charName).transform.GetComponent<Player>().character;
        DialoguePanel.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>().text = textVariantLanguage.HeroNameLocalization(character.baseHero);
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
        else if (choiceText.Split()[0].Equals("_QUEST_"))
            btn.transform.GetChild(0).gameObject.GetComponent<Text>().text = choiceText.Substring((7 + choiceText.Split()[1].Length + 2), choiceText.Length - (7 + choiceText.Split()[1].Length + 2));
        else if (choiceText.Split()[0].Equals("_QUESTCONTINUE_"))
            btn.transform.GetChild(0).gameObject.GetComponent<Text>().text = choiceText.Substring((15 + choiceText.Split()[1].Length + 2), choiceText.Length - (15 + choiceText.Split()[1].Length + 2));
        else if (choiceText.Split()[0].Equals("_QUESTDONE_"))
            btn.transform.GetChild(0).gameObject.GetComponent<Text>().text = choiceText.Substring((11 + choiceText.Split()[1].Length + 2), choiceText.Length - (11 + choiceText.Split()[1].Length + 2));
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
        else if (choice.text.Split()[0].Equals("_QUEST_"))
        {
            Quest quest_ = Resources.LoadAll<Quest>("ScriptObj/Quests").FirstOrDefault(i => i.name.Equals(choice.text.Split()[1]));

            InventoryObj inventoryObj = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>().inventoryObj;
            if (!inventoryObj.quests.Find(i => i.name.Equals(quest_.name)))
            {
                quest_.isTaken = true;
                quest_.isCompleted = false;
                inventoryObj.quests.Add(quest_);
                SaveQuest();
            }
        }
        else if (choice.text.Split()[0].Equals("_QUESTCONTINUE_"))
        {
            Quest quest_ = Resources.LoadAll<Quest>("ScriptObj/Quests").FirstOrDefault(i => i.name.Equals(choice.text.Split()[1]));
            questSystem.QuestContinue(quest_);
            SaveQuest();
        }
        else if (choice.text.Split()[0].Equals("_QUESTDONE_"))
        {
            Quest quest_ = Resources.LoadAll<Quest>("ScriptObj/Quests").FirstOrDefault(i => i.name.Equals(choice.text.Split()[1]));
            questSystem.EndQuest(quest_);
            quest_.isCompleted = true;
            questSystem.Reward(quest_);

            SaveQuest();
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

    private void SaveQuest()
    {
        GameObject player = GameObject.Find("MainCharacter");
        AddInventoryToObj inventory = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>();
        BinarySavingSystem.SavePlayer(inventory, player);
    }
}


