using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if (horizontal > 0 || horizontal < -0 || vertical < -0 || vertical > 0)
        {
            var movement = new Vector2(
                horizontal * Time.deltaTime * 30,
                vertical * Time.deltaTime * 30
            );
            GetComponent<Rigidbody2D>().AddForce(
                movement,
                ForceMode2D.Force
            );
            GetComponentInChildren<Animator>().SetTrigger("Walk");

            if (horizontal < 0)
            {
                var scale = transform.localScale;
                if (scale.x > 0)
                {
                    transform.localScale = new Vector3(-1, scale.y, scale.z);
                }
            }
            else if (horizontal > 0)
            {
                var scale = transform.localScale;
                if (scale.x < 0)
                {
                    transform.localScale = new Vector3(1, scale.y, scale.z);
                }
            }
        }
        else
        {
            GetComponentInChildren<Animator>().SetTrigger("Idle");
        }
        
        GetComponentInChildren<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 100) + 100;
    }
}