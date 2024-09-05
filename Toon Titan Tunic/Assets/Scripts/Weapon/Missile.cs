using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private bool _canExplode;
    [SerializeField] private float _speed;
    [SerializeField] float bounceFactor = 0.08f;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _canExplode = true;
    }

    private void Update()
    {
        if (_canExplode)
        {
            //transform.Translate(Vector3.forward * _speed * Time.deltaTime); ;
            _rb.velocity = transform.forward * _speed;
        }
        else
        {
            transform.position = Vector3.zero;
        }

        Debug.Log($"Velocity is {_rb.velocity}");

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision is {collision.gameObject.name}");

        if (_canExplode && collision.collider.CompareTag("Player")) 
        {
            var hitPlayer = collision.gameObject.GetComponent<IPlayer>();
            hitPlayer.Die();
        }

        if (_canExplode && !collision.collider.CompareTag("Player"))
        {
            Vector3 normal = collision.contacts[0].normal;

            Vector3 incomingVelocity = _rb.velocity;
            Vector3 reflectedVelocity = Vector3.Reflect(incomingVelocity, normal);

            _rb.velocity = reflectedVelocity * bounceFactor;

            print("soy gaymer");
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