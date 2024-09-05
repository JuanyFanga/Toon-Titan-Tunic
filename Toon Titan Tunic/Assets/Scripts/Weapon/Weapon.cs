using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private bool _hasBullet = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _hasBullet)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject projectile = PhotonNetwork.Instantiate(_bullet.name,
            _shootPoint.position,
            _shootPoint.rotation);
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
