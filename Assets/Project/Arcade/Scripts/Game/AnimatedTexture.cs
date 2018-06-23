using UnityEngine;

public class AnimatedTexture : MonoBehaviour {
    public Vector2 Speed;

    private Material _material;
    private Vector2 _offset;

    private void Awake() {
        _material = GetComponent<Renderer>().material;
        _offset = _material.GetTextureOffset("_MainTex");
    }

    private void Update() {
        _offset += Speed * Time.deltaTime;
        _material.SetTextureOffset("_MainTex", _offset);
    }
}
