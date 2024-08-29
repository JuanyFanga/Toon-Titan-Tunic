using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _coins;
    [SerializeField] private List<PlayerController> _player;
    [SerializeField] private PhotonView _pv;

    private int count = 0;

    private void Update()
    {
        foreach()
        {

        }
    }

    [PunRPC]
    private void CollectCoin()
    {
        count++;
    }
}