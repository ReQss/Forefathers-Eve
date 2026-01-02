using UnityEngine;
using UnityEngine.SceneManagement;
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
    public float elapsedTime = 0f;
    public bool isDay = true;
    public QuestVariables questVariables = new QuestVariables();
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
    public void ResetQuests()
    {
        UIHandler.Instance.panelText.text = "ToDo!";

        questVariables.collectedWood = 0;
        questVariables.collectedStone = 0;
        questVariables.cleanedGrave = 0;
        questVariables.prayedAtChapel = false;
        questVariables.isEverythingAchieved = false;
        UIHandler.Instance.ResetAllResourceUI();
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
        DayNightCycle();
    }
    public void DayNightCycle(){
        
        elapsedTime += Time.deltaTime;
        if (elapsedTime>=40f)
        {
            if (isDay)
            {
                 PlayerMovement.Instance.SetLanternState(true); 
                PlayerMovement.Instance.SetRakeState(false);
            }
            else 
            {
                 PlayerMovement.Instance.SetLanternState(false); 
                PlayerMovement.Instance.SetRakeState(true);
            }
        }
       
        if(elapsedTime >= 59f){
            isDay = !isDay;
            if (isDay)
            {
                QuestManager.Instance.NextQuest();
                ResetQuests();
            }
            else
            {
                  Debug.Log("Night");
                StartRirtualScene();
            }
           
            elapsedTime = 0f;
        }
    }
    public void StartRirtualScene()
    {
        // Rozpocznij dialog i po jego zakończeniu przejdź do sceny
        DialogueManager.Instance.StartDialogue(
            DialogueManager.Instance.startRitualMonologue,
            () => SceneManager.LoadScene("RitualScene")
        );
    }
}

