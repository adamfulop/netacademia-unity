using UnityEngine;
using UnityEngine.UI;

public class UIPoints : MonoBehaviour {
    public PlayerInventory PlayerInventory;
    private Text _text;

    private void Awake() {
        _text = GetComponent<Text>();
    }

    private void Update() {
        if (PlayerInventory != null) {
            // minden updateben frissítjuk a pontszám kijelzőt a játékos pontszáma alapján
            _text.text = string.Format("Pontszám: {0} / 8", PlayerInventory.PickupCount);   
        }
    }
}
