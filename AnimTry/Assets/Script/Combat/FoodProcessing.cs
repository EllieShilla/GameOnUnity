using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;


public class FoodProcessing : MonoBehaviour
{
    // Start is called before the first frame update

    private BattleStateMachine stateMachine;
    private bool isDish = false;
    GameObject PanelMiniGameWithCook;

    public void SetPanelMiniGameWithCook(GameObject PanelMiniGameWithCook)
    {
        this.PanelMiniGameWithCook = PanelMiniGameWithCook;
    }

    //выбор блюда для подачи
    public IEnumerator ChoiceFood(string foodName)
    {
        TextVariantLanguageScriptObject textVariantLanguage = new TextVariantLanguageScriptObject();

        if (GameObject.Find(ClickForSearchInfo.nameVisitor) != null)
        {
            stateMachine = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();

            StaminaChange();

            foreach (var item in stateMachine.foodOrders)
            {
                if (item.visitor.Equals(ClickForSearchInfo.nameVisitor))
                {
                    //если в заказе находится выбраное игроком блюдо, оно удалится из заказа
                    foreach (var i in item.foodList)
                        //if (i.foodName.Equals(foodName))
                        if (textVariantLanguage.FoodNameLocalization(i).Equals(foodName))
                        {
                            MiniGame.stopCook = false;
                            MiniGame.goodCook = false;
                            MiniGame.count = 0;

                            while (!MiniGame.stopCook)
                            {
                                ChanseToCreateGoodFood(i, stateMachine);

                                yield return null;
                            }
                            PanelMiniGameWithCook.SetActive(false);

                            print("MiniGame.goodCook " + MiniGame.goodCook);

                            if (MiniGame.goodCook)
                            {
                                item.foodList.Remove(i);
                                isDish = true;
                                GroupCoin.coin += i.price;
                                break;
                            }
                            else
                            {
                                isDish = false;
                                break;
                            }
                        }
                }


            }

            stateMachine.FighterList.Insert(1, GameObject.Find(ClickForSearchInfo.nameVisitor));
            stateMachine.NextStep();

            if (!isDish)
            {
                ReturnChange();

            }
            else
            {
                isDish = false;

                ShowOrder.foodName = new List<string>();

                if (stateMachine.foodOrders.Find(i => i.visitor == ClickForSearchInfo.nameVisitor).foodList.Count > 0)
                {
                    foreach (var item in stateMachine.foodOrders)
                    {
                        if (item.visitor.Equals(ClickForSearchInfo.nameVisitor))
                            foreach (Food food in item.foodList)
                                ShowOrder.foodName.Add(textVariantLanguage.FoodNameLocalization(food));
                                //ShowOrder.foodName.Add(food.foodName);

                    }
                }

                if (stateMachine.foodOrders.Find(i => i.visitor == ClickForSearchInfo.nameVisitor).foodList.Count == 0)
                {


                    stateMachine.foodOrders.RemoveAt(stateMachine.foodOrders.FindIndex(i => i.visitor == ClickForSearchInfo.nameVisitor));
                    stateMachine.EnemysInBattle.RemoveAt(stateMachine.EnemysInBattle.FindIndex(i => i.name == ClickForSearchInfo.nameVisitor));
                    stateMachine.performList.RemoveAt(stateMachine.performList.FindIndex(i => i.fighter == ClickForSearchInfo.nameVisitor));
                    GameObject.Find(ClickForSearchInfo.nameVisitor).transform.Find("ChooseV").gameObject.SetActive(false);

                    Destroy(GameObject.Find(ClickForSearchInfo.nameVisitor));
                    GameObject.Find(ClickForSearchInfo.nameVisitor).SetActive(false);

                    if (BattleStateMachine.countVisitor < BattleStateMachine.countForWin)
                    {
                        CreateVisitor createVisitor = new CreateVisitor();
                        createVisitor.NewVisitors();

                        EnemyStateMachine.nextOrder = true;
                        BattleStateMachine.countVisitor += 1;
                    }
                }


                ShowOrder.isUpdate = true;

                if (stateMachine.foodOrders.Count == 0)
                {
                    //stateMachine.BattleResultPanel.SetActive(true);
                    BattleResultShow.GoodEnd = true;
                    BattleResultShow.EndBattle = true;
                    stateMachine.BattleResultPanel.SetActive(true);

                }

            }
        }
    }

