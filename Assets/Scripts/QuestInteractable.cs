using UnityEngine;

public class QuestInteractable : MonoBehaviour
{
    public string title;
    // public string description;
    public bool isCompleted;

    public QuestInteractable(string title)
    {
        this.title = title;
        // this.description = description;
        this.isCompleted = false;
    }

    public void CompleteQuest()
    {
        isCompleted = true;
        bool temp = QuestManager.Instance.allDailyQuests[QuestManager.Instance.currentQuestIndex].IsAllCompleted();

    }

}
