using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public float timeRemaining = 1;
    public bool timerIsRunning = false;
    public Text timeText;
    private GameObject detectTextTimer;
    public static Timer instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
    

        timerIsRunning = true;
    }

    void Update()
    {

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining += Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }





    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public void findTextTimer()
    {
        detectTextTimer = GameObject.Find("Timer");
        timeText = detectTextTimer.GetComponent<Text>();
        timeRemaining = 1;



    }
}
