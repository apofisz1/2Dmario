using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    public void OnStartGameClick() {
        // Game képernyőre navigálunk
        SceneManager.LoadSceneAsync("Game");
    }

    public void OnHighScoresClick() {
        // High Scores képernyőre navigálunk
        SceneManager.LoadSceneAsync("High Scores");
    }
}
