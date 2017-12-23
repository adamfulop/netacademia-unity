using System.Linq;
using UnityEngine;

public class UIGameOver : MonoBehaviour {
    private RectTransform[] _children;
    private Animator _animator;

    private void Awake() {
        // GameOver object gyerek objektumait (RectTransformjait) összegyűjtjük úgy, hogy ez az objektum kimaradjon
        _children = gameObject.GetComponentsInChildren<RectTransform>()
            .Where(rt => rt.gameObject != gameObject) // csak azokat vesszük be, amelyek nem ehhez a GameObject-hez tartoznak
            .ToArray();
        _animator = GetComponent<Animator>();
    }

    private void Start() {
        foreach (var child in _children) {
            // kezdetben inaktív a GameOver képernyő
            child.gameObject.SetActive(false);
        }
    }

    public void Show() {
        foreach (var child in _children) {
            child.gameObject.SetActive(true);
        }
        _animator.SetTrigger("GameOver");
    }
}
