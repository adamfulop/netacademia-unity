using System.Collections.Generic;
using UnityEngine;

public static class GameObjectUtil {
    private static readonly Dictionary<RecycleGameObject, ObjectPool> Pools = new Dictionary<RecycleGameObject, ObjectPool>();

    public static GameObject Instantiate(GameObject prefab, Vector3 position) {
        GameObject instance;
        
        var recycleScript = prefab.GetComponent<RecycleGameObject>();
        if (recycleScript != null) {
            var pool = GetObjectPool(recycleScript);
            instance = pool.NextObject(position).gameObject;
        } else {
            // Pooling nélkül
            instance = GameObject.Instantiate(prefab);
            instance.transform.position = position;
        }

        return instance;
    }

    public static void Destroy(GameObject gameObject) {
        var recycleGameObject = gameObject.GetComponent<RecycleGameObject>();

        if (recycleGameObject != null) {
            recycleGameObject.Shutdown();
        } else {
            // Pooling nélkül
            GameObject.Destroy(gameObject);
        }
    }

    public static ObjectPool GetObjectPool(RecycleGameObject reference) {
        ObjectPool pool;

        if (Pools.ContainsKey(reference)) {
            pool = Pools[reference];
        } else {
            var poolContainer = new GameObject(reference.gameObject.name + "ObjectPool");
            pool = poolContainer.AddComponent<ObjectPool>();
            pool.Prefab = reference;
            Pools.Add(reference, pool);
        }

        return pool;
    }
}