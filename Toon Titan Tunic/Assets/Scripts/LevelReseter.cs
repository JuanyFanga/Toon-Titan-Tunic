using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelReseter : MonoBehaviour
{
    private PhotonView _pv;
    public Transform[] _resetLocations;
    private void Awake()
    {
        _pv = GetComponent<PhotonView>();
    }
    private void Start()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            GameManager.Instance.CloseScoreBoards();

            var controllers = GameManager.Instance._playerControllers;
            foreach (var controller in controllers)
            {
                controller.Reset();
            }

            GameManager.Instance.DestroyAllMissiles();
        }
        else
        {
            _pv.RPC("ResetTimerAsMasterClient", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    private void ResetTimerAsMasterClient()
    {
        GameManager.Instance.ResetTimer();
    }
}
