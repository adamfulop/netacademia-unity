using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    private ScoreText _arrowText;

    [SerializeField] private int _arrowCount;

    private void Awake() {
        _arrowText = GameObject.FindGameObjectWithTag("ArrowsText").GetComponent<ScoreText>();
    }

    public int ArrowCount {
        get { return _arrowCount; }
        set {
            _arrowCount = value;
            _arrowText.ScoreValue = _arrowCount;
        }
    }
}