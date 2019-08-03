using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyDrinkItem : ItemBase
{
    void Start()
    {
    }

    protected override void Use()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRushEffect>().Activate();
        DisposeOf();
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