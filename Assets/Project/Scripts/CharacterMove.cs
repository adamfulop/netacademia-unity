using System;
using UnityEngine;

public class CharacterMove : MonoBehaviour {
    public float Speed = 10f;
    public float TurnSmoothing = 20f;
    
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Vector3 _movement;

    private const float TOLERANCE = 0.01f;
    
    private void Awake() {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        var inputVertical = Input.GetAxis("Vertical");
        var inputHorizontal = Input.GetAxis("Horizontal");

        Move(inputHorizontal, inputVertical);
        SetAnimation(inputHorizontal, inputVertical);
    }

    private void Move(float inputHorizontal, float inputVertical) {
        _movement.Set(inputHorizontal, 0, inputVertical);
        _movement = _movement.normalized * Speed * Time.deltaTime;
        _rigidbody.MovePosition(transform.position + _movement);

        if (Math.Abs(inputHorizontal) > TOLERANCE || Math.Abs(inputVertical) > TOLERANCE) {
            Rotate(inputHorizontal, inputVertical);
        }
    }

    private void Rotate(float inputHorizontal, float inputVertical) {
        var targetDirection = new Vector3(inputHorizontal, 0, inputVertical);
        var targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        var newRotation = Quaternion.Lerp(_rigidbody.rotation, targetRotation, Time.deltaTime * TurnSmoothing);
        _rigidbody.MoveRotation(newRotation);
    }

    private void SetAnimation(float inputHorizontal, float inputVertical) {
        var isRunning = Math.Abs(inputHorizontal) > TOLERANCE || Math.Abs(inputVertical) > TOLERANCE;
        _animator.SetBool("IsRunning", isRunning);
    }
}
