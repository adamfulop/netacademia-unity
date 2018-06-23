using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    public RecycleGameObject Prefab;

    private List<RecycleGameObject> _poolInstances = new List<RecycleGameObject>();

    private RecycleGameObject CreateInstance(Vector3 position) {
        var clone = Instantiate(Prefab);
        clone.transform.position = position;
        clone.transform.parent = transform;
        
        _poolInstances.Add(clone);

        return clone;
    }

    public RecycleGameObject NextObject(Vector3 position) {
        RecycleGameObject instance = null;
        
        // pooling
        foreach (var poolInstance in _poolInstances) {
            if (poolInstance.gameObject.activeSelf) continue;
            instance = poolInstance;
            instance.transform.position = position;
        }
        
        // pooling nélkül
        if (instance == null) instance = CreateInstance(position);
        instance.Restart();
        return instance;
    }
}
