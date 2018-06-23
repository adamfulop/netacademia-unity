using Lean.Pool;
using UnityEngine;

public class ArrowPickup : MonoBehaviour {
    public float HeightOffset;

    private void OnEnable() {
        transform.Translate(0, HeightOffset + 42, 0);    // mert a spawner -42-ben van
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            LeanPool.Despawn(gameObject);
            var inventory = other.GetComponent<PlayerInventory>();
            inventory.ArrowCount++;
        }
    }
}