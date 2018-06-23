using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    [SerializeField] private GameObject _arrowPrefab;

    private InputState _inputState;
    private PlayerInventory _playerInventory;

    private void Awake() {
        _inputState = GetComponent<InputState>();
        _playerInventory = GetComponent<PlayerInventory>();
    }

    private void Update() {
        if (_inputState.IsSpacePressed && _playerInventory.ArrowCount > 0) {
            _playerInventory.ArrowCount--;
            Instantiate(_arrowPrefab, transform.position, _arrowPrefab.transform.rotation);
        }
    }
}
