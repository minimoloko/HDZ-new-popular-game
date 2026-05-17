using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour 
{
    public void LoadScene(string sceneName)
    {
        Debug.Log($"{sceneName} scene changer"); 
        string curScene = SceneManager.GetActiveScene().name;
        if(curScene == "PaaGame") GameData.PaGameIsComplected = true;
        if(curScene == "SniperGame") GameData.SniperGameIsComplected = true;
        SceneManager.LoadScene(sceneName); 
    }
}
