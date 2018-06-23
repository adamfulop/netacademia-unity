using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonBehaviour<T> : MonoBehaviour where T : Object {
    private Button _button;
    protected T Controller;

    private void Awake() {
        _button = GetComponent<Button>();
        Controller = FindObjectOfType<T>();
    }

    private void Start() {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDestroy() {
        _button.onClick.RemoveListener(OnClick);
    }

    protected abstract void OnClick();
}
