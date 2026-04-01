using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Events;

[System.Serializable]
public class DialogueLine
{
    public string speakerName; 
    public Sprite avatar;
    [TextArea(3, 5)]
    public string text;
    public float displayTime = 2f;

    [Header("event after text")]
    public UnityEvent onLineComplete;
} // class for dialouge text 

public class DialogueManager : MonoBehaviour
{
    [Header("dialouge arr")]
    public DialogueLine[] dialogueLines;

    [Header("UI")]
    public Image avatarImage;
    public TextMeshProUGUI textField;
    public TextMeshProUGUI speakerNameText;

    [Header("settings")]
    public float typingSpeed = 0.05f;

    private int currentIndex = 0;
    private bool isTyping = false;
    private string currentText = "";

    void Start()
    {
        if (dialogueLines.Length > 0)
        {
            ShowLine(0);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                // show all text if isTyping
                StopCoroutine("TypeText");
                textField.text = currentText;
                isTyping = false;
            }
            else
            {
                //else go to next dialouge
                NextLine();
            }
        }
    }

    void NextLine()
    {
        currentIndex++;

        if (currentIndex < dialogueLines.Length)
        {
            ShowLine(currentIndex);
        }
        else
        {
            EndDialogue();
        }
    }

    void ShowLine(int index)
    {
        DialogueLine line = dialogueLines[index];

        // change avatar
        if (line.avatar != null)
        {
            avatarImage.sprite = line.avatar;
            avatarImage.enabled = true;
        }
        else
        {
            avatarImage.enabled = false;
        }

        // change name
        speakerNameText.text = line.speakerName;

        // start typing effect lol
        currentText = line.text;
        StartCoroutine(TypeText(line.text));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        textField.text = "";

        foreach (char letter in text.ToCharArray())
        {
            textField.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        dialogueLines[currentIndex].onLineComplete?.Invoke(); // call event
        yield return new WaitForSeconds(dialogueLines[currentIndex].displayTime);
    }

    void EndDialogue()
    {
        gameObject.SetActive(false);
        Debug.Log("all dialogs is end");
        // end
    }
}