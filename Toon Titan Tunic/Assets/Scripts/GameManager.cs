using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<PlayerController> _player;
    [SerializeField] private PhotonView _pv;
    [SerializeField] private GameObject _pointPanel;
    [SerializeField] private TextMeshProUGUI _player1PointsTexts;
    [SerializeField] private TextMeshProUGUI _player2PointsTexts;

    public static GameManager Instance;

    public LevelsManager _levelsManager;

    private int player1Points = 0;
    private int player2Points = 0;

    private int playersReseted;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _pointPanel.SetActive(false);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }  
        _pv = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void AddPoint(int playerID)
    {
        if (playerID == 0)
        {
            player1Points++;
        }
        else
        {
            player2Points++;
        }

        _player1PointsTexts.text = player1Points.ToString();
        _player2PointsTexts.text = player2Points.ToString();
        _pointPanel.SetActive(true);

        Debug.Log($"Player1 points are: {player1Points}");
        Debug.Log($"Player2 points are: {player2Points}");
    }

    public void AddPointToPlayer(int playerID)
    {
        _pv.RPC("AddPoint", RpcTarget.All, playerID);
        Invoke("NextRound", 5);
    }

    public void NextRound()
    {
        playersReseted = 0;
        _levelsManager.NextRound();
    }

    public bool ArePlayersAlreadySpawned()
    {
        return _player.Count > 1;
    }

    public void ResetPlayer(Vector3 location)
    {
        _player[playersReseted].transform.position = location;
        playersReseted++;
    }
}