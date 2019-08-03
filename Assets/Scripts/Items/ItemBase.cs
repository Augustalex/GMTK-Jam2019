using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ItemBase : MonoBehaviour
{
	private void Reset()
	{
		GetComponent<CircleCollider2D>().isTrigger = true;
	}

	void Start()
    {
        
    }

    void Update()
    {
        
    }

	public void Pickup()
	{

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			var itemHolder = collision.GetComponent<PlayerItemHolder>();

			itemHolder.nearbyItem = this;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			var itemHolder = collision.GetComponent<PlayerItemHolder>();
			if(itemHolder.nearbyItem == this)
			{
				itemHolder.nearbyItem = null;
			}

		}
	}
}
