using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHeroStat : MonoBehaviour
{
    [SerializeField] BaseHero HeroStat;
    [SerializeField] RectTransform HeroStatPanel;
    [SerializeField] GameObject HeroStatPanelPrefab;


    void Start()
    {
        //используется карутин так как данные не успевают загрузится на старте
        StartCoroutine(FoodPanel());
    }


    IEnumerator FoodPanel()
    {
        yield return new WaitForSeconds(0.5f);
        DrawFood();
    }
    //вывод всех блюд на панель
    void DrawFood()
    {
        

        GameObject panel = HeroStatPanelPrefab;
        GameObject HeroName = panel.transform.GetChild(1).gameObject;
        HeroName.GetComponent<Text>().text = HeroStat.heroName;

        //GameObject aboutFood = foodPanel.transform.GetChild(1).gameObject;
        //aboutFood.GetComponent<Text>().text = food.foodName;

        //GameObject icon = foodPanel.transform.GetChild(0).gameObject;
        //icon.GetComponent<Image>().sprite = food.foodArt;

        //Button b = foodPanel.transform.GetChild(3).gameObject.GetComponent<Button>();
        //b.name = food.foodName;

        //var newFoodPlane = Instantiate(foodPanel, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        //newFoodPlane.transform.parent = gameObject.transform;


        ////поиск кнопки на каждом из блюд и создание onClick
        //Button btn = GameObject.Find(food.foodName).GetComponent<Button>();
        //btn.onClick.AddListener(delegate { ChoiceFood(btn.name); });
    }

    private BattleStateMachine stateMachine;
    private bool isDish = false;

    //выбор блюда для подачи
    //public void ChoiceFood(string foodName)
    //{
    //    if (GameObject.Find(ClickForSearchInfo.nameVisitor) != null)
    //    {

    //        stateMachine = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();

    //        foreach (var item in stateMachine.foodOrders)
    //        {
    //            if (item.visitor.Equals(ClickForSearchInfo.nameVisitor))
    //            {
    //                //если в заказе находится выбраное игроком блюдо, оно удалится
    //                foreach (var i in item.foodList)
    //                    if (i.foodName.Equals(foodName))
    //                    {
    //                        item.foodList.Remove(i);
    //                        isDish = true;
    //                        break;
    //                    }


    //            }

    //        }
    //        if (!isDish)
    //            Debug.Log("FFF");
    //        else
    //        {

    //            isDish = false;
    //        }

    //    }

    //}
}
