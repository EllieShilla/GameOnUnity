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
            GameObject.Find(item.fighter).transform.Find("ChooseV").gameObject.SetActive(false);
        }

        foreach (var item in stateMachine.foodOrders)
        {
            if (item.visitor.Equals(gameObject.name))
                foreach (Food food in item.foodList)
                    ShowOrder.foodName.Add(food.foodName);

        }

        transform.Find("ChooseV").gameObject.SetActive(true); //��������� ��������� ����������
        ShowOrder.isUpdate = true;

        stateMachine.heroInput = BattleStateMachine.HeroGUI.ACTIVATE;
    }
    void Start()
    {
        stateMachine = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        

    }

    void Update()
    {

    }
}


