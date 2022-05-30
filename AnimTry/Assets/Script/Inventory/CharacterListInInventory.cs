using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterListInInventory : MonoBehaviour
{
    GameObject allCharacterPanel;
    GameObject aboutCharacterPanel;
    List<Character> characters;
    [SerializeField]
    GameObject CharacterPanel;
    public static bool isCreate = false;

    void Update()
    {
        if (isCreate)
        {

            characters = new List<Character>();

            if (GameObject.Find("InventoryGameObject"))
                characters.AddRange(GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>().inventoryObj.group);

            allCharacterPanel = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;

            aboutCharacterPanel = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject;
            ShowCharacter();
            InfoAboutCharacter(characters[0]);

            isCreate = false;
        }
    }

    void DestroyCharacterGrid()
    {
        if (GameObject.FindGameObjectsWithTag("CharacterListInInventory").Length > 0)
        {
            GameObject[] ItemScroll = GameObject.FindGameObjectsWithTag("CharacterListInInventory");
            foreach (GameObject item in ItemScroll)
            {
                Destroy(item);
            }
        }
    }

    void ShowCharacter()
    {
        DestroyCharacterGrid();

        TextVariantLanguageScriptObject textVariantLanguage = new TextVariantLanguageScriptObject();

        for (int i = 0; i < characters.Count; i++)
        {
            GameObject panel = CharacterPanel;
            Image img = panel.transform.GetChild(0).gameObject.GetComponent<Image>();
            img.sprite = characters[i].characterPhoto;
            //img.GetComponent<RectTransform>().sizeDelta = new Vector2(0.2f, 0.2f);

            Text charName = panel.transform.GetChild(1).gameObject.GetComponent<Text>();
            charName.text = textVariantLanguage.HeroNameLocalization(characters[i].baseHero);

            Button button = panel.transform.GetChild(2).gameObject.GetComponent<Button>();
            button.name = characters[i].baseHero.heroNameEng + "_Character_Inventory_Button";

            var newItemPanel = Instantiate(panel, new Vector3(allCharacterPanel.transform.position.x, allCharacterPanel.transform.position.y, allCharacterPanel.transform.position.z), Quaternion.identity);
            newItemPanel.transform.parent = allCharacterPanel.transform;
            newItemPanel.tag = "CharacterListInInventory";

            Character character_ = characters[i];
            Button btn = GameObject.Find(button.name).GetComponent<Button>();
            btn.onClick.AddListener(() => AboutHero(character_));
        }

    }

    void AboutHero(Character character)
    {
        InfoAboutCharacter(character);
    }

    void InfoAboutCharacter(Character character)
    {
        TextVariantLanguageScriptObject textVariantLanguage = new TextVariantLanguageScriptObject();

        aboutCharacterPanel.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>().text = textVariantLanguage.HeroNameLocalization(character.baseHero);
        aboutCharacterPanel.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = character.characterPhoto;
        aboutCharacterPanel.transform.GetChild(2).gameObject.GetComponent<Text>().text = textVariantLanguage.HeroStatLocalization()[0] + character.baseHero.HotShop + "\n"+ textVariantLanguage.HeroStatLocalization()[1] + character.baseHero.ColdShop +
            "\n" + textVariantLanguage.HeroStatLocalization()[2] + character.baseHero.Confectioner + "\n" + textVariantLanguage.HeroStatLocalization()[3]+
       + character.baseHero.Pressure + "\n" + textVariantLanguage.HeroStatLocalization()[4] + character.baseHero.stamina + "\n" + textVariantLanguage.HeroStatLocalization()[5] + character.baseHero.Return;

    }
}




