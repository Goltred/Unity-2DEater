using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Scenes/In-Game");
    }
}
