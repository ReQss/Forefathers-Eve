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
    // public void ResetQuestResources()
    // {
    //     foreach(QuestResources qr in questResources)
    //     {
    //         foreach(GameObject item in qr.itemSlots)
    //         {
    //             item.SetActive(false);
    //         }
    //         // Reset each quest resource as needed
    //     }
    // }
     public void UpdateHealthBar()
{
    if (healthBarImage == null) return;

    float fill = (float)PlayerMovement.Instance.currentHealth /
                 PlayerMovement.Instance.maxHealth;

    healthBarImage.fillAmount = Mathf.Clamp01(fill);
}

     
    public void RemoveResourceUI(ResourceType resourceType)
    {
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
    }

    void Update()
    {
        if(graveyardHandler == null) return;
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
        playerTipsText.gameObject.SetActive(false);
    }
}
