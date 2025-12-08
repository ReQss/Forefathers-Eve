using System;
using UnityEngine;

public class GraveInteractable : Interactable
{
    private bool wasInteracted = false;
    public string interactionTipAfter = "The grave looks like new now!";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // string interactionTip = "This grave looks like it needs some cleaning... Press E to interact.";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Interaction()
    {
        if(wasInteracted) return;
        wasInteracted = true;
        
        
        if(Input.GetKeyDown(KeyCode.E))
                    {
                        UIHandler.Instance.animationFramesClick.OpenTargetUI();
                        UIHandler.Instance.animationFramesClick.InitFrames(null);
                    }
        
        interactionTip = interactionTipAfter;
       UIHandler.Instance.playerTipsText.text = interactionTip;
    }
   
  
}
