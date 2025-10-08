using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public void ResetScene(){ //our temp fail condition
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
