using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class IngridientListInventory : MonoBehaviour
{
    public List<Ingridient> ingridients;
    GameObject allIngridientsPanel;
    public GameObject panelForIngridient;
    public static bool isCreate;
    List<Ingridient> noDupesIngridient;

    void Update()
    {
        if (isCreate)
        {
            if (GameObject.FindGameObjectsWithTag("IngridientInInventoryPanel").Length > 0)
            {
                GameObject[] ItemInInventoryPanel = GameObject.FindGameObjectsWithTag("IngridientInInventoryPanel");
                foreach (GameObject item in ItemInInventoryPanel)
                {
                    Destroy(item);
                }
            }

            PrepareIngridientsPanel();
        }
    }

    public void PrepareIngridientsPanel()
    {

        ingridients = new List<Ingridient>();

        if (GameObject.Find("InventoryGameObject"))
            ingridients.AddRange(GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>().inventoryObj.ingridients);

        var OrderByIngridient = ingridients.OrderBy(p => p.Title);
        noDupesIngridient = OrderByIngridient.Distinct().ToList();

        allIngridientsPanel = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject;

        Show();

        isCreate = false;
    }

    void Show()
    {
        for (int i = 0; i < noDupesIngridient.Count; i++)
        {
            GameObject panel = panelForIngridient;
            Image img = panel.transform.GetChild(0).gameObject.GetComponent<Image>();
            img.sprite = noDupesIngridient[i].itemArt;
            img.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 80);

            Text text = panel.transform.GetChild(1).gameObject.GetComponent<Text>();
            text.text = noDupesIngridient[i].Title;

            int countIngridient = ingridients.FindAll(ing => ing.Title.Equals(noDupesIngridient[i].Title)).Count;
            Text textCount = panel.transform.GetChild(2).gameObject.GetComponent<Text>();

            if (countIngridient > 1)
                textCount.text = "x" + countIngridient;
            else
                textCount.text = "";

            var newItngridientPanel = Instantiate(panel, new Vector3(allIngridientsPanel.transform.position.x, allIngridientsPanel.transform.position.y, allIngridientsPanel.transform.position.z), Quaternion.identity);
            newItngridientPanel.transform.parent = allIngridientsPanel.transform;
            newItngridientPanel.tag = "IngridientInInventoryPanel";

            //Button btn = GameObject.FindGameObjectsWithTag("ItemInInventoryPanel").Where(obj => obj.name == ButtonName).Last().transform.GetChild(1).GetComponent<Button>();
            //btn.onClick.AddListener(delegate { Description(ButtonName); });
        }
    }
}
