using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InSampleScene : MonoBehaviour
{
    [SerializeField]
    private Vector3 returnPosition;

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
            TextVariantLanguageInteractivePanel textVariantLanguage = new TextVariantLanguageInteractivePanel();
            IntoPanel.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = textVariantLanguage.PanelToSampleScene();

            if (Input.GetKeyDown(KeyCode.E))
            {
                IntoPanel.SetActive(false);
                playerInRange = false;
                FromScene.loadPosition = returnPosition;
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