    void GoodCookAndStopCook_TRUE()
    {
        MiniGame.stopCook = true;
        MiniGame.goodCook = true;
    }
    void ChanseToCreateGoodFood(Food food, BattleStateMachine battleStateMachine)
    {

        switch (food.typeOfFood)
        {
            case Food.Type.ColdShop:
                if (battleStateMachine.HeroesInBattle[stateMachine.HeroesInBattle.FindIndex(i => i.name == stateMachine.FighterList[0].name)].GetComponent<HeroStateMaschine>().baseHeroero.ColdShop >= food.skill)
                    GoodCookAndStopCook_TRUE();
                else
                    RangeGoodFood(battleStateMachine.HeroesInBattle[stateMachine.HeroesInBattle.FindIndex(i => i.name == stateMachine.FighterList[0].name)].GetComponent<HeroStateMaschine>().baseHeroero.ColdShop, food.skill);
                break;
            case Food.Type.HotShop:
                if (battleStateMachine.HeroesInBattle[stateMachine.HeroesInBattle.FindIndex(i => i.name == stateMachine.FighterList[0].name)].GetComponent<HeroStateMaschine>().baseHeroero.HotShop >= food.skill)
                    GoodCookAndStopCook_TRUE();
                else
                    RangeGoodFood(battleStateMachine.HeroesInBattle[stateMachine.HeroesInBattle.FindIndex(i => i.name == stateMachine.FighterList[0].name)].GetComponent<HeroStateMaschine>().baseHeroero.HotShop, food.skill);
                break;
            case Food.Type.Confectioner:
                if (battleStateMachine.HeroesInBattle[stateMachine.HeroesInBattle.FindIndex(i => i.name == stateMachine.FighterList[0].name)].GetComponent<HeroStateMaschine>().baseHeroero.Confectioner >= food.skill)
                    GoodCookAndStopCook_TRUE();
                else
                    RangeGoodFood(battleStateMachine.HeroesInBattle[stateMachine.HeroesInBattle.FindIndex(i => i.name == stateMachine.FighterList[0].name)].GetComponent<HeroStateMaschine>().baseHeroero.Confectioner, food.skill);
                break;
        }


    }



    //если skill персонажа меньше чем того требует блюдо включается мини-игра, по результатам которой игрок либо приготовит блюдо либо нет
    void RangeGoodFood(float heroSkill, int foodSkill)
    {
        float deficitSkill = foodSkill - heroSkill;

        if (deficitSkill >= 1 && deficitSkill <= 3)
            MiniGame.fast = 1f;
        else if (deficitSkill >= 4 && deficitSkill <= 6)
            MiniGame.fast = 2f;
        else if (deficitSkill >= 7)
            MiniGame.fast = 3f;

        PanelMiniGameWithCook.SetActive(true);

    }



    //изменение стамины у персонажа и ее отображение. Если стамины недостаточно пропадает возможность готовить блюда
    void StaminaChange()
    {
        HeroStateMaschine heroState = stateMachine.FighterList[0].GetComponent<HeroStateMaschine>();
        if (heroState.baseHeroero.currentStamina >= 2)
        {
            heroState.baseHeroero.currentStamina -= 2;

            GameObject CharacterInformPanel = heroState.CharacterInformPanel.transform.GetChild(8).gameObject;
            CharacterInformPanel.transform.GetChild(1).GetComponent<Text>().text = heroState.baseHeroero.currentStamina + "/" + heroState.baseHeroero.stamina;

            heroState.StaminaBar.rectTransform.localScale = new Vector2(heroState.baseHeroero.currentStamina / heroState.baseHeroero.stamina * 100 / 100, heroState.StaminaBar.rectTransform.localScale.y);
        }

        Debug.Log("stamina " + heroState.baseHeroero.currentStamina);
    }

    //если персонаж подаст не то блюдо, либо его блюдо не получится из-за нехватки skill у него отнимется 1 return. Если return == 0, персонаж выбывает из игры
    void ReturnChange()
    {
        HeroStateMaschine heroState = stateMachine.FighterList[stateMachine.FighterList.Count - 1].GetComponent<HeroStateMaschine>();
        if (heroState.baseHeroero.currentReturn > 0)
        {
            heroState.baseHeroero.currentReturn -= 1;

            GameObject CharacterInformPanel = heroState.CharacterInformPanel.transform.GetChild(8).gameObject;
            CharacterInformPanel.transform.GetChild(2).GetComponent<Text>().text = heroState.baseHeroero.currentReturn + "/" + heroState.baseHeroero.Return;

            heroState.ReturnBar.rectTransform.localScale = new Vector2(100 * heroState.baseHeroero.currentReturn / heroState.baseHeroero.Return / 100, heroState.ReturnBar.rectTransform.localScale.y);
        }
        else
        {
            GameObject CharacterInformPanel = heroState.CharacterInformPanel;

            CharacterInformPanel.GetComponent<Image>().color = new Color(0, 0, 0, 100);
            CharacterInformPanel.transform.GetChild(0).GetComponent<Image>().color = new Color(97, 88, 88, 255);

            Animator animator = stateMachine.FighterList[stateMachine.FighterList.Count - 1].GetComponent<Animator>();
            animator.SetBool("IsMaxPreassure", true);
            animator.SetBool("IsStandUp", true);

            stateMachine.FighterList[stateMachine.FighterList.Count - 1].transform.Find("Selector").gameObject.SetActive(false);
            stateMachine.HeroesInBattle.RemoveAt(stateMachine.HeroesInBattle.FindIndex(i => i.name == stateMachine.FighterList[stateMachine.FighterList.Count - 1].name));
            stateMachine.FighterList.RemoveAt(stateMachine.FighterList.Count - 1);
        }
    }
}

