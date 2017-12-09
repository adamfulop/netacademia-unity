using UnityEngine.Networking;

public class CharacterInit : NetworkBehaviour {
    public override void OnStartLocalPlayer() {
        FindObjectOfType<CameraFollow>().Player = transform;
    }

    public override void OnStartServer() {
        FindObjectOfType<EnemyMove>().Players.Add(gameObject);
    }
}
