using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class UseItem : MonoBehaviour
{

    bool isUse = false;
    public void Use(Item item, Character character, AddInventoryToObj inventory)
    {

        switch (item.type)
        {
            case Item.Type.Stamina:
                {
                    if (character.baseHero.currentStamina < character.baseHero.stamina)
                    {
                        if (character.baseHero.currentStamina + item.amount_of_recovery <= character.baseHero.stamina)
                        {
                            character.baseHero.currentStamina += item.amount_of_recovery;
                        }
                        else
                        {
                            character.baseHero.currentStamina = character.baseHero.stamina;
                        }

                        if (SceneManager.GetActiveScene().name.Equals("FightScene"))
                        {
                            ChangeStaminaPanelInform(character);
                        }

                        isUse = true;
                    }
                    else
                    {
                        print("Выносливость на максимуме");
                    }
                }
                break;
            case Item.Type.Pressure:
                {
                    if (character.baseHero.currentPressure > 0)
                    {
                        if (character.baseHero.currentPressure == character.baseHero.Pressure)
                        {
                            BattleStateMachine stateMachine = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
                            GameObject buffObj = stateMachine.HeroesInBattle[stateMachine.HeroesInBattle.FindIndex(buff => buff.GetComponent<HeroStateMaschine>().baseHeroero.heroName.Equals(character.baseHero.heroName))];
                            Animator animator = buffObj.GetComponent<Animator>();
                            stateMachine.FighterList.Add(buffObj);
                            animator.SetBool("IsMaxPreassure", false);
                            animator.SetBool("IsStandUp", false);
                        }

                        if (character.baseHero.currentPressure - item.amount_of_recovery >= 0)
                        {
                            character.baseHero.currentPressure -= item.amount_of_recovery;
                        }
                        else
                        {
                            character.baseHero.currentPressure = 0;
                        }

                        if (SceneManager.GetActiveScene().name.Equals("FightScene"))
                        {
                            ChangePreassurePanelInform(character);
                        }

                        isUse = true;
                    }
                    else
                    {
                        print("Понижение стресса не требуется");
                    }

                }
                break;
        }

        if (isUse)
        {
            inventory.inventoryObj.items.Remove(item);

            GameObject[] GroupCharacterPanels = GameObject.FindGameObjectsWithTag("GroupCharacterPanel");
            foreach (GameObject charPanel in GroupCharacterPanels)
                Destroy(charPanel);

            Transform panelTransform = GameObject.Find("InventoryPanel").transform;
            foreach (Transform child in panelTransform)
            {
                Destroy(child.gameObject);
            }

            InventoryController inventoryController = new InventoryController();
            inventoryController.ChoosePage(0, GameObject.Find("Inventory"));


            isUse = false;
        }
    }

    public void ShowGroup(GameObject ScrollPanelForGroup, GameObject GroupCharacterPanel, Item item)
    {
        GameObject PanelForGroup = ScrollPanelForGroup.transform.GetChild(0).gameObject;
        ScrollPanelForGroup.SetActive(true);
        PanelForGroup.SetActive(true);

        GameObject[] GroupCharacterPanels = GameObject.FindGameObjectsWithTag("GroupCharacterPanel");
        foreach (GameObject charPanel in GroupCharacterPanels)
            Destroy(charPanel);

        List<Character> group = new List<Character>();
        group.AddRange(GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>().inventoryObj.group);
        AddInventoryToObj inventorySctiptObj = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>();

        for (int i = 0; i < group.Count; i++)
        {
            BaseHero baseHero = group[i].baseHero;
            Character character = group[i];

            Image img = GroupCharacterPanel.transform.GetChild(0).gameObject.GetComponent<Image>();
            img.sprite = group[i].characterPhoto;
            img.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 3);

            Button button = GroupCharacterPanel.transform.GetChild(1).gameObject.GetComponent<Button>();
            button.name = baseHero.heroName + "_" + item.id + "_Button";

            var newItemPanel = Instantiate(GroupCharacterPanel, new Vector3(PanelForGroup.transform.position.x, PanelForGroup.transform.position.y, PanelForGroup.transform.position.z), Quaternion.identity);
            newItemPanel.transform.parent = PanelForGroup.transform;
            newItemPanel.tag = "GroupCharacterPanel";

            Button btn = GameObject.Find(button.name).GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => Use(item, character, inventorySctiptObj));
        }
    }

    void ChangeStaminaPanelInform(Character character)
    {
        BattleStateMachine stateMachine = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();

        if (GameObject.Find("ConfectionerPanel"))
        {
            GameObject[] GOS = GameObject.FindGameObjectsWithTag("FoodInPanel");
            foreach (var go in GOS)
                Destroy(go);

            ShowFoodList.confectionerFoodList = stateMachine.FighterList[0].GetComponent<ConfectionerList>();
            ShowFoodList.workDrawFood = true;
            stateMachine.FoodPanel.SetActive(false);
            stateMachine.FoodPanel.SetActive(true);
        }

        HeroStateMaschine heroState = stateMachine.FighterList.FirstOrDefault(i => i.name.Equals(character.baseHero.heroName)).GetComponent<HeroStateMaschine>();
        GameObject CharacterInformPanel = heroState.CharacterInformPanel.transform.GetChild(8).gameObject;
        CharacterInformPanel.transform.GetChild(1).GetComponent<Text>().text = heroState.baseHeroero.currentStamina + "/" + heroState.baseHeroero.stamina;

        heroState.StaminaBar.rectTransform.localScale = new Vector2(heroState.baseHeroero.currentStamina / heroState.baseHeroero.stamina * 100 / 100, heroState.StaminaBar.rectTransform.localScale.y);
    }

    void ChangePreassurePanelInform(Character character)
    {
        BattleStateMachine stateMachine = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();

        HeroStateMaschine heroState = stateMachine.FighterList.FirstOrDefault(i => i.name.Equals(character.baseHero.heroName)).GetComponent<HeroStateMaschine>();
        GameObject CharacterInformPanel = heroState.CharacterInformPanel.transform.GetChild(8).gameObject;
        CharacterInformPanel.transform.GetChild(0).GetComponent<Text>().text = heroState.baseHeroero.currentPressure + "/" + heroState.baseHeroero.Pressure;

        heroState.PreassureBar.rectTransform.localScale = new Vector2(heroState.baseHeroero.Pressure * (heroState.baseHeroero.currentPressure / 100), heroState.PreassureBar.rectTransform.localScale.y);
    }



}








