using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemHolder : MonoBehaviour
{
	public ItemBase heldItem;
	public ItemBase nearbyItem;

	public Transform itemHeldPosition;

    void Awake()
    {
		if (itemHeldPosition == null)
			itemHeldPosition = transform;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			PickupItem();
		}
    }

	private void PickupItem()
	{
		if (nearbyItem == null)
			return;

		heldItem = nearbyItem;
		nearbyItem = null;

		heldItem.transform.position = itemHeldPosition.position;
		heldItem.transform.parent = itemHeldPosition;
	}

	private void DropItem()
	{
		if (heldItem == null)
			return;

		heldItem.transform.parent = null;
		heldItem = null;
	}
}
