using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    private const float DiscoverPlayerRadius = 10.0f;
    private const float Speed = 5;
    
    public GameObject Face;
    private GameObject _player;
    private Rigidbody2D _body;
    private Vector2 _currentDirection;
    private Vector2 _nextDirection;
    private bool seingPlayer;

    private void Awake() {
        _player = GameObject.FindWithTag("Player");
        _body = GetComponent<Rigidbody2D>();
        _body.gravityScale = 0;
        _body.drag = Speed / 5.0f;
        _currentDirection = Random.insideUnitCircle;
        _nextDirection = Random.insideUnitCircle;

        var faceScale = new Vector3(DiscoverPlayerRadius, DiscoverPlayerRadius, 1);
        var sightSprite = Face.GetComponentInChildren<SpriteRenderer>();
        sightSprite.transform.localScale = faceScale;
        sightSprite.transform.Translate(DiscoverPlayerRadius / 2.0f, 0, 0);
    }

    private void Update() {
        var directionToPlayer = (_player.transform.position - transform.position).normalized;
        var distanceToPlayer = Vector2.Distance(_player.transform.position, transform.position);
        var playerWithinSight = Vector2.Angle(_currentDirection, directionToPlayer) <= 45;
        var playerWithinDistance = distanceToPlayer <= DiscoverPlayerRadius;
        var objectInTheWay = IsPlayerHidden();
        
        if (playerWithinDistance && playerWithinSight && !objectInTheWay) {
            seingPlayer = true;
            _body.AddForce(_currentDirection * Speed * 0.9f, ForceMode2D.Force);
            _currentDirection = directionToPlayer;
        }
        else {
            seingPlayer = false;
            _body.AddForce(_currentDirection * Speed, ForceMode2D.Force);
            if (Random.value < 0.04f) {
                _nextDirection = Random.insideUnitCircle;
            }
        }


        _currentDirection = Vector2.MoveTowards(_currentDirection, _nextDirection, 0.01f);
        var angle = Vector2.SignedAngle(Vector2.right, _currentDirection);
        Face.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private bool IsPlayerHidden() {
        var results = new List<RaycastHit2D>();
        var layerMask = ~ LayerMask.GetMask("Enemy");
        var filter = new ContactFilter2D {
            useLayerMask = true,
            layerMask = layerMask
        };
        
        if (Physics2D.Linecast(transform.position, _player.transform.position, filter, results) > 0) {
            return results.Any(x => !x.collider.CompareTag("Player"));  
        }

        return false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (Random.value < 0.2f) {
            _currentDirection = Random.insideUnitCircle;
        }
    }
}