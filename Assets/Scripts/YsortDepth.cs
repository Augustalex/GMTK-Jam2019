using UnityEngine;

public class YsortDepth : MonoBehaviour {
    private SpriteRenderer _spriteRenderer;

    private void Start() {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update() {
        _spriteRenderer.sortingOrder = -(int) (transform.position.y * 100);
    }
}