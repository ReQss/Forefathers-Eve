using UnityEngine;

public class InteractableChapel : Interactable
{
     public string interactionTipAfter = "Praise the Lord!";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    public override void Interaction()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            // this.gameObject.SetActive(false);
            GameManager.Instance.questVariables.prayedAtChapel = true;
            if(resourceType != ResourceType.None)
            {
                UIHandler.Instance.RemoveResourceUI(resourceType);
                PlayerMovement.Instance.animator.SetTrigger("Pray");
            }
        }
        
       UIHandler.Instance.playerTipsText.text = interactionTipAfter;
    }
}
