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
    [SerializeField]
    private int index;
    InventoryController inventoryController;

    void Start()
    {
        inventoryController = new InventoryController();
        animator = button.GetComponent<Animator>();

        if (index!=0)
            animator.Play("Button_Always", 0, 0f);
        else
            animator.Play("Button_Choice", 0, 0f);

        BookmarksInventory indexButton = panel.transform.GetChild(index).gameObject.GetComponent<Button>().GetComponent<BookmarksInventory>();
        ButtonsList.buttons.Add(indexButton);
    }

    public int GetIndex()
    {
        return index;
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
        foreach(var i in ButtonsList.buttons)
        {
            if (i.isChoice)
            {
                i.animator.Play("Button_In", 0, 0f);
                i.isChoice = false;
                inventoryController.ChoosePage(index, GameObject.Find("Inventory"));
                break;
            }
        }

        isChoice = true;
        animator.Play("Button_Choice", 0, 0f);
    }
}
