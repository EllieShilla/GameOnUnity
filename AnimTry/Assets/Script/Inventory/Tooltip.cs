using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;

    Text toolText;
    RectTransform toolBackground;
    //public RectTransform rectTransform;

    private void Awake()
    {
        instance = this;

        toolBackground = transform.Find("BackGround").GetComponent<RectTransform>();
        toolText = transform.Find("Text").GetComponent<Text>();
        ShowTooltip("");
    }

    private void Start()
    {
        instance.HideTooltip();
    }

    private void Update()
    {
        //if (!GameObject.Find("Inventory") || !GameObject.Find("ChooseItemToCreate"))
        //{
        //    instance.gameObject.SetActive(false);
        //}

        Vector2 localPoint=Input.mousePosition;
        localPoint.y = localPoint.y - 25f;
        float pivotX = localPoint.x / Screen.width;
        float pivotY = localPoint.y / Screen.height;
        toolBackground.pivot = new Vector2(pivotY, pivotY);

        transform.position = localPoint;
    }
    void ShowTooltip(string TextThatNeedShow)
    {
        gameObject.SetActive(true);

        toolText.text = TextThatNeedShow;
        float textPaddingSize = 1f;
        Vector2 backgroundSize = new Vector2(toolText.preferredWidth + textPaddingSize * 1f, toolText.preferredHeight + textPaddingSize * 1f);
        toolBackground.sizeDelta = backgroundSize;
    }

    void HideTooltip()
    {
        gameObject.SetActive(false);
    }
    
    public static void ShowTooltip_Static(string TextThatNeedShow)
    {
        instance.ShowTooltip(TextThatNeedShow);
    }

    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }
}
