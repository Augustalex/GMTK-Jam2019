using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private PlayerMovement _movement;

    public bool Dead;
    
    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    private void Die()
    {
        Dead = true;
        _movement.Die();
    }
}