using UnityEngine;

public class Pickup : MonoBehaviour {
	// ha egy játékos felveszi (= ütközik vele), akkor a játékos pontszámát növeljük, és deaktiváljuk a pickupot
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			other.gameObject.GetComponent<PlayerInventory>().PickupCount++;
			gameObject.SetActive(false);
		}
	}
}
