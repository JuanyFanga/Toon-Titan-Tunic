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
    
    private PhotonView _pv;
    private Camera _camera;
    private Vector3 _direction;
    private int _playerID;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        _player = GetComponent<IPlayer>();
        _pv = GetComponent<PhotonView>();
        _camera = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        _camera.gameObject.SetActive(_pv.IsMine);
        if (PhotonNetwork.IsMasterClient)
        {
            GameManager.Instance.AddPlayerController(this);
        }
    }

    void Update()
    {
        if (_pv.IsMine)
        {
            if (transform.GetChild(0).gameObject.activeInHierarchy)
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

    public void InitPlayerController(int playerID)
    {
        _playerID = playerID;
    }

    public void SendDeathToManager()
    {
        GameManager.Instance.AddPointToPlayer(2);
    }

    public void Reset()
    {
        _pv.RPC("RPCReset", RpcTarget.All);
    }
    [PunRPC]
    public void RPCReset()
    {
        LevelReseter reseter = FindAnyObjectByType<LevelReseter>();
        transform.SetPositionAndRotation(reseter._resetLocations[ID-1].position, reseter._resetLocations[ID-1].rotation);
        transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<IPlayer>().PickupBullet();

    }
}