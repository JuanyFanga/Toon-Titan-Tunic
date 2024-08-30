using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _coins;
    [SerializeField] private List<PlayerController> _player;
    [SerializeField] private PhotonView _pv;
    public static GameManager Instance;

    private int count = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
            
    }

    private void Update()
    {

    }

    [PunRPC]
    private void CollectCoin()
    {
        count++;
    }
}