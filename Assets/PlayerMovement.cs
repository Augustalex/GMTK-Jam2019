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
        if (horizontal > 0 || horizontal < 0)
        {
            var horizontalVector = new Vector3(horizontal, 0, 0);

            transform.position = transform.position + horizontalVector * Time.deltaTime * 30;
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