using Project.Scripts.Signal;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class Pickup : NetworkBehaviour {
    [Inject]
    public PlayerScoreChangedSignal PlayerScoreChangedSignal { private get; set; }
    
    // ha egy játékos felveszi (= ütközik vele), akkor a játékos pontszámát növeljük, és deaktiváljuk a pickupot
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            var inventory = other.gameObject.GetComponent<PlayerInventory>();
            
            if (isServer) {
                inventory.PickupCount++;
            }
            
            if (inventory.isLocalPlayer) {
                // ha a lokális játékossal történt a változás, elsütünk egy signalt
                // (változott a pontszám -> GUI ez alapján frissül)
                PlayerScoreChangedSignal.Fire(inventory.PickupCount);   
            }
            
            gameObject.SetActive(false);
        }
    }
}