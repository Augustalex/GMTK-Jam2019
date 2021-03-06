using System;
using UnityEngine;

public class EndGameDoor : MonoBehaviour
{
    private Cart _cart;
    private bool _hasEnded;

    // Start is called before the first frame update
    void Start()
    {
        _cart = GameObject.FindGameObjectWithTag("Cart").GetComponent<Cart>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var component = other.transform.parent.GetComponent<Cart>();
        if (component)
        {
            _hasEnded = true;
        }
    }

    public int GetScore()
    {
        return _cart.Score;
    }

    public bool HasEnded()
    {
        return _hasEnded;
    }
}