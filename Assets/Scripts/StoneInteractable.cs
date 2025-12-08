using UnityEngine;

public class StoneInteractable : Interactable
{
    public string interactionTipAfter = "This is some nick rock!";
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
            GameManager.Instance.collectedStone += 1;
        }
        
       UIHandler.Instance.playerTipsText.text = interactionTipAfter;
    }
}
