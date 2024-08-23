using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    //hacer una lista con varias posiciones y poder elegir entre esas posiciones
    
    GameObject player;
    private PhotonView _pv;

    private void Awake()
    {
        _pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        player = PhotonNetwork.Instantiate(playerPrefab.name,
            new Vector3(Random.Range(-2, 2), 1, Random.Range(-2, 2)),
            Quaternion.identity);
    }

    //[PunRPC]
    //private void SetColorToPlayers()
    //{
    //    foreach (KeyValuePair<int, Photon.Realtime.Player> PlayersData in PhotonNetwork.CurrentRoom.Players)
    //    {
    //        switch (PlayersData.Value.UserId)
    //        {
                
    //        }
    //    }
    //}

}