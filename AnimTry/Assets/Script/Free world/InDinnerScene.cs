using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InDinnerScene : MonoBehaviour
{
    [SerializeField]
    private Cafe cafe;
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private string IntoTitle;
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
            IntoPanel.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = textVariantLanguage.PanelToDinnerScene();

            if (Input.GetKeyDown(KeyCode.E))
            {
                IntoPanel.SetActive(false);
                playerInRange = false;

                SceneManager.LoadScene(IntoTitle);
            }
        }
    }
}


