using UnityEngine;

public class Interactable : MonoBehaviour
{
  
    public string interactionTip = "Hmm.... This looks interesting...";

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public virtual void Interaction()
    {
        UIHandler.Instance.playerTipsText.text = "Wow I can touch it!";
    }
    public void Interaction2()
    {
            if(GameManager.Instance.isDay)
            {
                UIHandler.Instance.playerTipsText.text = "It's still too soon.....";
            }
            else
            {
                UIHandler.Instance.playerTipsText.text = "It's dark, but I can make out some shapes.";
            }
    }
}
