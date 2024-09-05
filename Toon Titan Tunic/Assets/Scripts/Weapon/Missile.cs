using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private bool _canExplode;
    [SerializeField] private float _speed;
    [SerializeField] float bounceFactor = 0.08f;

    private void Awake()
    {
        _canExplode = true;
    }

    private void Update()
    {
        if (_canExplode)
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime); ;
        }
        else
        {
            transform.position = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_canExplode && collision.collider.CompareTag("Player")) 
        {
            var hitPlayer = collision.gameObject.GetComponent<IPlayer>();
            hitPlayer.Die();
        }

        if (_canExplode && !collision.collider.CompareTag("Player"))
        {
            Vector3 normal = collision.contacts[0].normal;

            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 incomingVelocity = rb.velocity;
            Vector3 reflectedVelocity = Vector3.Reflect(incomingVelocity, normal);

            rb.velocity = reflectedVelocity * bounceFactor;
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