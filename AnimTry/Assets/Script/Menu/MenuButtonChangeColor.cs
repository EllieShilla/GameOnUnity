using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonChangeColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Button>().GetComponent<Image>().color = new Color(0.070f, 0.280f, 0.519f, 1.000f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Button>().GetComponent<Image>().color = new Color(0.000f, 0.141f, 0.302f, 1.000f);
    }
}
