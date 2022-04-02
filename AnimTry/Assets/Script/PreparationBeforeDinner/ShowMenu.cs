using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        Menu();
    }


    //вывод меню кафе
    void Menu()
    {
        Cafe[] allCafe = (Cafe[])Resources.FindObjectsOfTypeAll(typeof(Cafe));

        foreach (var cafe in allCafe)
        {
            if (cafe.CafeName.Equals(CafeForCooking.CafeTitle))
                foreach (var MenuItem in cafe.Menu)
                {
                    GameObject foodPanel = panelForFood;

                    GameObject foodName = foodPanel.transform.GetChild(2).gameObject;
                    foodName.GetComponent<Text>().text = MenuItem.typeOfFood + ": " + MenuItem.skill.ToString();

                    GameObject aboutFood = foodPanel.transform.GetChild(1).gameObject;
                    aboutFood.GetComponent<Text>().text = MenuItem.foodName;

                    GameObject icon = foodPanel.transform.GetChild(0).gameObject;
                    icon.GetComponent<Image>().sprite = MenuItem.foodArt;

                    var newFoodPlane = Instantiate(foodPanel, new Vector3(menuPanel.transform.position.x, menuPanel.transform.position.y, menuPanel.transform.position.z), Quaternion.identity);
                    newFoodPlane.transform.parent = menuPanel.gameObject.transform;
                }
        }
    }
}
