using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadScene(int sceneIndex) => SceneManager.LoadScene(sceneIndex);
    public void QuitGame() => Application.Quit();
}
