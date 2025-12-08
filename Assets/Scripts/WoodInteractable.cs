using UnityEngine;

public class WoodInteractable : Interactable
{
    public string interactionTipAfter = "This is some nicely chopped wood!";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Interaction()
    {
    
        
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            this.gameObject.SetActive(false);
            GameManager.Instance.collectedWood += 1;
        }
        
       UIHandler.Instance.playerTipsText.text = interactionTipAfter;
    }
}
