using Unity.VisualScripting;
using UnityEngine;

public class InteractableFire : Interactable
{
      public string interactionTipAfter = "Interact with fire to attract the ghost...";
    public DialogueData dialogueData;
    private bool firstInteraction = false;
    public RitualHandler ritualHandler;
    public override void Interaction()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(ritualHandler.currentNumberOfGhostsInRitual < ritualHandler.maxNumberOfGhostsInRitual)
            {
                ritualHandler.currentNumberOfGhostsInRitual += 1;
                
                if (firstInteraction == false)
                {
                    firstInteraction = true;
                    this.gameObject.GetComponent<Animator>().SetTrigger("startFire");
                }
                DialogueManager.Instance.StartDialogue(DialogueManager.Instance.GetDialogue());
                PlayerMovement.Instance.isMovementLocked = true;
                PlayerMovement.Instance.animator.SetBool("Interact",false);
                PlayerMovement.Instance.animator.SetBool("RitualIdle",true);
            }
            else
            {
                ritualHandler.DayNightCycle();
            }
        }
        
    //    UIHandler.Instance.playerTipsText.text = interactionTipAfter;
    }
}
