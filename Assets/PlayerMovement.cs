using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if (horizontal > 0 || horizontal < -0 || vertical < -0 || vertical > 0)
        {
            var hor = horizontal > 0 ? 1 : -1;
            var ver = vertical > 0 ? 1 : -1;
            var movement = new Vector2(
                hor * Time.deltaTime * 30,
                ver * Time.deltaTime * 30
            );
            GetComponent<Rigidbody2D>().AddForce(
                movement,
                ForceMode2D.Force
            );
            GetComponent<Animator>().SetTrigger("Walk");

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
            GetComponent<Animator>().SetTrigger("Idle");
        }
    }
}