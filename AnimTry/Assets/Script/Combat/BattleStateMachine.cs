using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleStateMachine : MonoBehaviour
{
    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION
    }

    public PerformAction battleState;

    public List<NewBehaviourScript> performList = new List<NewBehaviourScript>();
    public List<CharacterObjInf> cookerList = new List<CharacterObjInf>();
    public List<GameObject> HeroesInBattle = new List<GameObject>();
    public List<GameObject> EnemysInBattle = new List<GameObject>();
    bool isNextStepVisitor = false;

    GameObject OrderPanel;

    private Button nextButton;
    private Button cookButton;


    public enum HeroGUI
    {
        ACTIVATE,
        WAITING,
        INPUT,
        DONE
    }

    public List<GameObject> HeroesToManage = new List<GameObject>();

    [SerializeField]
    public GameObject FoodPanel;

    public HeroGUI heroInput;
    public static bool newVisitor = false;
    ShowFoodList showFoodList;

    bool isReady = false;

    void MyStart()
    {
        OrderPanel = GameObject.Find("OrderPanel");
        OrderPanel.SetActive(false);
        nextButton = GameObject.Find("NextStep").GetComponent<Button>();
        cookButton = GameObject.Find("Cook").GetComponent<Button>();
        nextButton.onClick.AddListener(delegate { NextStep(); });
        cookButton.onClick.AddListener(delegate { ActiveFoodListPanel(); });

        battleState = PerformAction.WAIT;
        EnemysInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        HeroesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Character"));

        heroInput = HeroGUI.ACTIVATE;
        FoodPanel.SetActive(false);
        CreateFighterList();
        SelectorPrepare();

        ShowFoodList.confectionerFoodList = GameObject.Find(FighterList[0]).GetComponent<ConfectionerList>();
        ShowFoodList.workDrawFood = true;

        isReady = true;
        countVisitor = 0;
    }

    public static int countVisitor = 0;
    public static int countForWin = 3;
    public static bool starting = false;

    void Update()
    {
        if (starting)
        {
            MyStart();
            starting = false;
        }
        if (isReady)
        {


            if (newVisitor)
            {
                foreach (var i in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    if (!i.name.Equals(EnemysInBattle[0].name))
                        EnemysInBattle.Add(i);
                }
                newVisitor = false;

                EnemysInBattle[1].GetComponent<EnemyStateMachine>().NewVisitor();

            }

            switch (battleState)
            {
                case (PerformAction.WAIT):
                    if (performList.Count > 0)
                    {
                        battleState = PerformAction.TAKEACTION;
                    }

                    break;
                case (PerformAction.TAKEACTION):
                    if (isNextStepVisitor)
                        NextStep();
                    break;
                case (PerformAction.PERFORMACTION):

                    break;
            }

        }
    }

    public void CollectActions(NewBehaviourScript input)
    {
        performList.Insert(0,input);
    }

    public void AddCooker(CharacterObjInf input)
    {
        cookerList.Add(input);
    }

    public List<FoodOrder> foodOrders = new List<FoodOrder>();

    public void Order(FoodOrder input)
    {
        foodOrders.Add(input);
    }

    void HeroInputDone()
    {
        HeroesToManage[0].transform.Find("Selector").gameObject.SetActive(false);
        HeroesToManage.RemoveAt(0);
        heroInput = HeroGUI.ACTIVATE;
    }

    public List<string> FighterList = new List<string>();

    void CreateFighterList()
    {
        foreach (var i in HeroesInBattle)
            FighterList.Add(i.name.ToString());
    }

    public void NextStep()
    {
        
        if (FighterList[0].IndexOf("V") == 0)
        {
            OrderPanel.SetActive(false);

                int indexVisitor = performList.FindIndex(item => item.fighter.Equals(ClickForSearchInfo.nameVisitor));

            if (performList.Count == 1)
                 indexVisitor = 0;

                //выбор героя который подавал блюдо
                for (int i = 0; i < performList.Count; i++)
                {
                    performList[i].FighterTarget = HeroesInBattle.Find(i => i.name == FighterList[2]);
                }



            //if (performList[0].fighter.Equals(ClickForSearchInfo.nameVisitor))
            //{
                GameObject performer = GameObject.Find(performList[indexVisitor].fighter);
                    if (performList[indexVisitor].Type.Equals("BaseVisitor"))
                    {
                        EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
                        ESM.HeroToAttack = performList[indexVisitor].FighterTarget;
                        ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                    }
            //}


            GameObject.Find(FighterList[0]).transform.Find("Selector").gameObject.SetActive(false);
                FighterList.RemoveAt(0);

        }
        else
        {
            HeroStateMaschine heroState = GameObject.Find(FighterList[0]).GetComponent<HeroStateMaschine>();

            if (heroState.baseHeroero.currentReturn == 0)
            {
                GameObject.Find(FighterList[0]).transform.Find("Selector").gameObject.SetActive(false);
                HeroesInBattle.RemoveAt(HeroesInBattle.FindIndex(i => i.name == FighterList[0]));
                FighterList.RemoveAt(0);
            }
            else
            {
                FighterList.Add(FighterList[0]);
                FighterList.RemoveAt(0);
            }

            GameObject.Find(FighterList[FighterList.Count - 1]).transform.Find("Selector").gameObject.SetActive(false);

            if (FighterList[0].IndexOf("V") == 0)
                NextStep();
        }

        ClearAndAddFoodToPanel();

        GameObject.Find(FighterList[0]).transform.Find("Selector").gameObject.SetActive(true);
        battleState = PerformAction.PERFORMACTION;

        FoodPanel.SetActive(false);
        OrderPanel.SetActive(false);
    }

    void SelectorPrepare()
    {
        for (int i = 1; i < FighterList.Count; i++)
            GameObject.Find(FighterList[i]).transform.Find("Selector").gameObject.SetActive(false);

        GameObject.Find(FighterList[0]).transform.Find("Selector").gameObject.SetActive(true);
    }

    void ActiveFoodListPanel()
    {
        FoodPanel.SetActive(true);
    }
    public void ActiveOrderPanel()
    {
        OrderPanel.SetActive(true);
    }

    void ClearAndAddFoodToPanel()
    {
        GameObject[] GOS = GameObject.FindGameObjectsWithTag("FoodInPanel");
        foreach (var go in GOS)
            Destroy(go);

        ShowFoodList.confectionerFoodList = GameObject.Find(FighterList[0]).GetComponent<ConfectionerList>();
        ShowFoodList.workDrawFood = true;
    }
}
