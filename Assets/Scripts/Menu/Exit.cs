using UnityEngine;

#if UNITY_EDITOR // Загружаем библиотеку UnityEditor если она присутствует
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Quitting");
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // Останавливает игру в редакторе
#else
        Application.Quit(); // Остановливает игру, если она запущена вне редактора.
#endif
    }
}