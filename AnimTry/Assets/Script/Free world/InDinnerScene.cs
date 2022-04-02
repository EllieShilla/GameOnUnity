using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InDinnerScene : MonoBehaviour
{
    [SerializeField]
    private Cafe cafe;
    [SerializeField]
    private string sceneName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
            SceneManager.LoadScene(sceneName);
    }
}


