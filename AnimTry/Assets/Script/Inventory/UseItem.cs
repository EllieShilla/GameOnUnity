using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{

    bool isUse = false;
    public void Use(Item item, Character character)
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
                        if (character.baseHero.currentPressure - item.amount_of_recovery >= 0)
                        {
                            character.baseHero.currentPressure -= item.amount_of_recovery;
                        }
                        else
                        {
                            character.baseHero.currentPressure = 0;
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
            character.items.Remove(item);

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

    public void ShowGroup(GameObject PanelForGroup, GameObject GroupCharacterPanel,Item item)
    {
        PanelForGroup.SetActive(true);

        foreach (var i in GameObject.FindGameObjectsWithTag("GroupCharacterPanel"))
        Destroy(i);

        List<Character> group = new List<Character>();
        group.AddRange(GameObject.Find("Standing W_Briefcase Idle").GetComponent<Player>().group);

        for (int i = 0; i < group.Count; i++)
        {
            BaseHero baseHero = group[i].baseHero;
            Character character = group[i];

            Image img = GroupCharacterPanel.transform.GetChild(0).gameObject.GetComponent<Image>();
            img.sprite = group[i].characterPhoto;
            img.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 3);

            Button button = GroupCharacterPanel.transform.GetChild(3).gameObject.GetComponent<Button>();
            button.name = baseHero.heroName+"_"+ item.id + "_Button";

            var newItemPanel = Instantiate(GroupCharacterPanel, new Vector3(PanelForGroup.transform.position.x, PanelForGroup.transform.position.y, PanelForGroup.transform.position.z), Quaternion.identity);
            newItemPanel.transform.parent = PanelForGroup.transform;
            newItemPanel.tag = "GroupCharacterPanel";

            Button btn = GameObject.Find(button.name).GetComponent<Button>();
            btn.onClick.AddListener(() => Use(item, character));
        }
    }
  
}



