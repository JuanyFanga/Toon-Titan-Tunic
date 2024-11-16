using UnityEngine;
using Photon.Pun;


public class PlayerReset : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    //hacer una lista con varias posiciones y poder elegir entre esas posiciones

    [SerializeField] private Material redMaterial;
    [SerializeField] private Material blueMaterial;
    [SerializeField] private Transform[] playerSpawnpoint;
    private GameObject _timerPanel;

    GameObject player;
    private PhotonView _pv;

    private void Awake()
    {
        _pv = GetComponent<PhotonView>();
        _timerPanel = GameObject.FindGameObjectWithTag("TimerPanel").gameObject;
        _timerPanel.SetActive(true);
    }

    private void Start()
    {
        player = PhotonNetwork.Instantiate(playerPrefab.name,
        new Vector3(Random.Range(-2, 2), 0.25f, Random.Range(-2, 2)),
        Quaternion.identity);

        int playerIndex = PhotonNetwork.PlayerList.Length;
        _pv.RPC("ChangeColor", RpcTarget.AllBuffered, player.GetComponent<PhotonView>().ViewID, playerIndex);


        _timerPanel.GetComponent<TimerBoard>().ResetTimer();
    }

    

    [PunRPC]
    private void ChangeColor(int playerViewID, int playerIndex)
    {
        PhotonView targetPhotonView = PhotonView.Find(playerViewID);

        if (targetPhotonView != null)
        {
            targetPhotonView.gameObject.GetComponentInChildren<MeshRenderer>().material = (playerIndex == 1) ? redMaterial : blueMaterial;
            targetPhotonView.gameObject.GetComponent<PlayerController>().InitPlayerController(playerIndex);
        }
    }
}