using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseShop : MonoBehaviour
{
    public GameObject ShopPanel;

    public void CloseShopPanel()
    {
        ShopPanel.SetActive(false);
    }
}
