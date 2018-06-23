using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour {
    public float PixelPerUnit;
    public float Scale;

    public Vector2 NativeResolution;

    private void Awake() {
        var gameCamera = GetComponent<Camera>();

        if (gameCamera.orthographic) {
            Scale = Screen.height / NativeResolution.y;
            PixelPerUnit *= Scale;
            gameCamera.orthographicSize = Screen.height / 2f / PixelPerUnit;
        }
    }
}