using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCharacter : MonoBehaviour
{
    [SerializeField] private GameObject parentPanel;
    [SerializeField] private GameObject characterPanel;
    private List<Character> characters;
    GameObject panel;
    public void UpdatePanel()
    {
        ShowCharacter();
    }

    private void Awake()
    {
        //GameObject activePanel = panel;
        //activePanel.SetActive(true);

        characters = new List<Character>();
        characters.AddRange(GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>().inventoryObj.group);
        panel = parentPanel.transform.GetChild(0).gameObject;

        ShowCharacter();
    }

    void ShowCharacter()
    {

        if (GameObject.FindGameObjectsWithTag("CharacterInPanel").Length > 0)
        {
            GameObject[] CharacterInPanel = GameObject.FindGameObjectsWithTag("CharacterInPanel");
            foreach (GameObject item in CharacterInPanel)
            {
                Destroy(item);
            }
        }

        foreach (var character in characters)
        {
            if (character.inCommand)
            {
                TextVariantLanguageScriptObject textVariantLanguage = new TextVariantLanguageScriptObject();

                GameObject charPanel = characterPanel;

                GameObject nameChar = charPanel.transform.GetChild(0).gameObject;
                nameChar.GetComponent<Text>().text = textVariantLanguage.HeroNameLocalization(character.baseHero);

                GameObject coldShop = charPanel.transform.GetChild(1).gameObject;
                coldShop.GetComponent<Text>().text = textVariantLanguage.HeroStatLocalization()[1] + character.baseHero.ColdShop.ToString();

                GameObject hotShop = charPanel.transform.GetChild(2).gameObject;
                hotShop.GetComponent<Text>().text = textVariantLanguage.HeroStatLocalization()[0] + character.baseHero.HotShop.ToString();

                GameObject Confectioner = charPanel.transform.GetChild(3).gameObject;
                Confectioner.GetComponent<Text>().text = textVariantLanguage.HeroStatLocalization()[2] + character.baseHero.Confectioner.ToString();

                Button b = charPanel.transform.GetChild(5).gameObject.GetComponent<Button>();
                SetNameToButton(b, character);

                GameObject img = charPanel.transform.GetChild(4).gameObject;
                img.GetComponent<Image>().sprite = character.characterPhoto;

                var newCharPlane = Instantiate(charPanel, new Vector3(panel.transform.position.x, panel.transform.position.y, panel.transform.position.z), Quaternion.identity);
                newCharPlane.transform.parent = panel.gameObject.transform;
                newCharPlane.tag = "CharacterInPanel";
                newCharPlane.name = "Character " + character.baseHero.heroNameEng;

                SetOnClick(b, character);
            }
        }
    }

    void ChoiceCharacter(Character character)
    {
        GameObject img = gameObject.transform.GetChild(0).gameObject;
        img.GetComponent<Image>().sprite = character.characterPhoto;
        img.name = character.baseHero.heroNameEng + "_" + gameObject.name + "Image";
        //Destroy(GameObject.Find("Character " + character.baseHero.heroName));
        switch (gameObject.name)
        {
            case "HotShopButton":
                StartCook.hotShopCook = character;
                break;
            case "ColdShopButton":
                StartCook.coldShopCook = character;
                break;
        }
    }

    private void Update()
    {
        //проверка панелек персонажей. Если персонаж выбран, его панелька будет неактивна и наоборот
        if (GameObject.FindGameObjectsWithTag("CharacterInPanel").Length > 0)
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("CharacterInPanel"))
            {
                if (StartCook.hotShopCook != null && StartCook.coldShopCook == null)
                {
                    if (item.name.Split(' ')[1].Equals(StartCook.hotShopCook.baseHero.heroNameEng) )
                        item.transform.GetChild(5).GetComponent<Button>().interactable = false;
                    else
                        item.transform.GetChild(5).GetComponent<Button>().interactable = true;
                }
                else if ( StartCook.coldShopCook != null && StartCook.hotShopCook == null)
                {
                    if (item.name.Split(' ')[1].Equals(StartCook.coldShopCook.baseHero.heroNameEng))
                        item.transform.GetChild(5).GetComponent<Button>().interactable = false;
                    else
                        item.transform.GetChild(5).GetComponent<Button>().interactable = true;
                }
                else if (StartCook.coldShopCook != null && StartCook.hotShopCook != null)
                {
                    if (item.name.Split(' ')[1].Equals(StartCook.coldShopCook.baseHero.heroNameEng) || item.name.Split(' ')[1].Equals(StartCook.hotShopCook.baseHero.heroNameEng))
                        item.transform.GetChild(5).GetComponent<Button>().interactable = false;
                    else
                        item.transform.GetChild(5).GetComponent<Button>().interactable = true;
                }
            }
        }
    }


    void SetNameToButton(Button button, Character character)
    {
        switch (gameObject.name)
        {
            case "HotShopButton":
                button.name = character.baseHero.heroNameEng + " HotShopButton";
                break;
            case "ColdShopButton":
                button.name = character.baseHero.heroNameEng + " ColdShopButton";
                break;
        }
    }

    void SetOnClick(Button button, Character character)
    {
        Button btn;
        switch (gameObject.name)
        {
            case "HotShopButton":
                btn = GameObject.Find(character.baseHero.heroNameEng + " HotShopButton").GetComponent<Button>();
                btn.onClick.AddListener(delegate { ChoiceCharacter(character); });
                break;
            case "ColdShopButton":
                btn = GameObject.Find(character.baseHero.heroNameEng + " ColdShopButton").GetComponent<Button>();
                btn.onClick.AddListener(delegate { ChoiceCharacter(character); });
                break;
        }
    }
}

