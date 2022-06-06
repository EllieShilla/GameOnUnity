using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    [SerializeField]
    Image _loadingBar;

    private void Start()
    {
        StartCoroutine(LoadNextLevel());
    }
    IEnumerator LoadNextLevel()
    {
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync("");

        while (!loadLevel.isDone)
        {
            _loadingBar.fillAmount=Mathf.Clamp01(loadLevel.progress/.9f);
            yield return null;
        }
    }
}
