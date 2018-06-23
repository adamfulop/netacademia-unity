using System.Collections.Generic;
using UnityEngine;

public class RecycleGameObject : MonoBehaviour {
    private List<IRecycle> _recycleComponents;

    private void Awake() {
        var components = GetComponents<MonoBehaviour>();
        _recycleComponents = new List<IRecycle>();
        foreach (var component in components) {
            if (component is IRecycle) {
                _recycleComponents.Add(component as IRecycle);
            }
        }
    }

    // Instantiate helyett
    public void Restart() {
        gameObject.SetActive(true);
        // többi scriptre
        foreach (var recycleComponent in _recycleComponents) {
            recycleComponent.Restart();
        }
    }

    // Destroy helyett
    public void Shutdown() {
        gameObject.SetActive(false);
        // többi scriptre
        foreach (var recycleComponent in _recycleComponents) {
            recycleComponent.Shutdown();
        }
    }
}
