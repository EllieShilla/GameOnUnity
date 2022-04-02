using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStateMachine : MonoBehaviour
{
    public BaseEnemy baseEnemy;
    private BattleStateMachine stateMachine;
    private List<Food> order = new List<Food>();
    [SerializeField]
    private GameObject Selector;



    public enum TurnState
    {
        PROCESSING,
        CHOOSEACTION,
        WAITING,
        ACTION,
        DEAD
    }

    public TurnState currentState;
    private float curCooldown = 0;
    private float maxCooldown = 1f;
    private Vector3 startPosition;
    private bool actionStarted = false;
    public GameObject HeroToAttack;
    private float animSpeed = 5f;

    private int CoundOfFoodInOrder;
    private List<Food> newOrder;

    void Start()
    {
        Selector.SetActive(false);

        currentState = TurnState.PROCESSING;
        stateMachine = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        startPosition = transform.position;

        foreach (Food item in Resources.FindObjectsOfTypeAll<Food>())
        {
            foreach (string i in item.listOfEstablishments)
                if (i.Equals(CafeForCooking.CafeTitle))
                    order.Add(item);
        }


        CoundOfFoodInOrder = Random.Range(1, order.Count);
        newOrder = new List<Food>();

        for (int i = 0; i < CoundOfFoodInOrder; i++)
            newOrder.Add(order[Random.Range(0, order.Count)]);

        //foreach (var i in newOrder)
        //    Debug.Log(i.foodName);

                   

    }

    public static bool nextOrder = false;
    void Update()
    {
        switch (currentState)
        {
            case (TurnState.PROCESSING):
                FoodOrder();
                UpgraidProgressBar();
                break;
            case (TurnState.CHOOSEACTION):
                ChooseAction();
                currentState = TurnState.WAITING;
                break;
            case (TurnState.WAITING):

                break;
            case (TurnState.ACTION):
                StartCoroutine(TimeForAction());
                break;
            case (TurnState.DEAD):

                break;
        }


        if (nextOrder)
        { 
            AddNewVisitor(); 
        }


        }

    void UpgraidProgressBar()
    {
        curCooldown = curCooldown + Time.deltaTime;

        //////////////////////////////////?
        if (curCooldown >= maxCooldown)
        {
            currentState = TurnState.CHOOSEACTION;
        }
    }

    public void ChooseAction()
    {
        if (stateMachine.HeroesInBattle.Count > 0)
        {
            NewBehaviourScript myAttack = new NewBehaviourScript();
            myAttack.fighter = baseEnemy.enemyName;
            myAttack.Type = "BaseVisitor";
            myAttack.FighterObj = this.gameObject;
            //myAttack.FighterTarget = stateMachine.HeroesInBattle[stateMachine.HeroesInBattle.FindIndex(item=>item.name== stateMachine.FighterList[0])];
            myAttack.FighterTarget = stateMachine.HeroesInBattle[Random.Range(0, stateMachine.HeroesInBattle.Count)];
            myAttack.foodListCount = CoundOfFoodInOrder;
            myAttack.foodList = newOrder;
            stateMachine.CollectActions(myAttack);
        }

    }

    //вывод заказов отдельно
    void FoodOrder()
    {
            if (stateMachine.foodOrders.Count < stateMachine.EnemysInBattle.Count)
            {
                FoodOrder foodOrder = new FoodOrder();
                foodOrder.foodList = newOrder;
                foodOrder.visitor = baseEnemy.enemyName;
                stateMachine.Order(foodOrder);
            }
    }


    //движения противника к героям: начало
    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        Vector3 heroPosition = new Vector3(HeroToAttack.transform.position.x + 0.7f, HeroToAttack.transform.position.y, HeroToAttack.transform.position.z);

        PressureChange();


        while (MoveTowardsEnemy(heroPosition))
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        Vector3 firstPosition = startPosition;
        while (MoveTowardsStart(firstPosition))
        {
            yield return null;
        }

        //stateMachine.performList.RemoveAt(0);
        if(stateMachine.performList.FindIndex(item => item.fighter.Equals(ClickForSearchInfo.nameVisitor))!=-1)
        stateMachine.performList.RemoveAt(stateMachine.performList.FindIndex(item => item.fighter.Equals(ClickForSearchInfo.nameVisitor)));
        stateMachine.battleState = BattleStateMachine.PerformAction.WAIT;

        actionStarted = false;

        curCooldown = 0f;
        currentState = TurnState.PROCESSING;
    }

    private bool MoveTowardsEnemy(Vector3 targer)
    {
        return targer != (transform.position = Vector3.MoveTowards(transform.position, targer, animSpeed * Time.deltaTime));
    }

    private bool MoveTowardsStart(Vector3 targer)
    {
        return targer != (transform.position = Vector3.MoveTowards(transform.position, targer, animSpeed * Time.deltaTime));
    }

    //движения противника к героям: конец


    void PressureChange()
    {
        HeroStateMaschine heroState = GameObject.Find(stateMachine.FighterList[stateMachine.FighterList.Count - 1]).GetComponent<HeroStateMaschine>();

        //HeroStateMaschine heroState = HeroToAttack.GetComponent<HeroStateMaschine>();
        if (heroState.baseHeroero.currentPressure < heroState.baseHeroero.Pressure)
        {
            heroState.baseHeroero.currentPressure += 2;
            heroState.PreassureBar.rectTransform.localScale = new Vector2(heroState.baseHeroero.Pressure * (heroState.baseHeroero.currentPressure / 100), heroState.PreassureBar.rectTransform.localScale.y);
        }
        Debug.Log("preasure " + heroState.baseHeroero.currentPressure);

    }

    public void AddNewVisitor()
    {
        

        BattleStateMachine.newVisitor = true;


        nextOrder = false;

    }

    public void NewVisitor()
    {

            Selector.SetActive(false);

            currentState = TurnState.PROCESSING;
            stateMachine = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
            startPosition = transform.position;


            CoundOfFoodInOrder = Random.Range(1, order.Count);
            newOrder = new List<Food>();

            for (int i = 0; i < CoundOfFoodInOrder; i++)
                newOrder.Add(order[Random.Range(0, order.Count)]);
    }
}
