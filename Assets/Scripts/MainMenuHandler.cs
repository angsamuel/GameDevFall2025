using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuHandler : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("DemoScene");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
