using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartItemHolder : MonoBehaviour
{
    public static CartItemHolder instance;

    public List<ItemBase> items = new List<ItemBase>();
    private Cart _cart;
    private GameObject _player;

    private void Awake()
    {
        instance = this;

        _player = GameObject.FindGameObjectWithTag("Player");
        _cart = GetComponent<Cart>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var distance = Vector2.Distance(transform.position, _player.transform.position);
            if (distance <= 2f)
            {
                var playerItemHolder = _player.GetComponent<PlayerItemHolder>();
                if (playerItemHolder.heldItem && !playerItemHolder.HaveRecentlyPickedUpItem())
                {
                    var item = playerItemHolder.TransferItemAway();
                    AddItem(item);
                }
            }
        }
    }

    public void AddItem(ItemBase item)
    {
        if (!items.Exists(x => x == item))
        {
            items.Add(item);
            _cart.AddItem();
        }

        item.transform.position = transform.position + new Vector3(.1f + Random.Range(-.1f, .1f), .6f + Random.Range(-.1f, .1f), 0);
        var rotation = item.transform.rotation;
        rotation.z = Random.Range(0, 360);
        item.transform.rotation = rotation;
        item.transform.parent = transform;

        item.tag = "Untagged";
        var drinkItemComponent = item.GetComponent<EnergyDrinkItem>();
        if (drinkItemComponent)
        {
            Destroy(drinkItemComponent);
        }
        var foodItemComponent = item.GetComponent<FoodItem>();
        if (foodItemComponent)
        {
            Destroy(foodItemComponent);
        }
    }
}