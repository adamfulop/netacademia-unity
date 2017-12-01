using UnityEngine;
using UnityEngine.UI;

public class UIPoints : MonoBehaviour {
    private PlayerInventory _playerInventory;
    private Text _text;

    private void Awake() {
        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        _text = GetComponent<Text>();
    }

    private void Update() {
        // minden updateben frissítjuk a pontszám kijelzőt a játékos pontszáma alapján
        _text.text = string.Format("Pontszám: {0} / 8", _playerInventory.PickupCount);
    }
}
