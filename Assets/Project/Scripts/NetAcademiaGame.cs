using UnityEngine;
using UnityEngine.SceneManagement;

public class NetAcademiaGame : MonoBehaviour {
	void Start () {
		DontDestroyOnLoad(gameObject);
		SceneManager.LoadSceneAsync("Game");
	}
}
