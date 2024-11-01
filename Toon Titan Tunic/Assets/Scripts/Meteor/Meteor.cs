using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] float _velocity;
    private Rigidbody _rb;
    private Vector3 _dir = Vector3.zero;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _dir *= _velocity;
        _dir.y = _rb.velocity.y;

        _rb.velocity = _dir;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        //Explota todo con un SphereOverlap
    }
}
