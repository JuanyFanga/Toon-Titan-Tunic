using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopiedPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] AudioClip _dashSFX;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource.PlayOneShot(_dashSFX);
    }
}
