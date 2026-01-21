using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum ResourceType
{
    None,
    Wood,
    Stone,
    Grave,
    Chapel,
    Fire
}
[System.Serializable]
public class QuestResources
{
    [SerializeField]
    public GameObject itemsPanel;
    public ResourceType resourceType;
    [SerializeField]
    public List<GameObject> itemSlots;
    [SerializeField]
    private TextMeshProUGUI questTitle;
    [SerializeField]
    private string questDescription;

}

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance { get; private set; }
    public AnimationFramesClick animationFramesClick;

    public TextMeshProUGUI dayTimer;
    public TextMeshProUGUI playerTipsText;

    private float nextAdviceTime = 0f;
    [SerializeField]
    private List <QuestResources> questResources = new List<QuestResources>();
     [SerializeField]
    public TextMeshProUGUI panelText;
     public Image healthBarImage;
     public GraveyardHandler graveyardHandler;
     public GameObject pauseMenu;

    public string[] tips = {
        "Remember to save your progress!",
        "Explore every corner for hidden secrets.",
        "Use items wisely to survive the night.",
        "Interact with objects to discover clues.",
        "Watch your health and rest when needed."
    };

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    public void UpdateHealthBar()
    {
        if (healthBarImage == null) return;

        float fill = (float)PlayerMovement.Instance.currentHealth /
                    PlayerMovement.Instance.maxHealth;

        healthBarImage.fillAmount = Mathf.Clamp01(fill);
    }
    public void StartUpdateHealthBar()
    {
        if (healthBarImage == null) return;

        int currentHealthStart = 0;

        if (!GameManager.Instance.questVariables.isEverythingAchieved)
        {
            currentHealthStart = PlayerMovement.Instance.maxHealth / 2;
        }
        else 
        {
            currentHealthStart = PlayerMovement.Instance.maxHealth;
        }

        float fill = (float)currentHealthStart / PlayerMovement.Instance.maxHealth;
        healthBarImage.fillAmount = Mathf.Clamp01(fill);
    }


     
    public void RemoveResourceUI(ResourceType resourceType)
    {
        
        GameManager.Instance.CheckQuests();
        foreach (QuestResources qr in questResources)
        {
            if(qr.resourceType == resourceType)
            {
                int i = 0;
                foreach(GameObject item in qr.itemSlots)
                {
                    if(item.activeSelf == false){
                        i++;
                        continue;
                        }
                    else
                    {
                        item.SetActive(false);
                        if(i == qr.itemSlots.Count -1)
                            qr.itemsPanel.SetActive(false);
                        return;
                    }
                    
                }
            }
        }

    }
    public void ResetAllResourceUI(){
        foreach(QuestResources qr in questResources)
        {
            foreach(GameObject item in qr.itemSlots)
            {
                item.SetActive(true);
            }
            qr.itemsPanel.SetActive(true);
        }
    }
    void Start()
    {
        ScheduleNextAdvice();
        StartUpdateHealthBar();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseTheGame();
        }
        if(graveyardHandler != null){
        // Liczenie czasu
            float percentOfDay = graveyardHandler.elapsedTime / 60f;
            int hours = (int)(percentOfDay * 24f);
            int minutes = (int)((percentOfDay * 24f - hours) * 60f);
            dayTimer.text = $"{hours:00}:{minutes:00}";

            // Losowy tip co 15–30s
            if (Time.time >= nextAdviceTime)
            {
                _ = AsyncShowTemporaryTip();
                ScheduleNextAdvice();
            }
        }
    }
    public void PauseTheGame()
    {
        Debug.Log("Game Paused");
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    } 
    private void ScheduleNextAdvice()
    {
        nextAdviceTime = Time.time + Random.Range(15f, 30f);
    }

    
    public async Task AsyncShowTemporaryTip()
    {
        string tip = tips[Random.Range(0, tips.Length)];
        ShowPlayerTip(tip);

        await Task.Delay(5000);
        HidePlayerTip();
    }

    public void ShowPlayerTip(string tip)
    {
        playerTipsText.gameObject.SetActive(true);
        playerTipsText.text = tip;
    }

    public void HidePlayerTip()
    {
        if(playerTipsText != null)
        playerTipsText.gameObject.SetActive(false);
    }
}
