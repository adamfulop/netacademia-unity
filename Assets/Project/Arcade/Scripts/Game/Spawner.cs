using System.Collections;
using Lean.Pool;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject[] Prefabs;
    public SpawnedObjectsContainer SpawnedObjectsContainer;
    public float Delay;
    public bool Active = true;
    public Vector2 DelayRange;

    private void Start() {
        ResetDelay();
        StartCoroutine(ObjectGenerator());
    }

    private IEnumerator ObjectGenerator() {
        yield return new WaitForSeconds(Delay);

        if (Active) {
            // Pooling nélkül: Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], transform.position, Quaternion.identity);
            // Saját pool: GameObjectUtil.Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], transform.position);
            LeanPool.Spawn(Prefabs[Random.Range(0, Prefabs.Length)], transform.position, Quaternion.identity, SpawnedObjectsContainer.transform);
            ResetDelay();
        }

        // vagy InvokeRepeating
        StartCoroutine(ObjectGenerator());
    }

    private void ResetDelay() {
        Delay = Random.Range(DelayRange.x, DelayRange.y);
    }
}
