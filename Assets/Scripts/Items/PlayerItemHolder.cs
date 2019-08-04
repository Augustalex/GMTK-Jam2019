using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemHolder : MonoBehaviour
{
    public static PlayerItemHolder instance;

    public ItemBase heldItem;
    public ItemBase nearbyItem;

    public Transform itemHeldPosition;
    private float _timeSinceHeltAnItem;

    void Awake()
    {
        instance = this;

        if (itemHeldPosition == null)
            itemHeldPosition = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (heldItem == null)
                PickupItem();
            else
                DropItem();
        }


        if (Input.GetKeyDown(KeyCode.F) && heldItem)
        {
            heldItem.Use();
        }

        if (!heldItem)
        {
            _timeSinceHeltAnItem += Time.deltaTime;
        }
        else
        {
            _timeSinceHeltAnItem = 0;
        }
    }

    public void PickupItem()
    {
        if (nearbyItem == null)
            return;

        heldItem = nearbyItem;
        nearbyItem = null;

        heldItem.circleCol.enabled = false;
        heldItem.transform.position = itemHeldPosition.position;
        heldItem.transform.parent = itemHeldPosition;
    }

    public void DropItem()
    {
        if (heldItem == null)
            return;

        heldItem.OnDrop();

        heldItem.circleCol.enabled = true;
        heldItem.transform.parent = null;
        heldItem = null;
    }

    public bool RecentlyHeldAnItem()
    {
        return _timeSinceHeltAnItem < .5f;
    }
}