using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    //hacer una lista con varias posiciones y poder elegir entre esas posiciones

    [SerializeField] private Material redMaterial;
    [SerializeField] private Material blueMaterial;

    GameObject player;
    private PhotonView _pv;

    private void Awake()
    {
        _pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        player = PhotonNetwork.Instantiate(playerPrefab.name,
            new Vector3(Random.Range(-2, 2), 0.25f, Random.Range(-2, 2)),
            Quaternion.identity);

        int playerIndex = PhotonNetwork.PlayerList.Length;
        _pv.RPC("ChangeColor", RpcTarget.AllBuffered, player.GetComponent<PhotonView>().ViewID, playerIndex);
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

    [PunRPC]
    private void ChangeColor(int playerViewID, int playerIndex)
    {
        PhotonView targetPhotonView = PhotonView.Find(playerViewID);

        if (targetPhotonView != null)
        {
            targetPhotonView.gameObject.GetComponentInChildren<MeshRenderer>().material = (playerIndex == 1) ? redMaterial : blueMaterial;
        }
    }
}