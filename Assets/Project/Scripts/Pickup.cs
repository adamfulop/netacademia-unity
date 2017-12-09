using UnityEngine;
using UnityEngine.Networking;

public class Pickup : NetworkBehaviour {
    // ha egy játékos felveszi (= ütközik vele), akkor a játékos pontszámát növeljük, és deaktiváljuk a pickupot
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (isServer) {
                other.gameObject.GetComponent<PlayerInventory>().PickupCount++;
            }
            gameObject.SetActive(false);
        }
    }
}