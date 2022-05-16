using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InSampleScene : MonoBehaviour
{
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Character")
    //    {
    //        SaveScript ReturnFromScene = this.gameObject.GetComponent<SaveScript>();
    //        ReturnFromScene.SceneLoad();
    //    }
    //}

    [SerializeField]
    private GameObject IntoPanel;
    private bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            playerInRange = true;
            IntoPanel.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            playerInRange = true;
            IntoPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            playerInRange = false;
            IntoPanel.SetActive(false);
        }
    }


    private void Update()
    {
        if (playerInRange)
        {
            IntoPanel.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "Exit";

            if (Input.GetKeyDown(KeyCode.E))
            {
                IntoPanel.SetActive(false);
                playerInRange = false;

                GameObject player = GameObject.Find("MainCharacter");
                AddInventoryToObj inventory = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>();
                BinarySavingSystem.SavePlayerBook(inventory, player);

                SaveScript ReturnFromScene = this.gameObject.GetComponent<SaveScript>();
                ReturnFromScene.SceneLoad();
            }
        }
    }
}
