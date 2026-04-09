using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[System.Serializable]
public class ClickAction
{
    public UnityEvent action;
}

public class FunctionsOnClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ClickAction[] events;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (events == null || !GameData.dialogueIsEnd) return;

        foreach (var e in events)
        {
            e.action?.Invoke();
        }
    }
}
