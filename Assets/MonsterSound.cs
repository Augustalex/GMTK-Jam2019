using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSound : MonoBehaviour
{
    public AudioClip LoudScream;
    public AudioClip LoudScreamFar;
    public AudioClip SilentScream;
    public AudioClip SilentScreamFar;
    public AudioClip BaitedScream;
    public AudioClip BaitedScreamFar;

    private AudioSource _audioSource;
    private EnemyMovement _enemyMovement;
    private GameObject _player;
    private float _timeSinceSilentScream = 10;
    private const float MinSilentScreamTime = 15; 

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _player = GameObject.FindWithTag("Player");

        _enemyMovement.statusChanged += EnemyStatusChanged;
    }

    private void EnemyStatusChanged(EnemyMovement.Status status)
    {
        if (status.Equals(EnemyMovement.Status.Idle)) return;

        _audioSource.clip = GetAudioClipForMovementStatus(status);
        _audioSource.volume = 1;
        _audioSource.Play();
    }

    private AudioClip GetAudioClipForMovementStatus(EnemyMovement.Status status)
    {
        if (status.Equals(EnemyMovement.Status.Baited))
        {
            return PlayerIsFarAway() ? BaitedScreamFar : BaitedScream;
        }

        return PlayerIsFarAway() ? LoudScreamFar : LoudScream;
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyMovement.Idle && _timeSinceSilentScream > MinSilentScreamTime && Random.value < .1f)
        {
            _timeSinceSilentScream = 0;
            
            _audioSource.clip = PlayerIsFarAway() ? SilentScreamFar : SilentScream;
            _audioSource.volume = 1;
            _audioSource.Play();
        }

        _timeSinceSilentScream += Time.deltaTime;
    }
    
    private bool PlayerIsFarAway()
    {
        return Vector3.Distance(transform.position, _player.transform.position) > 10;
    }
}