using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Signal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;
using Zenject;

public class EnemyMove : NetworkBehaviour {
    [Inject]
    public PlayerScoreChangedSignal PlayerScoreChangedSignal { private get; set; }
    
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
        if (other.CompareTag("Player")) {
            var inventory = other.gameObject.GetComponent<MainPlayerInventory>();
            
            if (isServer && other.gameObject.GetComponent<MainPlayerInventory>().PickupCount > 0)
                inventory.PickupCount--;

            if (inventory.isLocalPlayer) {
                // FIGYELEM: ÓRÁN IDŐHIÁNY MIATT KIMARADT VÁLTOZTATÁS! (Pickup.cs alapján)
                // ha a lokális játékost kapta el a zombi, akkor azt is jelezzük
                // hogy frissülhessen a GUI
                PlayerScoreChangedSignal.Fire(inventory.PickupCount);
            }
            
            Debug.Log("Zombie elkapta a játékost!");
        }
    }
}
