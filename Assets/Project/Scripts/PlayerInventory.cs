using UnityEngine;
using UnityEngine.Networking;

// játékos model osztály
public class PlayerInventory : NetworkBehaviour {
    [SyncVar]
    public int PickupCount = 0;    // hány pickup van nála jelenleg

    // csak a serveren hívódik meg
    public override void OnStartServer() {
        FindObjectOfType<GameState>().PlayerInventories.Add(this);
    }

    [ClientRpc]
    public void RpcPlayerWin() {
        if (isLocalPlayer) Debug.Log("Nyertél! :)");
    }

    [ClientRpc]
    public void RpcPlayerLost() {
        if (isLocalPlayer) FindObjectOfType<UIGameOver>().Show();
    }
}
