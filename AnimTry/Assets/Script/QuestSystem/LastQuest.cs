using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class LastQuest : MonoBehaviour
{
    public void ShowPlaneLastQuest(GameObject EndPanel)
    {
        EndPanel.SetActive(true);
        GameObject player = GameObject.Find("MainCharacter");
        AddInventoryToObj inventory = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>();
        BinarySavingSystem.SavePlayer(inventory, player);

        Button button = EndPanel.transform.GetChild(2).GetComponent<Button>();
        button.onClick.AddListener(() => ClosePanel(EndPanel));
    }

    public void ClosePanel(GameObject EndPanel)
    {
        EndPanel.SetActive(false);
    }
}
