using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCreateItemPanel : MonoBehaviour
{
    public GameObject CanvasPanel;
    bool ItemPanelVisible = false;
    public GameObject InteractivePanel;
    private bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            playerInRange = true;
            InteractivePanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            playerInRange = false;
            InteractivePanel.SetActive(false);
            ItemPanelVisible = false;
        }
    }

    private void Update()
    {
        if (playerInRange)
        {
            InteractivePanel.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "Create";

            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractivePanel.SetActive(false);
                ItemPanelVisible = true;
                CanvasPanel.SetActive(true);
                ShowItemToCreate.startCreate = true;
            }
        }


    }

    public void CloseCreateItemPanel()
    {
        CanvasPanel.SetActive(false);
    }

}

