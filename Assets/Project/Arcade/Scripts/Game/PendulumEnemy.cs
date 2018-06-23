using UnityEngine;

public class PendulumEnemy : MonoBehaviour {
    private void OnEnable() {
        transform.Translate(0, 130 + 42, 0);
    }
}
