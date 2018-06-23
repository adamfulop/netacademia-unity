using UnityEngine;

public class Jump : MonoBehaviour {
    public float JumpSpeed;
    public float ForwardSpeed;

    private InputState _inputState;
    private Rigidbody2D _rigidbody2D;

    private void Awake() {
        _inputState = GetComponent<InputState>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (_inputState.IsStanding && _inputState.IsActionButtonPressed) {
            // képernyő közepén tartjuk a játékost
            var horizontalSpeed = transform.position.x < 0 ? ForwardSpeed : 0;
            _rigidbody2D.velocity = new Vector2(horizontalSpeed, JumpSpeed);
        }
    }
}
