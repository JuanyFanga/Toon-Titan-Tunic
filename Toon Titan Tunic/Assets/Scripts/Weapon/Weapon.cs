using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private bool _hasBullet;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _hasBullet)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Instantiate(_bullet, _weaponPoint.position, Quaternion.identity);
    }

    public void Reload()
    {
         _hasBullet = true;
    }

    public bool HasBullet()
    {
        return _hasBullet;
    }
}
