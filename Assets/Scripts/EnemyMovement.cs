using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    public float discoverPlayerRadius = 10.0f;
    public float speed = 10.0f;
    private GameObject _player;
    private Rigidbody2D body;

    private Vector2 currentDirection = Random.insideUnitCircle;

    private void Awake() {
        _player = GameObject.FindWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0;
        body.drag = speed / 5.0f;
    }

    void Update() {
        if (Vector2.Distance(_player.transform.position, transform.position) <= discoverPlayerRadius) {
            var direciton = (_player.transform.position - transform.position).normalized;
            body.AddForce(direciton * speed * 1.1f, ForceMode2D.Force );
        }
        else {
            body.AddForce(currentDirection * speed, ForceMode2D.Force);

            if (Random.value < 0.08f) {
                currentDirection = Random.insideUnitCircle;
            }
        }
    }
}