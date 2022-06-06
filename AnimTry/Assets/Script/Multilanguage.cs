using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multilanguage : MonoBehaviour
{
    private void Awake()
    {
        LocalizationManager.Read();
        if (PlayerPrefs.HasKey("Language"))
            LocalizationManager.Language = PlayerPrefs.GetString("Language");
        else
            LocalizationManager.Language = "English";
    }
}
