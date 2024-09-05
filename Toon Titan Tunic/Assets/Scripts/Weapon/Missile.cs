using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private bool _canExplode;
    [SerializeField] private float _speed;

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
    }
    private void OnCollisionEnter(Collision collision)
    {

    }

}
