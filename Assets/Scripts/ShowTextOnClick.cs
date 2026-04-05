using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

public class TypewriterEffect : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI textElement;
    public string fullText;
    public float delay = 0.05f;
    public int waitToClose = 3  ;

    private Coroutine typingCoroutine;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameData.dialogueIsEnd)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;
                textElement.text = fullText;
                return;
            }
            textElement.gameObject.SetActive(true);
            typingCoroutine = StartCoroutine(TypeText());
        }
    }
    IEnumerator TypeText()
    {
        textElement.text = "";

        foreach (char letter in fullText.ToCharArray())
        {
            textElement.text += letter;
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(waitToClose);

        textElement.gameObject.SetActive(false);

        typingCoroutine = null;
    }
}
// srazu skazu tut nado delat TextMesh v object chtobi nastroit norm 