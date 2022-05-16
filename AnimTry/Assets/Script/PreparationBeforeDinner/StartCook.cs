using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCook : MonoBehaviour
{
    [SerializeField] private GameObject HeroPlacement;
    [SerializeField] private GameObject GridForCharacterPanel;
    [SerializeField] private GameObject CharacterPanel;
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

        //создание и добавление посетителей на сцену
        CreateVisitor createVisitor = new CreateVisitor();
        createVisitor.NewVisitors();

        SetCharacters();

    }

    //создание и добавление выбранных персонажей на сцену и привязка к ним панелей с информацией
    void SetCharacters()
    {
        count = 0;

        foreach (var item in team)
        {
            item.baseHero.currentReturn = item.baseHero.Return;

            GameObject characterPanel = CharacterPanel;
            characterPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = item.baseHero.heroName;
            GameObject detailInfo = characterPanel.transform.GetChild(8).gameObject;
            detailInfo.transform.GetChild(0).GetComponent<Text>().text = item.baseHero.currentPressure+"/"+item.baseHero.Pressure;
            detailInfo.transform.GetChild(1).GetComponent<Text>().text = item.baseHero.currentStamina+"/"+item.baseHero.stamina;
            detailInfo.transform.GetChild(2).GetComponent<Text>().text = item.baseHero.currentReturn+"/"+item.baseHero.Return;

            var charPlane = Instantiate(characterPanel, new Vector3(GridForCharacterPanel.transform.position.x, GridForCharacterPanel.transform.position.y, GridForCharacterPanel.transform.position.z), Quaternion.identity);
            charPlane.transform.parent = GridForCharacterPanel.gameObject.transform;


            Vector3 vector3 = new Vector3(position.GetPositions()[count].X, item.positionY, position.GetPositions()[count].Z);
            GameObject character = Instantiate(item.character, vector3, Quaternion.identity) as GameObject;
            character.transform.Rotate(0, position.GetPositions()[count].rotation_Y, 0);

            HeroStateMaschine heroStateMaschineChar = character.GetComponent<HeroStateMaschine>();

            heroStateMaschineChar.baseHeroero = item.baseHero;
            heroStateMaschineChar.character = item;

            heroStateMaschineChar.PreassureBar = charPlane.transform.GetChild(5).gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            heroStateMaschineChar.PreassureBar.rectTransform.localScale = new Vector2(item.baseHero.Pressure * (item.baseHero.currentPressure / 100), heroStateMaschineChar.PreassureBar.rectTransform.localScale.y);

            heroStateMaschineChar.StaminaBar = charPlane.transform.GetChild(6).gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            heroStateMaschineChar.StaminaBar.rectTransform.localScale = new Vector2(item.baseHero.currentStamina / item.baseHero.stamina * 100 / 100, heroStateMaschineChar.StaminaBar.rectTransform.localScale.y);

            heroStateMaschineChar.ReturnBar = charPlane.transform.GetChild(7).gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            character.GetComponent<ConfectionerList>().confectionerFood.AddRange(item.foods);

            heroStateMaschineChar.CharacterInformPanel = charPlane;

            character.transform.tag = "Character";
            character.transform.name = item.baseHero.heroName;
            Animator animator= character.GetComponent<Animator>();
            animator.SetBool("IsFight",true);

            count++;
        }

        List<GameObject> enemies = new List<GameObject>();
        enemies.AddRange(GameObject.Find("VisitorList").GetComponent<VisitorList>().visitors);

        foreach (var item in enemies)
            item.SetActive(true);

        BattleStateMachine.starting = true;

    }
}

