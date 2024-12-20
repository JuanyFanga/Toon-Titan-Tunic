using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerBoard : MonoBehaviour
{
    private float timer;
    [SerializeField] private float _gameTime = 5;
    public float GameTime() { return _gameTime; }
    private PhotonView _pv;
    [SerializeField] private TextMeshProUGUI timerText;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _pv = GetComponent<PhotonView>();
    }

    public void StartTimerCallRPC()
    {
        _pv.RPC("StartTimer", RpcTarget.All);
    }

    [PunRPC]
    public void StartTimer()
    {
        ResetTimer();
        InvokeRepeating("ElapseSecond",1,1);
    }

    private void ElapseSecond()
    {
        timer -= 1;

        timerText.text = ((int)timer).ToString();

        if (timer <= 0)
        {
            timer = 0;
        }
    }

    public void ResetTimer()
    {
        timer = _gameTime;
        StopTimer();
    }

    public void StopTimer()
    {
        CancelInvoke("ElapseSecond");
    }
}
