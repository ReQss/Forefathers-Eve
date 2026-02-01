using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [TextArea(3, 5)]
    public string text;
    public string characterName = "Duch";
    public Sprite characterPortrait; // Obraz postaci do wyświetlenia po lewej stronie
    public AudioClip dialogueAudioClip;
}
[System.Serializable]
public class DialogueChoice
{
    public string choiceText;
    public DialogueAnswerCorrectness correctness;
    public DialogueData afterChoiceDialogue;
    
}
[System.Serializable]
public class DialogueMultipleChoice
{
    public string choiceDescription;
    public List<DialogueChoice> choices;
}
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public DialogueLine[] dialogueLines;
    public string ghostName = "Duch";
    public bool isMultipleChoice = false;
    public DialogueMultipleChoice multipleChoice;
    public Sprite defaultGhostPortrait; // Domyślny obraz ducha jeśli linia dialogowa nie ma własnego
    public bool startNextDialogueAutomatically = false;
    public bool spawnEnemy = false;
    public bool setPlayerHealthToOne = false;
    public bool stopSoundAfterDialogue = false;
}

