using UnityEngine;

public class InteractableGhostScene : Interactable
{
    [Header("Dialogue Settings")]
    [SerializeField] private DialogueData ghostDialogue; // Dialog ducha - przypisz w inspektorze
    
    public override void Interaction()
    {
        // Sprawdź czy jest noc
        if (GameManager.Instance.isDay)
        {
            UIHandler.Instance.ShowPlayerTip("It's still too soon..... The spirits only appear at night.");
            return;
        }

        // Sprawdź czy dialog jest już aktywny
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueActive())
        {
            // Jeśli dialog jest aktywny, przejdź do następnej linii
            DialogueManager.Instance.NextLine();
            return;
        }

        // Sprawdź czy mamy przypisany dialog
        if (ghostDialogue == null)
        {
            UIHandler.Instance.ShowPlayerTip("It's dark, but I can make out some shapes.");
            Debug.LogWarning("InteractableGhostScene nie ma przypisanego DialogueData!");
            return;
        }

        // Rozpocznij dialog
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.StartDialogue(ghostDialogue);
        }
        else
        {
            Debug.LogError("DialogueManager nie został znaleziony w scenie!");
        }
    }
}
