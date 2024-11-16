using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<PlayerController> _playerControllers;
    [SerializeField] private PhotonView _pv;
    [SerializeField] private GameObject _pointPanel;
    [SerializeField] private TextMeshProUGUI _player1PointsTexts;
    [SerializeField] private TextMeshProUGUI _player2PointsTexts;

    public static GameManager Instance;

    public LevelsManager _levelsManager;

    private int player1Points = 0;
    private int player2Points = 0;

    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
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
    }

    [PunRPC]
    public void AddPoint(int playerID)
    {
        if (playerID == 1)
        {
            player1Points++;
        }
        else
        {
            player2Points++;
        }
        if (player1Points >= 5 || player2Points >= 5)
        {
            MatchEnd();
            return;
        }

        _player1PointsTexts.text = player1Points.ToString();
        _player2PointsTexts.text = player2Points.ToString();
        _pointPanel.SetActive(true);
        Invoke("CloseScoreBoard", 4.75f);
    }

    public void MatchEnd()
    {

    }

    public void AddPointToPlayer(int playerID)
    {
        _pv.RPC("AddPoint", RpcTarget.AllBuffered, playerID);
        Invoke("NextRound", 5);
    }

    public void CloseScoreBoard(int playerID)
    {
        _pointPanel.SetActive(false);
    }

    public void AddPlayerController(PlayerController playerToAdd)
    { 
        _playerControllers.Add(playerToAdd);    
    }
    public void NextRound()
    {
        _levelsManager.LoadNextLevel();
    }
}