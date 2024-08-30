using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    IPlayer _player;
    [SerializeField] private int count = 0;

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

    void Update()
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

    private void OnTriggerEnter(Collider other)
    {
        if (_pv.IsMine && other.transform.CompareTag("Coin"))
        {
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("CollectCoin", RpcTarget.AllBuffered, other.gameObject.GetComponent<PhotonView>().ViewID);
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
}