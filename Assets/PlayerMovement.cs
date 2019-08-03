using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Vector2 _originalScale;
    private Rigidbody2D _body;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Start() {
        _originalScale = transform.localScale;
        _body = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        _animator = GetComponentInChildren<Animator>();
        if (horizontal > 0 || horizontal < -0 || vertical < -0 || vertical > 0) {
            var movement = new Vector2(
                horizontal * Time.deltaTime * 60,
                vertical * Time.deltaTime * 60
            );
            _body.AddForce(
                movement,
                ForceMode2D.Force
            );
            _animator.SetTrigger("Walk");

            if (Math.Abs(horizontal) > 0) {
                var direction = Math.Sign(horizontal);
                transform.localScale = new Vector2(_originalScale.x * direction, _originalScale.y);
            }
        }
        else {
            _animator.SetTrigger("Idle");
        }

        //_spriteRenderer.sortingOrder = -(int) (transform.position.y * 100);
        _spriteRenderer.sortingOrder = 2;
    }
}