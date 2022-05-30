using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestListInventory : MonoBehaviour
{
    GameObject QuestsPanel;
    GameObject detailInformAboutQuest;
    [SerializeField]
    GameObject questDetailPanel;
    public static bool isCreate = false;
    List<Quest> quests;

    private void Update()
    {

        if (isCreate)
        {

            quests = new List<Quest>();

            if (GameObject.Find("InventoryGameObject"))
                quests.AddRange(GameObject.Find("InventoryGameObject").GetComponent<AddInventoryToObj>().inventoryObj.quests);

            QuestsPanel = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject;
            detailInformAboutQuest = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject;

            ClearQuestsPanel();
            ShowQuests();

            if(quests.Count>0)
            QuestDetail(quests[0]);

            isCreate = false;
        }
    }

    void ClearQuestsPanel()
    {
        if (GameObject.FindGameObjectsWithTag("QuestListInInventory").Length > 0)
        {
            GameObject[] questsScroll = GameObject.FindGameObjectsWithTag("QuestListInInventory");
            foreach (GameObject item in questsScroll)
            {
                Destroy(item);
            }
        }
    }

    private void ShowQuests()
    {
        TextVariantLanguageScriptObject textVariantLanguage = new TextVariantLanguageScriptObject();

        for (int i = 0; i < quests.Count; i++)
        {
            GameObject panel = questDetailPanel;

            Text title = panel.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>();
            title.text = textVariantLanguage.QuestTitleLocalization(quests[i]);

            GameObject panelForCkeck = panel.transform.GetChild(0).transform.GetChild(1).gameObject;
            panelForCkeck.SetActive(true);

            GameObject checkPanel = panel.transform.GetChild(0).transform.GetChild(2).gameObject;
            if(quests[i].isCompleted)
                checkPanel.SetActive(true);
            else 
                checkPanel.SetActive(false);

            var newquestDetailPanel = Instantiate(panel, new Vector3(QuestsPanel.transform.position.x, QuestsPanel.transform.position.y, QuestsPanel.transform.position.z), Quaternion.identity);
            newquestDetailPanel.transform.parent = QuestsPanel.transform;
            newquestDetailPanel.tag = "QuestListInInventory";

            Quest quest = quests[i];
            Button btn = newquestDetailPanel.GetComponent<Button>();
            btn.onClick.AddListener(() => QuestDetail(quest));
        }
    }

    private void QuestDetail(Quest quest)
    {
        TextVariantLanguageScriptObject textVariantLanguage = new TextVariantLanguageScriptObject();

        detailInformAboutQuest.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>().text = textVariantLanguage.QuestTitleLocalization(quest);
        detailInformAboutQuest.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = quest.questGiver.characterPhoto;
        detailInformAboutQuest.transform.GetChild(2).gameObject.GetComponent<Text>().text = textVariantLanguage.QuestDescriptionLocalization(quest);
    }
}
