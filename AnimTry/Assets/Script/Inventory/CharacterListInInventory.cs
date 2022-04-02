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

            if (GameObject.Find("Standing W_Briefcase Idle").GetComponent<Player>())
                characters.AddRange(GameObject.Find("Standing W_Briefcase Idle").GetComponent<Player>().group);

            allCharacterPanel = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;
            aboutCharacterPanel = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject;
            ShowCharacter();
                    InfoAboutCharacter(characters[0]);

            isCreate = false;
        }
    }


    void ShowCharacter()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            GameObject panel = CharacterPanel;
            Image img = panel.transform.GetChild(0).gameObject.GetComponent<Image>();
            img.sprite = characters[i].characterPhoto;
            img.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 2.7f);

            Text charName = panel.transform.GetChild(1).gameObject.GetComponent<Text>();
            charName.text = characters[i].baseHero.heroName;

            Button button = panel.transform.GetChild(2).gameObject.GetComponent<Button>();
            button.name = characters[i].baseHero.heroName + "_Character_Inventory_Button";

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
        aboutCharacterPanel.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>().text = character.baseHero.heroName;
        aboutCharacterPanel.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = character.characterPhoto;
        aboutCharacterPanel.transform.GetChild(2).gameObject.GetComponent<Text>().text = "HotShop: " + character.baseHero.HotShop+ "\nColdShop: " + character.baseHero.ColdShop + "\nConfectioner: " + character.baseHero.Confectioner +
            "\nPressure: "+character.baseHero.Pressure+ "\nStamina: " + character.baseHero.stamina + "\nReturn: " + character.baseHero.Return;

    }
}



