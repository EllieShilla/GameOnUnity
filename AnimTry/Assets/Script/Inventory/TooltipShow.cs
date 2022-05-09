using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipShow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.ShowTooltip_Static(gameObject.name.Split('_')[1]);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.HideTooltip_Static();
    }
}
