using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int health;
    public float elapsedTime = 0f;
    public bool isDay = true;
    public int collectedWood=0;
    public int collectedStone=0;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
            }
           
            elapsedTime = 0f;
        }
    }
}

