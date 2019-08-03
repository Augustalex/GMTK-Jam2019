using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    public float DiscoverPlayerRadius = 10.0f;
    public float Speed = 10.0f;
    private GameObject _player;
    private Rigidbody2D _body;

    private Vector2 _currentDirection;

    private void Awake() {
        _player = GameObject.FindWithTag("Player");
        _body = GetComponent<Rigidbody2D>();
        _body.gravityScale = 0;
        _body.drag = Speed / 5.0f;
        _currentDirection = Random.insideUnitCircle;
    }

    private void Update() {
        if (Vector2.Distance(_player.transform.position, transform.position) <= DiscoverPlayerRadius) {
            var direction = (_player.transform.position - transform.position).normalized;
            _body.AddForce(direction * Speed * 1.1f, ForceMode2D.Force);
        }
        else {
            _body.AddForce(_currentDirection * Speed, ForceMode2D.Force);

            if (Random.value < 0.08f) {
                _currentDirection = Random.insideUnitCircle;
            }
        }
    }
}