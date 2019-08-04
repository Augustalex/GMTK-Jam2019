using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cart : MonoBehaviour
{
    public int Score;
    public AudioClip CartPush;
    private AudioSource _audioSource;

    public void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = CartPush;
    }

    public void AddItem()
    {
        Score += 1;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!_audioSource.isPlaying || Random.value < .3f)
        {
            _audioSource.Play();
        }
    }
}