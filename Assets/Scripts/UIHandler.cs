using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance { get; private set; }

    public TextMeshProUGUI dayTimer;
    public TextMeshProUGUI playerTipsText;

    private float nextAdviceTime = 0f;

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

    void Start()
    {
        ScheduleNextAdvice();
    }

    void Update()
    {
        // Liczenie czasu
        float percentOfDay = GameManager.Instance.elapsedTime / 60f;
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
