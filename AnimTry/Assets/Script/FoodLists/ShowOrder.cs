using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowOrder : MonoBehaviour
{

    //[SerializeField] RectTransform orderPanel;
    [SerializeField] GameObject panelList;
    public static List<string> foodName;
    public static bool isUpdate = false;



    private void Update()
    {
        if (isUpdate)
        {
            DrawOrder();
            isUpdate = false;
        }
    }
    void DrawOrder()
    {

        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (string food in foodName)
        {
            GameObject foodPanel = panelList;

            GameObject aboutFood = foodPanel.transform.GetChild(0).gameObject;
            aboutFood.GetComponent<Text>().text = food;
            var newFoodPlane = Instantiate(foodPanel, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            newFoodPlane.transform.parent = gameObject.transform;
        }
    }
}



