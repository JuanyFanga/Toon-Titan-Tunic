using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    IPlayer _player;

    private PhotonView _pv;
    private Camera _camera;

    private void Awake()
    {
        _player = GetComponent<IPlayer>();
        _pv = GetComponent<PhotonView>();
        _camera = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        _camera.gameObject.SetActive(_pv.IsMine);
    }

    void FixedUpdate()
    {
        if (_pv.IsMine)
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            Vector3 dir = new Vector3(h, 0, v);

            if (h == 0 && v == 0)
                _player.Move(Vector3.zero);

            else
            {
                _player.Move(dir.normalized);
                _player.Look(dir);
            }
        }
    }
}