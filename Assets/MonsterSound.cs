using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSound : MonoBehaviour
{
    public AudioClip LoudScream;
    public AudioClip SilentScream;
    public AudioClip BaitedScream;

    private AudioSource _audioSource;
    private EnemyMovement _enemyMovement;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _enemyMovement = GetComponent<EnemyMovement>();

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
            return BaitedScream;
        }

        return LoudScream;
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyMovement.Idle && Random.value < .0005f)
        {
            _audioSource.clip = SilentScream;
            _audioSource.volume = .35f;
            _audioSource.Play();
        }
    }
}