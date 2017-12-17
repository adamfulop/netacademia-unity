using UnityEngine;

public class CharacterVRMove : MonoBehaviour {
    public float Speed = 0.1f;

    private Rigidbody _rigidbody;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        var move = GvrControllerInput.IsTouching ? Speed : 0;
        var direction = GvrControllerInput.Orientation * Vector3.forward;
        
        _rigidbody.MovePosition(transform.position + direction * move);
        _rigidbody.MoveRotation(Quaternion.Euler(0, GvrControllerInput.Orientation.eulerAngles.y, 0));
    }
}
