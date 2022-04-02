using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCook : MonoBehaviour
{
    [SerializeField] private GameObject HeroPlacement;
    [SerializeField] private GameObject GridForCharacterPanel;
    [SerializeField] private GameObject CharacterPanel;
    [SerializeField] private List<GameObject> enemy;
    [SerializeField] private GameObject Inventory;

    public static Character coldShopCook;
    public static Character hotShopCook;
      List<Character> team = new List<Character>();

    int count = 0;
    PositionCharacter position = new PositionCharacter();



    public void StartCooking()
    {
        HeroPlacement.SetActive(false);


        team.Add(coldShopCook);
        team.Add(hotShopCook);

        SetCharacters();

    }

    void SetCharacters()
    {
        foreach (var item in team)
        {
            GameObject characterPanel = CharacterPanel;
            characterPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = item.baseHero.heroName;

            var charPlane = Instantiate(characterPanel, new Vector3(GridForCharacterPanel.transform.position.x, GridForCharacterPanel.transform.position.y, GridForCharacterPanel.transform.position.z), Quaternion.identity);
            charPlane.transform.parent = GridForCharacterPanel.gameObject.transform;


            Vector3 vector3 = new Vector3(position.GetPositions()[count].X, item.positionY, position.GetPositions()[count].Z);
            GameObject character = Instantiate(item.character, vector3, Quaternion.identity) as GameObject;
            character.transform.Rotate(0, position.GetPositions()[count].rotation_Y, 0);

            character.GetComponent<HeroStateMaschine>().baseHeroero = item.baseHero;
            character.GetComponent<HeroStateMaschine>().character = item;


            character.GetComponent<HeroStateMaschine>().PreassureBar = charPlane.transform.GetChild(4).gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            character.GetComponent<HeroStateMaschine>().StaminaBar = charPlane.transform.GetChild(5).gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            character.GetComponent<HeroStateMaschine>().ReturnBar = charPlane.transform.GetChild(6).gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            character.GetComponent<ConfectionerList>().confectionerFood.AddRange(item.foods);


            if (item.baseHero.heroName.Equals("Standing W_Briefcase Idle"))
                character.GetComponent<Move>().inventory = Inventory;

            character.transform.tag = "Character";
            character.transform.name = item.baseHero.heroName;

            count++;
        }

        List<GameObject> enemies = new List<GameObject>();
        enemies = enemy;

        foreach (var item in enemies)
            item.SetActive(true);

        BattleStateMachine.starting = true;

    }




    private void Update()
    {


    }

}

