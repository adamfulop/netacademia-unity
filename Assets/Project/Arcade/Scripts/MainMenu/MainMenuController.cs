using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    public void OnStartGameClick() {
        SceneManager.LoadSceneAsync("Arcade Game");
    }

    public void OnHighScoresClick() {
        SceneManager.LoadSceneAsync("High Scores");
    }
}
