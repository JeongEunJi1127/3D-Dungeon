using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChangeManager : MonoBehaviour
{
    public void GoToGameScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GameScene");
    }

    public void GoToLobbyScene()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
