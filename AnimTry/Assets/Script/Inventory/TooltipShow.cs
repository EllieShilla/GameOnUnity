using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class TooltipShow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{    public void OnPointerEnter(PointerEventData eventData)
    {
        TextVariantLanguageScriptObject textVariantLanguage=new TextVariantLanguageScriptObject();
        Ingridient ing = Resources.LoadAll<Ingridient>("ScriptObj/Ingridients").FirstOrDefault(i => i.TitleEng.Equals(gameObject.name.Split('_')[1]));
        string title=textVariantLanguage.IngridientTitleLocalization(Resources.LoadAll<Ingridient>("ScriptObj/Ingridients").FirstOrDefault(i => i.TitleEng.Equals(gameObject.name.Split('_')[1])));
        Tooltip.ShowTooltip_Static(title);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.HideTooltip_Static();
    }
}
