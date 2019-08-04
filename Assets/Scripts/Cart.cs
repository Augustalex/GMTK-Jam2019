using System;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cart : MonoBehaviour
{
    public int Score;
    public AudioClip CartPush;
    private AudioSource _audioSource;
    private const float TimeBetweenSounds = 1;
    private float _timeSinceSound = TimeBetweenSounds;
    private GameObject _rider;
    private GameObject _player;
    private Vector3 _riderOriginalDistanceToCart;
    private double _timeSinceLastRide;

    public void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = CartPush;
    }

    private void Update()
    {
        _timeSinceSound += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && !_rider && _timeSinceLastRide > 1)
        {
            _timeSinceLastRide = 0;

            var distance = Vector2.Distance(transform.position, _player.transform.position);
            if (distance < 2 && !_player.GetComponent<PlayerItemHolder>().RecentlyHeldAnItem())
            {
                HopOn(_player);
            }
        }

        if (_rider)
        {
            _rider.transform.position = transform.position;
        }
        else
        {
            _timeSinceLastRide += Time.deltaTime;
        }
    }

    public void AddItem()
    {
        Score += 1;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (_timeSinceSound > TimeBetweenSounds && Random.value < .8f)
        {
            _timeSinceSound = 0;

            _audioSource.Play();
        }
    }

    private async void HopOn(GameObject obj)
    {
        _rider = obj;
        _riderOriginalDistanceToCart = transform.position - obj.transform.position;

        var riderBody = _rider.GetComponent<Rigidbody2D>();
        riderBody.simulated = false;
        var cartBody = GetComponent<Rigidbody2D>();
        var velocityNormalized = riderBody.velocity.normalized;
        cartBody.AddForce(velocityNormalized * .05f, ForceMode2D.Force);

        _audioSource.Play();
        await Task.Delay(1300);

        _rider.transform.position -= _riderOriginalDistanceToCart;
        riderBody.simulated = true;
        _rider = null;
    }
}