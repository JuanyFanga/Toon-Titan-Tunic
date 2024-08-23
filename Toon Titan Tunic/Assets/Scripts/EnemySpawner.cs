using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float timeToStartSpawning;
    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private GameObject enemyPrefab;

    private bool readyToSpawn;
    private float timer;
    private bool matchStarted;

    private void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            matchStarted = true;

            if (PhotonNetwork.IsMasterClient)
            {
                timer += Time.deltaTime;

                if (!readyToSpawn && timeToStartSpawning < timer)
                {
                    readyToSpawn = true;
                    timer = 0;
                }

                if (readyToSpawn && timer > timeBetweenSpawn)
                {
                    timer = 0;
                    PhotonNetwork.Instantiate(enemyPrefab.name,
                    new Vector3(Random.Range(-2, 2), 1, Random.Range(-2, 2)),
                    Quaternion.identity);
                }
            }
        }

        else if (PhotonNetwork.CurrentRoom.PlayerCount < 2 && matchStarted)
        {
            //Borrar lista de enemigos
            Debug.Log("Se desconectó el bro");
        }
    }
}