using UnityEngine;

public class Pendulum : MonoBehaviour {
    [SerializeField] private GameObject _chain;
    [SerializeField] private GameObject _enemy;

    private Collider2D[] _colliders;
    private FixedJoint2D _fixedJoint2D;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private void Awake() {
        _colliders = GetComponents<Collider2D>();
        _fixedJoint2D = GetComponent<FixedJoint2D>();
    }

    public void OnSpawn() {
        _chain.SetActive(true);
        _enemy.SetActive(true);

        if (_fixedJoint2D.enabled) {
            _startPosition = transform.position;
            _startRotation = transform.rotation; 
        } else {
            transform.position = _startPosition;
            transform.rotation = _startRotation;
        }
        _fixedJoint2D.enabled = true;

        foreach (var collider in _colliders) {
            collider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        _chain.SetActive(false);
        _fixedJoint2D.enabled = false;
    }

//    private void OnJointBreak2D(Joint2D brokenJoint) {
//        _chain.SetActive(false);
//    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject == _enemy) {
            _enemy.SetActive(false);
            foreach (var collider in _colliders) {
                collider.enabled = false;
            }
        }
    }
}
