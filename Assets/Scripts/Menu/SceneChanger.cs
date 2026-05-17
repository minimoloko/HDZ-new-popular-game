using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour 
{
    public void LoadScene(string sceneName)
    {
        Debug.Log($"{sceneName} scene changer"); 
        SceneManager.LoadScene(sceneName); 
        string curScene = SceneManager.GetActiveScene().name;
        if(curScene == "PaaGame") GameData.PaaGameIsComplected = true;
        if(curScene == "SniperGame") GameData.SniperGameIsComplected = true;
    }
}
