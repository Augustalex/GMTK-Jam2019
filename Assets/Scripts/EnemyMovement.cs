using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour {
    private const float DiscoverPlayerRadius = 10.0f;
    private const float Speed = 5;

    public GameObject Face;
    public bool IsEating;
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

        var faceScale = new Vector3(DiscoverPlayerRadius * 0.2f, DiscoverPlayerRadius * 0.2f, 1);
        var sightSprite = Face.GetComponentInChildren<SpriteRenderer>();
        sightSprite.transform.localScale = faceScale;
        sightSprite.transform.Translate(DiscoverPlayerRadius * 0.25f, 0, 0);
    }

    private bool CanSeeObject(Vector3 position, Func<GameObject, bool> isObject) {
        var distance = Vector2.Distance(transform.position, position);
        var objectDirection = (position - transform.position).normalized;
        var withinSight = Vector2.Angle(_currentDirection, objectDirection) <= 45;
        var withinDistance = distance <= DiscoverPlayerRadius;
        var objectVisible = IsObjectVisible(position, isObject);
        return withinDistance && withinSight && objectVisible;
    }

    private Vector2 DirectionTo(Vector3 position) {
        {
            return (position - transform.position).normalized;
        }
    }

    private void Update() {
        if (IsEating) {
            return;
        }
        
        var baits = GameObject.FindGameObjectsWithTag("Item")
            .Where(x => x.GetComponent<FoodItem>()?.Bait == true);

        Vector2? target = null;
        foreach (var bait in baits) {
            var canSeeBait = CanSeeObject(bait.transform.position, o => o.CompareTag("Item"));
            if (canSeeBait) {
                target = bait.transform.position;
            }
        }

        var canSeePlayer = CanSeeObject(_player.transform.position, o => o.CompareTag("Player"));
        if (!target.HasValue && canSeePlayer) {
            seingPlayer = true;
            target = _player.transform.position;
        }

        if (target.HasValue) {
            _body.AddForce(_currentDirection * Speed * 0.9f, ForceMode2D.Force);
            _currentDirection = DirectionTo(target.Value);
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

    private bool IsObjectVisible(Vector3 position, Func<GameObject, bool> isObject) {
        var results = new List<RaycastHit2D>();
        var layerMask = ~ LayerMask.GetMask("Enemy");
        var filter = new ContactFilter2D {
            useLayerMask = true,
            layerMask = layerMask
        };

        if (Physics2D.Linecast(transform.position, position, filter, results) > 0) {
            return results.TrueForAll(x => isObject(x.collider.gameObject));
        }

        return true;
    }

    private async void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<FoodItem>()?.Bait == true) {
            IsEating = true;
            _body.velocity = Vector2.zero;
            await Task.Delay(2000);
            Destroy(other.gameObject);
            IsEating = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (Random.value < 0.2f) {
            _currentDirection = Random.insideUnitCircle;
        }
    }
}