using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChangeManager : MonoBehaviour
{
    public void GoToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
