using UnityEngine;

#if UNITY_EDITOR // Это нужно, чтобы работало в редакторе
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        // Если это сборка — закрываем игру
        Application.Quit();
#endif
    }
}