using UnityEngine;

public class RitualHandler : MonoBehaviour
{
    public int maxNumberOfGhostsInRitual = 3;
    public int currentNumberOfGhostsInRitual = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DayNightCycle(){
        if(currentNumberOfGhostsInRitual >= maxNumberOfGhostsInRitual){
            GameManager.Instance.StartGraveyardScene();
        }
    }

}
