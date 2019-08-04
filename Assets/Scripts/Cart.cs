using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cart : MonoBehaviour
{
    public int Score;
    public AudioClip CartPush;
    private AudioSource _audioSource;
    private const float TimeBetweenSounds = 1;
    private float _timeSinceSound = TimeBetweenSounds;

    public void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = CartPush;
    }

    private void Update()
    {
        _timeSinceSound += Time.deltaTime;
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
}