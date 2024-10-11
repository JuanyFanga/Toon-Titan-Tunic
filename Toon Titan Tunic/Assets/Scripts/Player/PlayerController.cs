using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public int ID => _playerID;
    IPlayer _player;
    [SerializeField] private int count = 0;
    
    private PhotonView _pv;
    private Camera _camera;
    private Vector3 _direction;
    private int _playerID;

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

    void Update()
    {
        if (_pv.IsMine)
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            _direction = new Vector3(h, 0, v);

            if (h == 0 && v == 0)
                _player.Move(Vector3.zero);

            else
            {
                _player.Look(_direction);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _player.Dash();
            }
        }
    }

    private void FixedUpdate()
    {
        if (_pv.IsMine)
        {
            if (!(_direction.x == 0 && _direction.z == 0))
            {
                _player.Move(_direction.normalized);
            }
        }
    }

    [PunRPC]
    void CollectCoin(int coinViewID)
    {
        PhotonView coinPhotonView = PhotonView.Find(coinViewID);

        if (coinPhotonView != null)
        {
            PhotonNetwork.Destroy(coinPhotonView.gameObject);
            //GameManager.Instance.AddCoinToPool();
        }
    }

    public void InitPlayerController(int playerID)
    {
        _playerID = playerID;
    }
 
}