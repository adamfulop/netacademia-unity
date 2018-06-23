using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {
    private Animator _animator;
    private InputState _inputState;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _inputState = GetComponent<InputState>();
    }

    private void Update() {
        var running = !(_inputState.Velocity.x > 0 && _inputState.Velocity.y < _inputState.StandingThreshold);
        _animator.SetBool("Running", running);
    }
}
