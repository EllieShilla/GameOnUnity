using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHeroStat : MonoBehaviour
{
    [SerializeField] BaseHero HeroStat;
    [SerializeField] RectTransform HeroStatPanel;
    [SerializeField] GameObject HeroStatPanelPrefab;


    void Start()
    {
        //������������ ������� ��� ��� ������ �� �������� ���������� �� ������
        StartCoroutine(FoodPanel());
    }


    IEnumerator FoodPanel()
    {
        yield return new WaitForSeconds(0.5f);
        DrawFood();
    }
    //����� ���� ���� �� ������
    void DrawFood()
    {
        TextVariantLanguageScriptObject textVariantLanguage = new TextVariantLanguageScriptObject();

        GameObject panel = HeroStatPanelPrefab;
        GameObject HeroName = panel.transform.GetChild(1).gameObject;
        //HeroName.GetComponent<Text>().text = HeroStat.heroName;
        HeroName.GetComponent<Text>().text = textVariantLanguage.HeroNameLocalization(HeroStat);
    }

    private BattleStateMachine stateMachine;
    private bool isDish = false;
}
