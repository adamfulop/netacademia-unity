using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameState : MonoBehaviour {
    public int WinThreshold = 8;        // ennyi pont kell a nyeréshez
    
    public List<MainPlayerInventory> PlayerInventories = new List<MainPlayerInventory>();

    private void Update() {
        var winner = PlayerInventories.FirstOrDefault(pi => pi.PickupCount >= WinThreshold);
        if (winner != null) {
            // szólni a nyertesnek, hogy nyert
            winner.RpcPlayerWin();
            // szólni mindenki másnak, hogy vesztett :(
            foreach (var playerInventory in PlayerInventories) {
                if (playerInventory != winner) playerInventory.RpcPlayerLost();
            }
        }
    }
}
