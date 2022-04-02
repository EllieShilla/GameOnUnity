using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BookmarksInventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    [SerializeField]
    private RectTransform button;
    public Animator animator;
    public bool isChoice = false;
    [SerializeField]
    private GameObject panel;
    private List<Button> buttons;
    [SerializeField]
    private int index;
    InventoryController inventoryController;

    void Start()
    {
        inventoryController = new InventoryController();
        animator = button.GetComponent<Animator>();

        if (!isChoice)
            animator.Play("Button_Always", 0, 0f);
        else
            animator.Play("Button_Choice", 0, 0f);

        buttons = new List<Button>();
        buttons.Add(panel.transform.GetChild(0).gameObject.GetComponent<Button>());
        buttons.Add(panel.transform.GetChild(1).gameObject.GetComponent<Button>());
        buttons.Add(panel.transform.GetChild(2).gameObject.GetComponent<Button>());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isChoice)
            animator.Play("Button_Out", 0, 0f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isChoice)
            animator.Play("Button_In", 0, 0f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        foreach(var i in buttons)
        {

            if (i.GetComponent<BookmarksInventory>().isChoice)
            {
                i.GetComponent<BookmarksInventory>().animator.Play("Button_In", 0, 0f);
                i.GetComponent<BookmarksInventory>().isChoice = false;
                inventoryController.ChoosePage(index, GameObject.Find("Inventory"));
                break;
            }
        }

        isChoice = true;
        animator.Play("Button_Choice", 0, 0f);
    }
}
