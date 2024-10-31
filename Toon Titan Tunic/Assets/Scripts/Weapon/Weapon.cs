using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private bool _hasBullet = true;
    [SerializeField] private ParticleSystem _fireParticle;
    [SerializeField] private Camera _camera;
    private PhotonView _pv;
    private void Awake()
    {
        _pv = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (_pv.IsMine)
        {
            if (Input.GetMouseButtonDown(0) && _hasBullet)
            {
                Shoot();
                _hasBullet = false;
            }
        }
    }

    public void Shoot()
    {
        GameObject projectile = PhotonNetwork.Instantiate(
            _bullet.name,
            _shootPoint.position,
            PlayerAim()
        );

        projectile.GetComponent<IMissile>().Initialize(GetComponent<PlayerController>().ID);

        Instantiate(_fireParticle, _shootPoint.position, _shootPoint.rotation, _shootPoint);
    }

    public Quaternion PlayerAim()
    {
        Vector3 positionOnScreen = _camera.WorldToViewportPoint(transform.position);

        Vector3 mouseOnScreen = _camera.ScreenToViewportPoint(Input.mousePosition);

        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        return Quaternion.Euler(new Vector3(0f, -angle, 0f));
    }
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2((a.x - b.x) * 16, -(a.y - b.y) * 9) * Mathf.Rad2Deg;
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
