using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class SceneDialogues
{
    public List<DialogueData> dialogues;
    public int currentDialogueIndex = 0;
}
public class QuestVariables
{
    public int collectedWood = 0;
    public int collectedStone = 0;
    public int cleanedGrave = 0;
    public bool prayedAtChapel = false;
    public bool isEverythingAchieved = false;
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int health;
    public QuestVariables questVariables = new QuestVariables();
    public SceneDialogues saveZoneDialogues;
    public SceneDialogues ritualZoneDialogues;
    public int ghostGoodChoices = 0;
    public int ghostBadChoices = 0;
    public int cityGoodChoices = 0;
    public int cityBadChoices = 0;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void CheckQuests()
    {
        if (questVariables.collectedWood >= 5 &&
            questVariables.collectedStone >= 5 &&
            questVariables.cleanedGrave >= 4 &&
            questVariables.prayedAtChapel)
        {
            questVariables.isEverythingAchieved = true;
            UIHandler.Instance.panelText.text = "Everything ready for ritual!";
            // Debug.Log("All quests completed!");
        }
        else
        {
            questVariables.isEverythingAchieved = false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            StartRirtualScene();
        }
        // Liczenie czasu od startu gry
    }

    public void StartRirtualScene()
    {
            // Rozpocznij dialog i po jego zakończeniu przejdź do sceny
            DialogueManager.Instance.StartDialogue(
        dialogue: DialogueManager.Instance.startRitualMonologue,
        onEnd: () => SceneManager.LoadScene("RitualScene")
    );
    }
    public void StartGraveyardScene()
    {
            // Rozpocznij dialog i po jego zakończeniu przejdź do sceny
            DialogueManager.Instance.StartDialogue(
        dialogue: DialogueManager.Instance.startGraveyardMonologue,
        onEnd: () => SceneManager.LoadScene("Game")
    );
    }
}

