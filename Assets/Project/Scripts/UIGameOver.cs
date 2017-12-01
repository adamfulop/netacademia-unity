using System.Linq;
using UnityEngine;

public class UIGameOver : MonoBehaviour {
    private GameState _gameState;
    private RectTransform[] _children;

    private void Awake() {
        // GameOver object gyerek objektumait (RectTransformjait) összegyűjtjük úgy, hogy ez az objektum kimaradjon
        _children = gameObject.GetComponentsInChildren<RectTransform>()
            .Where(rt => rt.gameObject != gameObject) // csak azokat vesszük be, amelyek nem ehhez a GameObject-hez tartoznak
            .ToArray();
        _gameState = FindObjectOfType<GameState>();
    }

    private void Start() {
        foreach (var child in _children) {
            // kezdetben inaktív a GameOver képernyő
            child.gameObject.SetActive(false);
        }
    }

    private void Update() {
        // ha a játékos már nem nyerhet, aktiváljuk a GameOver képernyőt
        if (_gameState.CannotWin()) {
            foreach (var child in _children) {
                child.gameObject.SetActive(true);
            }
        }
    }
}
