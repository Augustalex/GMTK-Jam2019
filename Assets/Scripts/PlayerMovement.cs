using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 _originalScale;
    private Rigidbody2D _body;
    private Animator _animator;

    public AudioClip[] WalkingAudioClips;
    public bool Rushing;
    private AudioSource _audioSource;
    private float _timeSincePlayedWalkSound;
    private bool _dead;

    private void Awake()
    {
        _originalScale = transform.localScale;
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponentInParent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_dead) return;

        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if (horizontal > 0 || horizontal < -0 || vertical < -0 || vertical > 0)
        {
            var velocity = GetVelocity();
            var movement = new Vector2(
                horizontal * Time.deltaTime * velocity,
                vertical * Time.deltaTime * velocity
            );
            _body.AddForce(
                movement,
                ForceMode2D.Force
            );
            _animator.SetTrigger("Walk");

            if (Math.Abs(horizontal) > 0)
            {
                var direction = Math.Sign(horizontal);
                transform.localScale = new Vector3(_originalScale.x * direction, _originalScale.y, _originalScale.z);
            }

            PlayWalkingSound();
        }
        else
        {
            _timeSincePlayedWalkSound = 0;
            _animator.SetTrigger("Idle");
        }
    }

    private void PlayWalkingSound()
    {
        if (_timeSincePlayedWalkSound > .5f && Random.value > .3f)
        {
            _timeSincePlayedWalkSound = 0;
            
            _audioSource.clip = GetRandomWalkingClip();
            _audioSource.volume = Random.Range(0.05f, .1f);
            _audioSource.Play();
        }

        _timeSincePlayedWalkSound += Time.deltaTime;
    }

    private AudioClip GetRandomWalkingClip()
    {
        return WalkingAudioClips[Random.Range(0, WalkingAudioClips.Length)];
    }

    public void Die()
    {
        _animator.SetTrigger("Die");
        _body.velocity = Vector2.zero;
        _dead = true;
    }

    private int GetVelocity()
    {
        if (Rushing)
        {
            return 500;
        }

        return 260;
    }
}