using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    Rigidbody _rb;
    Animator _anim;

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody>();
        _anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _anim.SetFloat("Vel", _rb.velocity.magnitude);
    }


}
