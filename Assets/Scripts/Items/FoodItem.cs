using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : ItemBase
{
    public bool Bait;

    void Start()
    {
    }

    void Update()
    {
    }

    protected override void Use()
    {
    }

    public override void OnDrop()
    {
        Bait = true;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }
}