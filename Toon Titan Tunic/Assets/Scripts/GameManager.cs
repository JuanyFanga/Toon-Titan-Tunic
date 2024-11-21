using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnLevelReseted;

    public List<PlayerController> _playerControllers;
    [SerializeField] private PhotonView _pv;
    [SerializeField] private TimerBoard _board;
    public List<Missile> spawnedMissiles;
    public static GameManager Instance;

    public LevelsManager _levelsManager;

    private int player1Points = 0;
    private int player2Points = 0;

    [Header("Points Panel")]
    [SerializeField] private GameObject _pointPanel;
    [SerializeField] private TextMeshProUGUI _player1PointsTexts;
    [SerializeField] private TextMeshProUGUI _player2PointsTexts;

    [Header("Match End Panel")]
    [SerializeField] private GameObject _matchEndPanel;
    [SerializeField] private TextMeshProUGUI _matchEndplayer1PointsTexts;
    [SerializeField] private TextMeshProUGUI _matchEndplayer2PointsTexts;
    [SerializeField] private TextMeshProUGUI _playerWin;

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

        _player1PointsTexts.text = player1Points.ToString();
        _player2PointsTexts.text = player2Points.ToString();
        _matchEndplayer1PointsTexts.text = player1Points.ToString();
        _matchEndplayer2PointsTexts.text = player2Points.ToString();

        if (player1Points >= 5 || player2Points >= 5)
        {
            MatchEnd(player2Points >= 5 ? 2 : 1);
            return;
        }

        _pointPanel.SetActive(true);
        Invoke("CloseScoreBoard", 4.75f);
    }

    public void MatchEnd(int winnerPlayer)
    {
        _pointPanel.SetActive(false);
        _playerWin.text = $"Player {winnerPlayer} won";
        _matchEndPanel.SetActive(true);
        Invoke("ReturnToMainMenu", 3f);
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void AddPointToPlayer(int playerID)
    {
        _pv.RPC("AddPoint", RpcTarget.AllBuffered, playerID);
        Invoke("NextRound", 5);
    }

    public void CloseScoreBoards()
    {
        _pv.RPC("CloseScoreBoardsRPC", RpcTarget.All);
    }

    [PunRPC]
    public void CloseScoreBoardsRPC()
    {
        _pointPanel.SetActive(false);
    }

    public void CloseScoreBoard(int playerID)
    {
        _pointPanel.SetActive(false);
    }

 
    public void ResetLevel()
    {
        _pv.RPC("CloseScoreBoardsRPC", RpcTarget.All);

        var controllers = _playerControllers;
        foreach (var controller in controllers)
        {
            controller.Reset();
        }


        if (OnLevelReseted != null)
        {
            OnLevelReseted?.Invoke();
            OnLevelReseted.RemoveAllListeners();
        }
    }

    public void ResetTimer()
    {
        _board.StartTimerCallRPC();
    }

    [PunRPC]
    private void ResetTimerRPC()
    {
        _board.StartTimerCallRPC();
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