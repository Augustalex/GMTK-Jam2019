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

    public bool Rushing;

    private void Start()
    {
        _originalScale = transform.localScale;
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        _animator = GetComponentInChildren<Animator>();
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

    private int GetVelocity()
    {
        if (Rushing)
        {
            return 500;
        }

        return 260;
    }
}