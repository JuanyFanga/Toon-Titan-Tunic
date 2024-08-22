using UnityEngine;
using Photon.Pun;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    //hacer una lista con varias posiciones y poder elegir entre esas posiciones

    private void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, 
            new Vector3(Random.Range(-2, 2), 1, Random.Range(-2,2)), 
            Quaternion.identity);
    }
}
