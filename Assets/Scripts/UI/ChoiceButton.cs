using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public DialogueAnswerCorrectness isCorrect;
    public DialogueUI dialogueUI;
    public DialogueData choiceDialogue;
    void Start()
    {
        
    }
    // public void 
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectChoice()
    {
        DialogueManager.Instance.answerCorrectness = isCorrect;

        DialogueManager.Instance.StartDialogue(choiceDialogue,isCorrect);
        
        dialogueUI.HideMultipleChoiceUI();
    }

}
