using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.SimpleLocalization;

public class TextVariantLanguageInteractivePanel
{
    public string DialogTrigerPanel()
    {
        string phrace = "";

        if(LocalizationManager.Language.Equals("English"))
         phrace = "Talk";
        else
            phrace = "��������";

        return phrace;
    }

    public string PanelToDinnerScene()
    {
        string phrace = "";

        if (LocalizationManager.Language.Equals("English"))
            phrace = "Entry";
        else
            phrace = "�����";

        return phrace;
    }


    public string PanelToSampleScene()
    {
        string phrace = "";

        if (LocalizationManager.Language.Equals("English"))
            phrace = "Exit";
        else
            phrace = "�����";

        return phrace;
    }

    public string PanelLoot()
    {
        string phrace = "";

        if (LocalizationManager.Language.Equals("English"))
            phrace = "Loot";
        else
            phrace = "�����";

        return phrace;
    }

    public string PanelCreate()
    {
        string phrace = "";

        if (LocalizationManager.Language.Equals("English"))
            phrace = "Create";
        else
            phrace = "�������";

        return phrace;
    }
}
