using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowFoodList : MonoBehaviour
{
    public static ConfectionerList confectionerFoodList;
    public static bool workDrawFood = false;
    [SerializeField] GameObject panelList;
    private BattleStateMachine stateMachine;
    private bool isDish = false;
    [SerializeField] GameObject PanelMiniGameWithCook;

    FoodProcessing foodProcessing;

    private void Start()
    {
        foodProcessing = gameObject.AddComponent<FoodProcessing>(); ;
        foodProcessing.SetPanelMiniGameWithCook(PanelMiniGameWithCook);
        stateMachine = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
    }
    private void Update()
    {
        if (workDrawFood)
        {
            HeroStateMaschine heroState = stateMachine.FighterList[0].GetComponent<HeroStateMaschine>();

            if (confectionerFoodList != null)
                for (int i = 0; i < confectionerFoodList.confectionerFood.Count; i++)
                {
                    var food = confectionerFoodList.confectionerFood[i];

                    GameObject foodPanel = panelList;

                    GameObject foodName = foodPanel.transform.GetChild(2).gameObject;
                    foodName.GetComponent<Text>().text = food.typeOfFood+": "+food.skill.ToString();

                    GameObject aboutFood = foodPanel.transform.GetChild(1).gameObject;
                    aboutFood.GetComponent<Text>().text = food.foodName;

                    GameObject icon = foodPanel.transform.GetChild(0).gameObject;
                    icon.GetComponent<Image>().sprite = food.foodArt;

                    Button b = foodPanel.transform.GetChild(3).gameObject.GetComponent<Button>();
                    b.name = food.foodName;


                    var newFoodPlane = Instantiate(foodPanel, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    newFoodPlane.transform.parent = gameObject.transform;
                    newFoodPlane.tag = "FoodInPanel";


                    //поиск кнопки на каждом из блюд и создание onClick
                    Button btn = GameObject.Find(food.foodName).GetComponent<Button>();
                    btn.onClick.RemoveAllListeners();
                    btn.onClick.AddListener(delegate { StartCoroutine( foodProcessing.ChoiceFood(btn.name)); });

                    if (heroState.baseHeroero.currentStamina < 2)
                        btn.interactable = false;
                    else 
                        btn.interactable = true;
                }
            workDrawFood = false;
        }
    }

}
