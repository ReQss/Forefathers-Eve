using System.Collections; // ✅ REQUIRED
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum Zone
{
    SaveZone,
    RitualZone,
}
[System.Serializable]
public enum DialogueAnswerCorrectness
{
    Neutral,
    Correct,
    Incorrect,
}
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private DialogueUI dialogueUI;
    
    private DialogueData currentDialogue;
    private int currentLineIndex = 0;
    private bool isDialogueActive = false;
    public DialogueData startRitualMonologue;
    private System.Action onDialogueEnd; 
    public DialogueAnswerCorrectness answerCorrectness;
    public EnemySpawner enemySpawner;
    public SceneDialogues sceneDialogues;
    public Zone zone;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (dialogueUI == null)
        {
            dialogueUI = FindObjectOfType<DialogueUI>();
            if (dialogueUI == null)
            {
                Debug.LogError("DialogueUI nie został znaleziony! Dodaj DialogueUI do sceny.");
            }
        }
        sceneDialogues = GetDialogueDataByZone();
        StartCoroutine(LateStart());
    }
    public SceneDialogues GetDialogueDataByZone()
    {
        switch (zone)
        {
            case Zone.SaveZone:
                return GameManager.Instance.saveZoneDialogues;
            case Zone.RitualZone:
                return GameManager.Instance.ritualZoneDialogues;
            default:
                return null;
        }
    }
      public IEnumerator LateStart()
    {
        if(zone != Zone.SaveZone)yield return null;
        
        yield return new WaitForSeconds(2f);
        if(zone == Zone.SaveZone)
        {
            StartDialogue(GetDialogue());
        }
        yield return null;   
    }
    public DialogueData GetDialogue()
    {
        if (sceneDialogues.currentDialogueIndex < sceneDialogues.dialogues.Count)
        {
            sceneDialogues.currentDialogueIndex++;
            return sceneDialogues.dialogues[sceneDialogues.currentDialogueIndex - 1];

        }
        return null;
    }
    
    void Update()
    {
        // Sprawdzanie inputu podczas dialogu
        if (isDialogueActive)
        {
            // Spacje, Enter lub kliknięcie myszy
            if (Input.GetKeyDown(KeyCode.Space) || 
                Input.GetKeyDown(KeyCode.Return) || 
                Input.GetKeyDown(KeyCode.Mouse0))
            {
                // Jeśli tekst się pisze, zakończ pisanie, w przeciwnym razie przejdź do następnej linii
                if (dialogueUI != null && dialogueUI.IsTyping())
                {
                    dialogueUI.CompleteTyping();
                }
                else
                {
                    NextLine();
                }
            }

            // Escape zamyka dialog
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EndDialogue();
            }
        }
    }

    public void StartDialogue(DialogueData dialogue, DialogueAnswerCorrectness answerCorrectness = DialogueAnswerCorrectness.Neutral, System.Action onEnd = null)
    {
        if (dialogue == null || dialogue.dialogueLines == null || dialogue.dialogueLines.Length == 0)
        {
            Debug.LogWarning("Próba rozpoczęcia pustego dialogu!");
            return;
        }
        if(PlayerMovement.Instance != null)
        {
            PlayerMovement.Instance.animator.SetBool("Running",false);
        }
        currentDialogue = dialogue;
        currentLineIndex = 0;
        isDialogueActive = true;
        onDialogueEnd = onEnd;
        this.answerCorrectness = answerCorrectness;

        // Zatrzymaj ruch gracza podczas dialogu
        if (PlayerMovement.Instance != null)
        {
            PlayerMovement.Instance.enabled = false;
        }

        // Ukryj quest log podczas dialogu
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.HideQuestLog();
        }

        // Pokaż UI dialogu
        if (dialogueUI != null)
        {
            dialogueUI.ShowDialogueUI();
            DisplayCurrentLine();
        }
        else
        {
            Debug.LogError("DialogueUI nie jest przypisany!");
        }
    }

    private void DisplayCurrentLine()
    {
        if (currentDialogue == null || dialogueUI == null) return;

        if (currentLineIndex >= currentDialogue.dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = currentDialogue.dialogueLines[currentLineIndex];
        
        // Użyj obrazu z linii dialogowej lub domyślnego
        Sprite portraitToShow = currentLine.characterPortrait != null 
            ? currentLine.characterPortrait 
            : currentDialogue.defaultGhostPortrait;

        // Użyj imienia z linii lub domyślnego
        string nameToShow = !string.IsNullOrEmpty(currentLine.characterName) 
            ? currentLine.characterName 
            : currentDialogue.ghostName;

        dialogueUI.DisplayDialogue(currentLine.text, nameToShow, portraitToShow);
    }

    public void NextLine()
    {
        if (!isDialogueActive || currentDialogue == null) return;

        currentLineIndex++;

        if (currentLineIndex >= currentDialogue.dialogueLines.Length)
        {
            EndDialogue();
        }
        else
        {
            DisplayCurrentLine();
        }
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        currentDialogue = null;
        currentLineIndex = 0;

        // Przywróć ruch gracza
        if (PlayerMovement.Instance != null)
        {
            PlayerMovement.Instance.enabled = true;
        }

        // Pokaż quest log z powrotem
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.ShowQuestLog();
        }

        // Ukryj UI dialogu
        if (dialogueUI != null)
        {
            dialogueUI.HideDialogueUI();
        }

        if (PlayerMovement.Instance != null && PlayerMovement.Instance.animator != null)
        {
            PlayerMovement.Instance.animator.SetBool("RitualIdle", false);
        }

        if(answerCorrectness == DialogueAnswerCorrectness.Incorrect)
        {
            IncorrectChoice();
        }
        // Wywołaj callback jeśli jest ustawiony
        if (onDialogueEnd != null)
        {
            onDialogueEnd.Invoke();
            onDialogueEnd = null;
        }
    }

    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
    public void IncorrectChoice()
    {
        if(enemySpawner == null)return;
        
        Debug.Log("Incorrect choice made.");
        enemySpawner.SpawnEnemy();
    }
}
