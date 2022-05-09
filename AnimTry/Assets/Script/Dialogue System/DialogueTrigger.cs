using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField]
    GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField]
    TextAsset inkJSON;

    bool playerInRange;

    public GameObject ShopPanel;
    public Cafe cafe;

    [SerializeField]
    private GameObject IntoPanel;
    private Text PanelText;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        PanelText = IntoPanel.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            playerInRange = true;
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
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);

            PanelText.text = "Talk";
            IntoPanel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                IntoPanel.SetActive(false);
                DialogueManager.GetInstance().StartDialogue(inkJSON, ShopPanel, cafe);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }


}
