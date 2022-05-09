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
                GameObject charPanel = characterPanel;

                GameObject nameChar = charPanel.transform.GetChild(0).gameObject;
                nameChar.GetComponent<Text>().text = character.baseHero.heroName;

                GameObject coldShop = charPanel.transform.GetChild(1).gameObject;
                coldShop.GetComponent<Text>().text = "ColdShop: " + character.baseHero.ColdShop.ToString();

                GameObject hotShop = charPanel.transform.GetChild(2).gameObject;
                hotShop.GetComponent<Text>().text = "HotShop: " + character.baseHero.HotShop.ToString();

                GameObject Confectioner = charPanel.transform.GetChild(3).gameObject;
                Confectioner.GetComponent<Text>().text = "Confectioner: " + character.baseHero.Confectioner.ToString();

                Button b = charPanel.transform.GetChild(5).gameObject.GetComponent<Button>();
                SetNameToButton(b, character);

                GameObject img = charPanel.transform.GetChild(4).gameObject;
                img.GetComponent<Image>().sprite = character.characterPhoto;

                var newCharPlane = Instantiate(charPanel, new Vector3(panel.transform.position.x, panel.transform.position.y, panel.transform.position.z), Quaternion.identity);
                newCharPlane.transform.parent = panel.gameObject.transform;
                newCharPlane.tag = "CharacterInPanel";
                newCharPlane.name = "Character " + character.baseHero.heroName;

                SetOnClick(b, character);
            }
        }
    }

    void ChoiceCharacter(Character character)
    {
        GameObject img = gameObject.transform.GetChild(0).gameObject;
        img.GetComponent<Image>().sprite = character.characterPhoto;
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


    void SetNameToButton(Button button, Character character)
    {
        switch (gameObject.name)
        {
            case "HotShopButton":
                button.name = character.baseHero.heroName + " HotShopButton";
                break;
            case "ColdShopButton":
                button.name = character.baseHero.heroName + " ColdShopButton";
                break;
        }
    }

    void SetOnClick(Button button, Character character)
    {
        Button btn;
        switch (gameObject.name)
        {
            case "HotShopButton":
                btn = GameObject.Find(character.baseHero.heroName + " HotShopButton").GetComponent<Button>();
                btn.onClick.AddListener(delegate { ChoiceCharacter(character); });
                break;
            case "ColdShopButton":
                btn = GameObject.Find(character.baseHero.heroName + " ColdShopButton").GetComponent<Button>();
                btn.onClick.AddListener(delegate { ChoiceCharacter(character); });
                break;
        }
    }
}


