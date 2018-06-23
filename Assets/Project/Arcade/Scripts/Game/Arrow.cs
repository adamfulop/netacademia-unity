using UnityEngine;

public class Arrow : MonoBehaviour {
    [SerializeField] private float _speed;
    
    private Rigidbody2D _rigidbody2D;

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        _rigidbody2D.AddForce(transform.up * _speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (!other.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
