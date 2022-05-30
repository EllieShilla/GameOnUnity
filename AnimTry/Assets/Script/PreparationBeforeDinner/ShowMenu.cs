using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ShowMenu : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject panelForFood;

    public void ActivePanelMenu()
    {
        if (GameObject.Find("Menu"))
            menuPanel.SetActive(false);
        else
        {
            menuPanel.SetActive(true);
            Menu();
        }
    }

    //вывод меню кафе
    void Menu()
    {

        ClearTheField();
        TextVariantLanguageScriptObject textVariantLanguage = new TextVariantLanguageScriptObject();

        Cafe[] allCafe = (Cafe[])Resources.FindObjectsOfTypeAll(typeof(Cafe));
        Cafe cafe = allCafe.FirstOrDefault(item => item.CafeName.Equals(CafeForCooking.ChooseCafe.CafeName));


        foreach (var MenuItem in cafe.Menu)
        {
            GameObject foodPanel = panelForFood;

            GameObject foodName = foodPanel.transform.GetChild(1).gameObject;
            foodName.GetComponent<Text>().text = textVariantLanguage.FoodNameLocalization(MenuItem);

            GameObject aboutFood = foodPanel.transform.GetChild(2).gameObject;
            aboutFood.GetComponent<Text>().text = textVariantLanguage.FoodTypeLocalization(MenuItem) + MenuItem.skill.ToString();

            GameObject icon = foodPanel.transform.GetChild(0).gameObject;
            icon.GetComponent<Image>().sprite = MenuItem.foodArt;

            var newFoodPlane = Instantiate(foodPanel, new Vector3(menuPanel.transform.position.x, menuPanel.transform.position.y, menuPanel.transform.position.z), Quaternion.identity);
            newFoodPlane.transform.parent = menuPanel.gameObject.transform;
            newFoodPlane.tag = "MenuItem";
        }
    }

    void ClearTheField()
    {
        GameObject[] MenuItems = GameObject.FindGameObjectsWithTag("MenuItem");
        foreach (var item in MenuItems)
            Destroy(item);
    }
}
