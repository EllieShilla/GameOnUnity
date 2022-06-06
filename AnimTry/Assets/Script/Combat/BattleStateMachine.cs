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
    public GameObject BattleResultPanel;

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

    [SerializeField]
    private GameObject FirstCamera;
    [SerializeField]
    private GameObject SecondCamera;


    Image backgroundColor;

    void MyStart()
    {
        OrderPanel = GameObject.Find("OrderPanel");
        OrderPanel.SetActive(false);
        nextButton = GameObject.Find("NextStep").GetComponent<Button>();
        cookButton = GameObject.Find("Cook").GetComponent<Button>();
        nextButton.onClick.AddListener(delegate { NextStep(); });
        cookButton.onClick.AddListener(delegate { ActiveFoodListPanel(); });

        battleState = PerformAction.WAIT;
        EnemysInBattle.AddRange(GameObject.Find("VisitorList").GetComponent<VisitorList>().visitors);
        HeroesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Character"));

        heroInput = HeroGUI.ACTIVATE;
        FoodPanel.SetActive(false);
        CreateFighterList();
        SelectorPrepare();

        //смена фона панели информации на желтый(означает, что персонаж сейчас ходит)
        backgroundColor = FighterList[0].GetComponent<HeroStateMaschine>().CharacterInformPanel.GetComponent<Image>();
        backgroundColor.color = new Color32(255, 205, 6, 255);

        ShowFoodList.confectionerFoodList = FighterList[0].GetComponent<ConfectionerList>();
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
        performList.Insert(0, input);
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

    public List<GameObject> FighterList = new List<GameObject>();

    void CreateFighterList()
    {
        foreach (var i in HeroesInBattle)
            FighterList.Add(i);
    }

    public void NextStep()
    {
        if (!BattleResultShow.EndBattle)
        {
            if (FighterList[0].tag.Equals("Enemy"))
            {
                OrderPanel.SetActive(false);

                int indexVisitor = performList.FindIndex(item => item.fighter.Equals(ClickForSearchInfo.nameVisitor));

                if (performList.Count == 1)
                    indexVisitor = 0;

                //выбор героя который подавал блюдо
                for (int i = 0; i < performList.Count; i++)
                {
                    performList[i].FighterTarget = HeroesInBattle.Find(i => i.name == FighterList[FighterList.Count - 1].name);
                }


                GameObject performer = GameObject.Find(performList[indexVisitor].fighter);
                if (performList[indexVisitor].Type.Equals("BaseVisitor"))
                {
                    EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
                    ESM.HeroToAttack = performList[indexVisitor].FighterTarget;
                    ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                }


                //FighterList[0].transform.Find("Selector").gameObject.SetActive(false);
                FighterList.RemoveAt(0);
            }
            else
            {
                HeroStateMaschine heroState = FighterList[0].GetComponent<HeroStateMaschine>();

                //смена фона панели информации на обычный
                backgroundColor = FighterList[0].GetComponent<HeroStateMaschine>().CharacterInformPanel.GetComponent<Image>();
                backgroundColor.color = new Color32(255, 255, 255, 100);

                FighterList.Add(FighterList[0]);
                FighterList.RemoveAt(0);

                StartCoroutine(changeCamera());

                //FighterList[FighterList.Count - 1].transform.Find("Selector").gameObject.SetActive(false);

                if (FighterList[0].tag.Equals("Enemy"))
                    NextStep();
            }

            ClearAndAddFoodToPanel();

            //FighterList[0].transform.Find("Selector").gameObject.SetActive(true);
            battleState = PerformAction.PERFORMACTION;

            FoodPanel.SetActive(false);
            OrderPanel.SetActive(false);
        }
    }

    IEnumerator changeCamera()
    {
        yield return new WaitForSeconds(1.5f);

        if (FighterList.Count > 0)
        {
            if (FighterList[0].transform.position.z <= -3)
            {
                FirstCamera.SetActive(true);
                SecondCamera.SetActive(false);
            }
            else
            {
                FirstCamera.SetActive(false);
                SecondCamera.SetActive(true);
            }

            //смена фона панели информации на желтый(означает, что персонаж сейчас ходит)
            backgroundColor = FighterList[0].GetComponent<HeroStateMaschine>().CharacterInformPanel.GetComponent<Image>();
            backgroundColor.color = new Color32(255, 205, 6, 255);
        }
        else
        {
            BattleResultShow.GoodEnd = false;
            BattleResultShow.EndBattle = true;
            StartCoroutine(EndFight());
        }

    }

    void SelectorPrepare()
    {
        //for (int i = 1; i < FighterList.Count; i++)
        //    FighterList[i].transform.Find("Selector").gameObject.SetActive(false);

        //FighterList[0].transform.Find("Selector").gameObject.SetActive(true);
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

        ShowFoodList.confectionerFoodList = FighterList[0].GetComponent<ConfectionerList>();
        ShowFoodList.workDrawFood = true;
    }

    public IEnumerator EndFight()
    {
        yield return new WaitForSeconds(5f);
        BattleResultPanel.SetActive(true);
    }
}
