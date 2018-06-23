using System.Collections.Generic;
using System.Linq;
using Lean.Pool;
using UnityEngine;

public class SpawnedObjectsContainer : MonoBehaviour {
    public IEnumerable<Transform> SpawnedObjects {
        get {
            return GetComponentsInChildren<Transform>()
                .Where(t => t != transform && t.gameObject.GetComponent<LeanPoolable>() != null);
        }
    }
}