using UnityEngine;

public class InputState : MonoBehaviour {
    public bool IsActionButtonPressed;
    public bool IsSpacePressed;
    public bool IsStanding;
    public float StandingThreshold;
    public Vector2 Velocity;

    private Rigidbody2D _rigidbody2D;

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            IsActionButtonPressed = false;
            IsSpacePressed = true;
        } else {
            IsActionButtonPressed = Input.anyKeyDown;
            IsSpacePressed = false;
        }
    }

    private void FixedUpdate() {
        Velocity = new Vector2(Mathf.Abs(_rigidbody2D.velocity.x), Mathf.Abs(_rigidbody2D.velocity.y));
        IsStanding = Velocity.y <= StandingThreshold;
    }
}