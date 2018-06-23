using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeMachine : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            SceneManager.LoadSceneAsync("Main Menu");
        }
    }
}
