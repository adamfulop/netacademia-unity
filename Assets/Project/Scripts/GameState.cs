using System.Linq;
using UnityEngine;

public class GameState : MonoBehaviour {
    public int WinThreshold = 8;        // ennyi pont kell a nyeréshez
    
    private PlayerInventory _playerInventory;
    private Pickup[] _pickups;
    
    private void Awake() {
        _pickups = FindObjectsOfType<Pickup>();
        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    private void Update() {
        if (CannotWin()) {
            Debug.Log("A játékos már sajnos nem nyerhet :(");
        }
        
        // ha a játékosnál már legalább annyi pont van, amennyi a nyerési küszöb, akkor nyert
        if (WinThreshold <= _playerInventory.PickupCount) {
            Debug.Log("A játékos nyert! :)");
        }
    }

    // igaz, hogyha a játékos már nem nyerhet (mert nincs elég pont a pályán ahhoz, hogy meglegyen a WinThreshold darab
    // pickup nála)
    // az összes pickup közül megszámolja azokat, amelyek active és enabled -> még felvehető pickupok
    // WinThreshold - PickupCount -> mennyi pickup kell még a játékosnak ahhoz, hogy nyerjen
    public bool CannotWin() {
        return _pickups.Count(p => p.isActiveAndEnabled) < WinThreshold - _playerInventory.PickupCount;
    }
}
