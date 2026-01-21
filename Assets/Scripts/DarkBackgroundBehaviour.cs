using UnityEngine;

public class DarkBackgroundBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject dialoguePanel;
    void Start()
    {
        
    }
    public void DisableDialoguePanel()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }
    public void EnableDialoguePanel()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
