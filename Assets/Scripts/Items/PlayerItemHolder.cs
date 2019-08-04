using UnityEngine;

public class PlayerItemHolder : MonoBehaviour
{
    public static PlayerItemHolder instance;

    public ItemBase heldItem;
    public ItemBase nearbyItem;

    public Transform itemHeldPosition;
    private float _timeSinceHeltAnItem;
    private float _timeSincePickedUpItem;

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
            var cartPosition = CartItemHolder.instance.gameObject.transform.position;
            if (Vector2.Distance(transform.position, cartPosition) > 2f)
            {
                if (heldItem == null)
                    PickupItem();
                else
                    DropItem();
            }
        }


        if (Input.GetKeyDown(KeyCode.F) && heldItem)
        {
            heldItem.Use();
        }

        if (heldItem)
        {
            _timeSincePickedUpItem += Time.deltaTime;
            _timeSinceHeltAnItem = 0;
        }
        else
        {
            _timeSinceHeltAnItem += Time.deltaTime;
        }
    }

    public void PickupItem()
    {
        if (nearbyItem == null)
            return;

        _timeSincePickedUpItem = 0;

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

        var item = TransferItemAway();
        item.OnDrop();
    }

    public ItemBase TransferItemAway()
    {
        heldItem.circleCol.enabled = true;
        heldItem.transform.parent = null;

        var item = heldItem;
        heldItem = null;
        return item;
    }

    public bool RecentlyHeldAnItem()
    {
        return _timeSinceHeltAnItem < .5f;
    }

    public bool HaveRecentlyPickedUpItem()
    {
        return _timeSincePickedUpItem < .1f;
    }
}