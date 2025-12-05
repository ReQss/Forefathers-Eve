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
    
    private GameObject questLogPanel; // Cache referencji do panelu QuestLog

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
        
        // Znajdź i zapamiętaj referencję do panelu QuestLog
        if (questLogPanel == null)
        {
            questLogPanel = GameObject.Find("QuestLog");
        }
        
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
        if (questUIElements == null || questUIElements.Count == 0)
        {
            Debug.LogWarning("questUIElements lista jest pusta lub null!");
            return;
        }

        if (currentDailyQuests == null || currentDailyQuests.quests == null)
        {
            Debug.LogWarning("currentDailyQuests lub quests są null!");
            return;
        }

        // Najpierw ukryj wszystkie elementy
        foreach(GameObject uiElement in questUIElements)
        {
            if (uiElement != null)
            {
                uiElement.SetActive(false);
            }
        }

        // Potem pokaż tylko te, które mają odpowiadające questy
        for(int i=0; i < questUIElements.Count && i < currentDailyQuests.quests.Length; i++)
        {
            if (questUIElements[i] != null && currentDailyQuests.quests[i] != null)
            {
                GameObject currentUIElement = questUIElements[i];
                currentUIElement.SetActive(true);
                
                // Upewnij się, że element ma dziecko z TextMeshProUGUI
                if (currentUIElement.transform.childCount > 0)
                {
                    TextMeshProUGUI textComponent = currentUIElement.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                    if (textComponent != null)
                    {
                        textComponent.text = currentDailyQuests.quests[i].title;
                    }
                }
            }
        }
    }

    // Ukryj quest log (wywołane podczas dialogu)
    public void HideQuestLog()
    {
        // Upewnij się, że mamy referencję do panelu
        if (questLogPanel == null)
        {
            questLogPanel = GameObject.Find("QuestLog");
        }

        // Ukryj główny panel QuestLog jeśli istnieje (ukrywa również wszystkie dzieci)
        if (questLogPanel != null)
        {
            questLogPanel.SetActive(false);
        }
    }

    // Pokaż quest log (wywołane po zakończeniu dialogu)
    public void ShowQuestLog()
    {
        // Upewnij się, że mamy referencję do panelu
        if (questLogPanel == null)
        {
            questLogPanel = GameObject.Find("QuestLog");
        }

        // NAJPIERW pokaż główny panel QuestLog (musi być aktywny żeby dzieci mogły być aktywne)
        if (questLogPanel != null)
        {
            questLogPanel.SetActive(true);
            
            // Wymuś odświeżenie UI Canvas, żeby upewnić się że panel jest widoczny
            Canvas.ForceUpdateCanvases();
        }
        else
        {
            Debug.LogWarning("QuestLog panel nie został znaleziony!");
        }

        // Upewnij się, że currentDailyQuests nie jest null
        if (currentDailyQuests == null && allDailyQuests != null && allDailyQuests.Count > 0)
        {
            if (currentQuestIndex >= 0 && currentQuestIndex < allDailyQuests.Count)
            {
                currentDailyQuests = allDailyQuests[currentQuestIndex];
            }
            else if (allDailyQuests.Count > 0)
            {
                currentDailyQuests = allDailyQuests[0];
                currentQuestIndex = 0;
            }
        }

        // Potem odśwież i pokaż quest UI (UpdateDailyQuestsUI pokaże odpowiednie elementy)
        if (currentDailyQuests != null)
        {
            UpdateDailyQuestsUI();
        }
        else
        {
            Debug.LogWarning("Nie można pokazać quest logu - currentDailyQuests jest null!");
        }
    }

}
