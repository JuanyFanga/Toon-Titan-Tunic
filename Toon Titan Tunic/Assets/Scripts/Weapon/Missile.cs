using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private bool _canExplode;
    [SerializeField] private float _speed;
    [SerializeField] float bounceFactor = 1f;
    private Vector3 _velocity;
    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _canExplode = true;
            _velocity = Vector3.forward * _speed;
        }
    }

    private void FixedUpdate()
    {
        if (_canExplode)
        {  
            transform.position += _velocity * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (_canExplode && collision.collider.CompareTag("Player"))
            {
                var hitPlayer = collision.gameObject.GetComponent<IPlayer>();
                hitPlayer.Die();
            }
            else if (_canExplode && !collision.collider.CompareTag("Player"))
            {
                Vector3 normal = collision.contacts[0].normal;

                Vector3 reflectedVelocity = Vector3.Reflect(_velocity, normal);
                _velocity.y = 0;
                _velocity = reflectedVelocity;
                transform.forward = _velocity.normalized;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_canExplode && other.CompareTag("Player")) 
        {
            var hitPlayerWeapon = other.GetComponent<IWeapon>();
            hitPlayerWeapon.Reload();
        }
    }
}