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
    private PhotonView _pv;
    private void Awake()
    {
        _pv = GetComponent<PhotonView>();
    }
    private void Update()
    {

        if (_pv.IsMine)
        {
            if (Input.GetMouseButtonDown(0))// && _hasBullet)
            {
                Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
                Vector2 mousePosition = Input.mousePosition;
                Vector2 direction = mousePosition - screenCenter;
                direction.Normalize();
                //direction.y = 0;
                float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

                transform.GetChild(0).rotation = Quaternion.Euler(-90, angle, 0);
                Shoot(Quaternion.Euler(0, angle, 0));
            }
        }
    }

    public void Shoot(Quaternion spawnRot)
    {
        GameObject projectile = PhotonNetwork.Instantiate(_bullet.name,
            _shootPoint.position,
            spawnRot);

        Instantiate(_fireParticle, _shootPoint.position, _shootPoint.rotation);
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
