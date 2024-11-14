using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSystem : MonoBehaviour
{
    [SerializeField] private GameObject _meteorPrefab;
    private PhotonView _pv;

    [SerializeField] private Vector2 meteorZone;
    TimerBoard _timerBoard;

    private void Awake()
    {
        _timerBoard = FindObjectOfType<TimerBoard>();
        _pv = GetComponent<PhotonView>();
        _pv.ViewID = 873;
    }

    private void Start()
    {
        //Empieza a contar cuando entran ambos jugadores
        if (!PhotonNetwork.IsMasterClient)
        {
            _pv.RPC("StartMeteors", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    private void StartMeteors()
    {
        InvokeRepeating("SpawnMeteors", _timerBoard.GameTime(), 1);
    }

    private void SpawnMeteors()
    {
        PhotonNetwork.Instantiate(_meteorPrefab.name,
            new Vector3(transform.position.x + Random.Range( - meteorZone.x / 2, meteorZone.x / 2), 
            10f,
            transform.position.z + Random.Range(-meteorZone.y / 2, meteorZone.y / 2)),
            Quaternion.identity);
    }

    private float GetInvokeTiming()
    {
        float newTime = 3;

        newTime -= 0.5f;

        if (newTime <= 1)
            newTime = 1;

        return newTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(meteorZone.x, 2.0f, meteorZone.y));
    }
}
