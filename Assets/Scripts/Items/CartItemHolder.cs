using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartItemHolder : MonoBehaviour
{
    public static CartItemHolder instance;

    public List<ItemBase> items = new List<ItemBase>();
    private Cart _cart;

    private void Awake()
    {
        instance = this;

        _cart = GetComponent<Cart>();
    }

    public void AddItem(ItemBase item)
    {
        if (!items.Exists(x => x == item))
        {
            items.Add(item);
            _cart.AddItem();
        }

        item.transform.position = new Vector3(-1000, -1000);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            AddItem(collision.GetComponent<ItemBase>());
            PlayerItemHolder.instance.DropItem();
        }
    }
}