using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
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
        }
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
                DialogueManager.GetInstance().StartDialogue(inkJSON);

            if (Input.GetKeyDown(KeyCode.F))
                SceneManager.LoadScene("FightScene");
        }
        else
        {
            visualCue.SetActive(false);
        }
    }


}
