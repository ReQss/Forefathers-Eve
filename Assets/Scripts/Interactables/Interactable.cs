using UnityEngine;

public class Interactable : MonoBehaviour
{
  
    public string interactionTip = "Hmm.... This looks interesting...";
    public ResourceType resourceType;

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
  

}
