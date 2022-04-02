using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    float min = 0;
    float max = 0;

    private GameObject Pointer;
    private GameObject Stop;
    private GameObject PlaneImg;
    float zPlus;
    float zMinus;

    RectTransform rectTransform;
   public static int count = 0;

     void UpdateStartInfo()
    {
        Pointer = GameObject.Find("pointer");
        Stop = GameObject.Find("stopPlace");
        PlaneImg = GameObject.Find("MiniGameWithCook");
        rectTransform = PlaneImg.GetComponent<RectTransform>();

        float stopX = Random.Range(PlaneImg.transform.position.x, PlaneImg.transform.position.x + rectTransform.rect.width) - Stop.GetComponent<RectTransform>().rect.width;
        Stop.transform.position = new Vector3(stopX, Stop.transform.position.y, Stop.transform.position.z);
        Pointer.transform.position = new Vector3(PlaneImg.transform.position.x, Pointer.transform.position.y, Pointer.transform.position.z);


        min = PlaneImg.transform.position.x;
        max = PlaneImg.transform.position.x + rectTransform.rect.width - Pointer.GetComponent<RectTransform>().rect.width;
    }

    bool minSize = true;
    public static float fast = 1f;
    public static bool stopCook = false;

    public void SetFast(float Fast)
    {
        fast = Fast;
    }

    public void Update()
    {
        if (count == 0)
        {
            count++;
            UpdateStartInfo();
        }
        if (!stopCook)
            CookMiniGame();
    }
    public static bool goodCook = false;
    public static bool onStop = false;
    public bool CookMiniGame()
    {
        zPlus = Stop.transform.position.x + Stop.GetComponent<RectTransform>().rect.width;
        zMinus = Stop.transform.position.x;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onStop)
            {
                goodCook = true;
                onStop = false;
            }
            else
                goodCook = false;


            stopCook = true;
        }

        if (!stopCook)
        {

            if (zMinus <= Pointer.transform.position.x && zPlus >= Pointer.transform.position.x)
            {
                onStop = true;
            }
            else
                onStop = false;



            if (minSize)
            {
                Pointer.transform.position = new Vector3(Pointer.transform.position.x + fast, Pointer.transform.position.y, Pointer.transform.position.z);

                if (Pointer.transform.position.x >= max)
                {
                    minSize = false;
                }
            }

            if (!minSize)
            {
                Pointer.transform.position = new Vector3(Pointer.transform.position.x - fast, Pointer.transform.position.y, Pointer.transform.position.z);

                if (Pointer.transform.position.x <= min)
                {
                    minSize = true;
                }
            }
        }
        return goodCook;

    }

}


