using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public UnityEvent OnStop;
    [SerializeField] private TextMeshProUGUI timerText;
    private bool istiming = true;
    public bool isTiming { private get { return istiming; } set { istiming = value; } }
    [SerializeField] private TMP_ColorGradient colorGrad;
    private float maxDuration = 10;
    private float colorT;
    private float seconds = 10;
    private float miliseconds = 0;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (isTiming)
        {
            UpdateColor();
            if (TimerTick())
            {
                OnStop?.Invoke();
                gameManager.endGame?.Invoke();
                isTiming = false;
            }
        }
    }

    private bool TimerTick()
    {
        if (miliseconds <= 0)
        {
            if (seconds <= 0)
            {
                seconds = 59;
            } else if (seconds >= 0)
            {
                seconds--;
            }

            miliseconds = 100;
        }

        miliseconds -= Time.deltaTime * 100;


        timerText.text = string.Format("{0}.{1}", seconds, (int)miliseconds);
        if (seconds <= 0 && miliseconds <= 0)
        {
            return true;
        }
        return false;
    }

    private void UpdateColor()
    {
        timerText.color = Color.Lerp(colorGrad.topLeft, colorGrad.bottomLeft, colorT);
        if (colorT < 1)
        {
            colorT += Time.deltaTime / maxDuration;
        }
    }
}
