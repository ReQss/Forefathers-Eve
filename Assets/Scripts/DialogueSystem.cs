using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [TextArea(3, 5)]
    public string text;
    public string characterName = "Duch";
    public Sprite characterPortrait; // Obraz postaci do wyświetlenia po lewej stronie
}

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public DialogueLine[] dialogueLines;
    public string ghostName = "Duch";
    public Sprite defaultGhostPortrait; // Domyślny obraz ducha jeśli linia dialogowa nie ma własnego
}

