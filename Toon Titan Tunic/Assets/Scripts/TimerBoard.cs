using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerBoard : MonoBehaviour
{
    private float timer;
    [SerializeField] private float gameTime = 20;
    [SerializeField] private TextMeshProUGUI timerText;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartTimer();
        }
    }

    public void StartTimer()
    {
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
        timer = gameTime;
    }

    public void StopTimer()
    {
        CancelInvoke("ElapseSecond");
    }
}
