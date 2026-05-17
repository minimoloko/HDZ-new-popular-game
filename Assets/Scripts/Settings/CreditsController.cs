using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsAutoSwitch : MonoBehaviour
{
    [Header("Settings")]
    public float duration = 120f;
    public string mainMenuScene = "MainMenu"; 

    [Header("Прокрутка")]
    public float scrollSpeed = 1.8f;

    private float timer = 0f;
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime, Space.Self);

        if (timer > duration - 10f)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, (timer - (duration - 10f)) / 10f);
        }

        if (timer >= duration)
        {
            SceneManager.LoadScene(mainMenuScene);
        }
    }
}