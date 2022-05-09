using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStateMaschine : MonoBehaviour
{
    public BaseHero baseHeroero;

    public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }
    private BattleStateMachine stateMachine;

    public TurnState currentState;
    private float curCooldown = 0;
    private float maxCooldown = 5f;
    public Image ProgressBar;
    public Image PreassureBar;
    public Image StaminaBar;
    public Image ReturnBar;
    public GameObject CharacterInformPanel; 


    [SerializeField]
    private GameObject Selector;
    public Character character;



    public GameObject HeroToMove;

    void Start()
    {
        //if (StartCook.coldShopCook)
        //    baseHeroero = StartCook.coldShopCook.baseHero;

        curCooldown = Random.Range(0, 2.5f);
        Selector.SetActive(false);
        stateMachine = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        currentState = TurnState.PROCESSING;

    }

    void Update()
    {
        switch (currentState)
        {
            case (TurnState.PROCESSING):
                UpgraidProgressBar();
                break;
            case (TurnState.ADDTOLIST):
                stateMachine.HeroesToManage.Add(this.gameObject);
                NewCook();
                currentState = TurnState.WAITING;
                break;
            case (TurnState.WAITING):

                break;
            case (TurnState.ACTION):

                break;
            case (TurnState.DEAD):

                break;
        }
    }

    void UpgraidProgressBar()
    {
        //curCooldown = curCooldown + Time.deltaTime;
        //float calcCooldown = curCooldown / maxCooldown;
        //ProgressBar.transform.localScale = new Vector3(Mathf.Clamp(calcCooldown, 0, 1), ProgressBar.transform.localScale.y, ProgressBar.transform.localScale.z);


        //if (curCooldown >= maxCooldown)
        //{
        currentState = TurnState.ADDTOLIST;
        //}
    }

    void NewCook()
    {
        CharacterObjInf cook = new CharacterObjInf();
        cook.CharacterName = this.gameObject.name;
        cook.CharacterObj = this.gameObject;
        cook.Type = "Hero";
        stateMachine.AddCooker(cook);
    }
}
