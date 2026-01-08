using UnityEngine;

public class GraveyardHandler : MonoBehaviour
{
    
    public float elapsedTime = 0f;
    
    public bool isDay = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DayNightCycle();
    }
    public void DayNightCycle(){
        
        elapsedTime += Time.deltaTime;
       
        if(elapsedTime >= 59f){
            isDay = !isDay;
            if (isDay)
            {
                QuestManager.Instance.NextQuest();
                ResetQuests();
            }
            else
            {
                GameManager.Instance.StartRirtualScene();
            }
           
            elapsedTime = 0f;
        }
    }
     public void ResetQuests()
    {
        UIHandler.Instance.panelText.text = "ToDo!";

        GameManager.Instance.questVariables.collectedWood = 0;
        GameManager.Instance.questVariables.collectedStone = 0;
        GameManager.Instance.questVariables.cleanedGrave = 0;
        GameManager.Instance.questVariables.prayedAtChapel = false;
        GameManager.Instance.questVariables.isEverythingAchieved = false;
        UIHandler.Instance.ResetAllResourceUI();
    }
}
