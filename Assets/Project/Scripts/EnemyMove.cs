using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour {
    private NavMeshAgent _navMeshAgent;
    private Transform _player;

    private void Awake() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() {
        // minden pillanatban a játékos pozíciója a célja a zombinak
        _navMeshAgent.SetDestination(_player.position);
    }

    private void OnTriggerEnter(Collider other) {
        // ha elkapott egy játékost (= ütközött egy játékos taggel rendelkező colliderrel)
        // a játékostól elveszünk egy pontot
        if (other.CompareTag("Player")) {
            if (other.gameObject.GetComponent<PlayerInventory>().PickupCount > 0)
                other.gameObject.GetComponent<PlayerInventory>().PickupCount--;
            Debug.Log("Zombie elkapta a játékost!");
        }
    }
}
