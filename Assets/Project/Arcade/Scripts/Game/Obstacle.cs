using UnityEngine;

public class Obstacle : MonoBehaviour, IRecycle {
    public Sprite[] Sprites;

    private Vector2 _colliderOffset = Vector2.zero;
    
    public void Restart() {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprites[Random.Range(0, Sprites.Length)];

        var boxCollider = GetComponent<BoxCollider2D>();
        var size = spriteRenderer.bounds.size;
        size.y += _colliderOffset.y;

        boxCollider.size = size;
        boxCollider.offset = new Vector2(-_colliderOffset.x, boxCollider.size.y / 2 - _colliderOffset.y);
    }

    public void Shutdown() {
        throw new System.NotImplementedException();
    }
}
