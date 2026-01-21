using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadScene(string sceneName){
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
    public void ExitGame(){
        Application.Quit();
    }
    public void ToggleGameObject(GameObject obj){
        obj.SetActive(!obj.activeSelf);
    }
}
