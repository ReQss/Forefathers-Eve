using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
// [System.Serializable]
// public class Quest
// {
//     public string title;
//     // public string description;
//     public bool isCompleted;

//     public Quest(string title)
//     {
//         this.title = title;
//         // this.description = description;
//         this.isCompleted = false;
//     }

//     public void CompleteQuest()
//     {
//         isCompleted = true;
//         bool temp = QuestManager.Instance.allDailyQuests[QuestManager.Instance.currentQuestIndex].IsAllCompleted();

//     }
// }
[System.Serializable]
public class DailyQuests{
    public QuestInteractable[] quests;
    public bool IsAllCompleted()
    {
        foreach (QuestInteractable quest in quests)
        {
            if (!quest.isCompleted)
                return false;
        }
        return true;
    }
    public bool allQuestCompleted = false;
}
public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
 
    public List<DailyQuests> allDailyQuests = new List<DailyQuests>();
    public int currentQuestIndex = 0;
    public DailyQuests currentDailyQuests;
    public List<GameObject> questUIElements;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        currentDailyQuests = allDailyQuests[0];
        UpdateDailyQuestsUI();
    }
    public void NextQuest()
    {
        if (currentQuestIndex + 1 < allDailyQuests.Count)
        {
            currentQuestIndex++;
            currentDailyQuests = allDailyQuests[currentQuestIndex];
            UpdateDailyQuestsUI();
        }
    }
    public void UpdateDailyQuestsUI()
    {
        foreach(GameObject uiElement in questUIElements)
        {
            uiElement.SetActive(false);
        }
        for(int i=0;i<questUIElements.Count && i < currentDailyQuests.quests.Length; i++)
        {
            GameObject currentUIElement = questUIElements[i];
            currentUIElement.SetActive(true);
            currentUIElement.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentDailyQuests.quests[i].title;
        }
    }

}
