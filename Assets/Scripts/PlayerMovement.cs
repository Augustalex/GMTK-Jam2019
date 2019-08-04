using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 _originalScale;
    private Rigidbody2D _body;
    private Animator _animator;

    public bool Dead;
    public bool Rushing;

    private void Awake()
    {
        _originalScale = transform.localScale;
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Dead) return;

        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if (horizontal > 0 || horizontal < -0 || vertical < -0 || vertical > 0)
        {
            var velocity = GetVelocity();
            var movement = new Vector2(
                horizontal * Time.deltaTime * velocity,
                vertical * Time.deltaTime * velocity
            );
            _body.AddForce(
                movement,
                ForceMode2D.Force
            );
            _animator.SetTrigger("Walk");

            if (Math.Abs(horizontal) > 0)
            {
                var direction = Math.Sign(horizontal);
                transform.localScale = new Vector3(_originalScale.x * direction, _originalScale.y, _originalScale.z);
            }
        }
        else
        {
            _animator.SetTrigger("Idle");
        }
    }

    public void Die()
    {
        _animator.SetTrigger("Die");
        _body.velocity = Vector2.zero;
        Dead = true;
    }

    private int GetVelocity()
    {
        if (Rushing)
        {
            return 500;
        }

        return 260;
    }
}