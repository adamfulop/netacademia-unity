using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class EnemyMove : NetworkBehaviour {
    private NavMeshAgent _navMeshAgent;
    public List<GameObject> Players = new List<GameObject>();

    private void Awake() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        // minden pillanatban a játékos pozíciója a célja a zombinak
        if (isServer && Players.Count > 0) {
            var minDistance = Players.Min(p => Vector3.Distance(transform.position, p.transform.position));
            var target = Players.FirstOrDefault(p =>
                Vector3.Distance(transform.position, p.transform.position) == minDistance);
            if (target != null) {
                _navMeshAgent.SetDestination(target.transform.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        // ha elkapott egy játékost (= ütközött egy játékos taggel rendelkező colliderrel)
        // a játékostól elveszünk egy pontot
        if (isServer && other.CompareTag("Player")) {
            if (other.gameObject.GetComponent<PlayerInventory>().PickupCount > 0)
                other.gameObject.GetComponent<PlayerInventory>().PickupCount--;
            Debug.Log("Zombie elkapta a játékost!");
        }
    }
}
