using UnityEngine;

public class InteractableFire : Interactable
{
      public string interactionTipAfter = "Interact with fire to attract the ghost...";
    public DialogueData dialogueData;
    public override void Interaction()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.Instance.StartDialogue(dialogueData);

            PlayerMovement.Instance.isMovementLocked = true;
            PlayerMovement.Instance.animator.SetBool("Interact",false);
            PlayerMovement.Instance.animator.SetBool("RitualIdle",true);
            
        }
        
    //    UIHandler.Instance.playerTipsText.text = interactionTipAfter;
    }
}
