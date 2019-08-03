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
        if (horizontal > 0.1f || horizontal < 0.1f || vertical < 0.1f || vertical > 0.1f)
        {
            GetComponent<Rigidbody2D>().AddForce(
                new Vector2(
                    horizontal * Time.deltaTime * 10000,
                    vertical * Time.deltaTime * 10000
                )
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