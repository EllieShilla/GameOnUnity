using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.SimpleLocalization;

public class Menu : MonoBehaviour
{
    public void OpenPlaneBeforeExit()
    {
        GameObject.Find("MENU").transform.GetChild(1).gameObject.SetActive(true);
    }
    public void ExitFromGame()
    {
        Application.Quit();
    }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name.Equals("MainMenuScene"))
        {
            GameObject menu = GameObject.Find("MENU").gameObject;

            if (!File.Exists(Application.persistentDataPath + "/data.save"))
            {
                menu.transform.GetChild(0).transform.GetChild(0).GetComponent<Button>().interactable = false;
            }
            else
            {
                menu.transform.GetChild(0).transform.GetChild(0).GetComponent<Button>().interactable = true;
            }
        }
    }

    private void Start()
    {
        if (FromScene.isContinue)
        {
            ContinueGame();
            FromScene.isContinue = false;
        }

        if (FromScene.isFirstOpen)
        {
            LoadGame();
            FromScene.isFirstOpen = false;
        }
    }
    void ContinueGame()
    {
        LoadCharacterOnScene loadCharacterOnScene = new LoadCharacterOnScene();
        loadCharacterOnScene.LoadInformation();
    }
    public void LoadGame()
    {
        FromScene.isContinue = true;
        PlayerData data = BinarySavingSystem.LoadPlayer();
        SceneManager.LoadScene(data.sceneName);
    }
    Text TextInfo;


    public void SaveGame()
    {
        GameObject player = GameObject.Find("MainCharacter");
        AddInventoryToObj inventory = GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>();
        BinarySavingSystem.SavePlayer(inventory, player);
        TextInfo = GameObject.Find("InfoMenu").GetComponent<Text>();
        TextInfo.text = "Data saved";
        StartCoroutine(MenuInfoClose());
    }

    public void SaveBeforeExit()
    {
        SaveGame();
        ExitFromGame();
    }

    IEnumerator MenuInfoClose()
    {
        yield return new WaitForSeconds(1f);
        TextInfo = GameObject.Find("InfoMenu").GetComponent<Text>();
        TextInfo.text = "";
    }

    public void ContinueFromPause()
    {
        //FromScene.isMenuActive = true;
        Move.ActiveMenu(false);
    }

    public void ClosePanel()
    {
        GameObject.Find("MENU").transform.GetChild(1).gameObject.SetActive(false);

    }

    public void NewGame()
    {
        string savePath = Application.persistentDataPath + "/data.save";
        File.Delete(savePath);
        LoadGame();
    }

    public void OpenKeybinds()
    {
        GameObject.Find("MENU").transform.GetChild(2).gameObject.SetActive(true);
    }

    public void BackFromKeybinds()
    {
        GameObject.Find("MENU").transform.GetChild(2).gameObject.SetActive(false);
    }

    public void OpenOptionsPanel()
    {
        GameObject.Find("MENU").transform.GetChild(2).gameObject.SetActive(true);
    }

    public void CloseOptionsPanel()
    {
        GameObject.Find("MENU").transform.GetChild(2).gameObject.SetActive(false);
    }

    public void OpenLocalizationPanel()
    {
        GameObject.Find("MENU").transform.GetChild(4).gameObject.SetActive(true);
    }

    public void CloseLocalizationPanel()
    {
        GameObject.Find("MENU").transform.GetChild(4).gameObject.SetActive(false);
    }

    public void OpenKeyPanel()
    {
        GameObject.Find("MENU").transform.GetChild(3).gameObject.SetActive(true);
    }

    public void CloseKeyPanel()
    {
        GameObject.Find("MENU").transform.GetChild(3).gameObject.SetActive(false);
    }

    public void OpenCreditsPanel()
    {
        GameObject.Find("MENU").transform.GetChild(5).gameObject.SetActive(true);
    }

    public void CloseCreditsPanel()
    {
        GameObject.Find("MENU").transform.GetChild(5).gameObject.SetActive(false);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void ChangeLanguage()
    {
        switch (LocalizationManager.Language)
        {
            case "English":
                {
                    LocalizationManager.Language = "Russian";

                }
                break;
            case "Russian":
                {
                    LocalizationManager.Language = "English";
                }
                break;
        }

        PlayerPrefs.SetString("Language", LocalizationManager.Language);
    }

    public static void ActiveMenuButton(GameObject menu)
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "MainMenuScene":
                {
                    if (!File.Exists(Application.persistentDataPath + "/data.save"))
                    {
                        menu.transform.GetChild(0).transform.GetChild(0).GetComponent<Button>().interactable = false;
                    }
                    else
                    {
                        menu.transform.GetChild(0).transform.GetChild(0).GetComponent<Button>().interactable = true;
                    }
                }
                break;
            case "FightScene":
                {
                    //кнопка сохранения игры не активна на сцене боя
                    Button button = menu.transform.GetChild(0).transform.GetChild(2).GetComponent<Button>();

                    if (SceneManager.GetActiveScene().name.Equals("FightScene"))
                        button.interactable = false;
                    else
                        button.interactable = true;
                }
                break;
            default:
                {
                    if (!File.Exists(Application.persistentDataPath + "/data.save"))
                    {
                        menu.transform.GetChild(0).transform.GetChild(1).GetComponent<Button>().interactable = false;
                    }
                    else
                    {
                        menu.transform.GetChild(0).transform.GetChild(1).GetComponent<Button>().interactable = true;
                    }
                }
                break;
        }
    }
}

