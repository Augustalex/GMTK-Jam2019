using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(SpriteRenderer))]
public class ItemBase : MonoBehaviour
{
	public CircleCollider2D circleCol;

	private void Reset()
	{
		GetComponent<CircleCollider2D>().isTrigger = true;
		gameObject.tag = "Item";
	}

	private void Awake()
	{
		circleCol = GetComponent<CircleCollider2D>();
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

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			var itemHolder = collision.GetComponent<PlayerItemHolder>();

			itemHolder.nearbyItem = this;
		}
	}

	protected virtual void OnTriggerExit2D(Collider2D collision)
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
