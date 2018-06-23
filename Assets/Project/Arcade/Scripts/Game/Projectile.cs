using Lean.Pool;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public Vector2 Velocity;

    private Rigidbody2D _rigidbody2D;

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        _rigidbody2D.velocity = Velocity;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) return;
        
        LeanPool.Despawn(gameObject);
    }
}
