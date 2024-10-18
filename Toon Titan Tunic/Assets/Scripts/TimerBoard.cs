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

    void Update()
    {
        timer -= Time.deltaTime;

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
}
