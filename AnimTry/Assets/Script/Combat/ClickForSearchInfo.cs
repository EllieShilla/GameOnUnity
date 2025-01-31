using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class ClickForSearchInfo : MonoBehaviour, IPointerClickHandler
{
    private BattleStateMachine stateMachine;
    public static string nameVisitor="";



    //��� ������� �� ������� �� ����������. ���������� ������ ��� ����������� ��� ������
    public void OnPointerClick(PointerEventData eventData)
    {
        stateMachine.heroInput = BattleStateMachine.HeroGUI.DONE;

        stateMachine.ActiveOrderPanel();

        if (!nameVisitor.Equals(gameObject.name))
        nameVisitor = gameObject.name;

        ShowOrder.foodName = new List<string>();

        //������ ���������� ��������� ���� �����������
        foreach (var item in stateMachine.performList)
        {
            if(GameObject.Find(item.fighter))
            GameObject.Find(item.fighter).transform.Find("ChooseV").gameObject.SetActive(false);

        }

        TextVariantLanguageScriptObject textVariantLanguage = new TextVariantLanguageScriptObject();


        foreach (var item in stateMachine.foodOrders)
        {
            if (item.visitor.Equals(gameObject.name))
                foreach (Food food in item.foodList)
                    ShowOrder.foodName.Add(textVariantLanguage.FoodNameLocalization(food));
        }

        transform.Find("ChooseV").gameObject.SetActive(true); //��������� ��������� ����������
        ShowOrder.isUpdate = true;

        stateMachine.heroInput = BattleStateMachine.HeroGUI.ACTIVATE;
    }
    void Start()
    {
        stateMachine = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        

    }

}


