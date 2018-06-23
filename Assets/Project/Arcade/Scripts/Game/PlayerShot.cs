using UnityEngine;

public class PlayerShot : MonoBehaviour {
    private DestroyOffscreen _destroyOffscreen;

    private void Awake() {
        _destroyOffscreen = GetComponent<DestroyOffscreen>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Projectile")) {
            _destroyOffscreen.OnOutOfBounds();
        }
    }
}
