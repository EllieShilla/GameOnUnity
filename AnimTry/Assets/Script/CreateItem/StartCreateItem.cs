using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCreateItem : MonoBehaviour
{
    public GameObject KeyOuter;
    public static bool isStartCreate = false;

    private void Update()
    {
        if (isStartCreate)
        {
            KeyOuter.SetActive(true);
        }

        if (!isStartCreate)
        {
            KeyOuter.SetActive(false);
        }
    }

}
