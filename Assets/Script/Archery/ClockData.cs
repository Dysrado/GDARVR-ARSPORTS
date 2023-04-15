using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockData : MonoBehaviour
{
    [Header("Clock Information")]
    [SerializeField] private TextMeshProUGUI roundNum;
    [SerializeField] private TextMeshProUGUI timerData;

    public void SetRoundNum(string Description, int count)
    {
        roundNum.text = "Round: " + count.ToString(); // replace the "Round: " with Description after defining properly;
    }

    public void SetTimerData (int seconds)//seconds 11
    {
        int minutes = seconds / 60;
        int largeSeconds = seconds % 60;
        int smallSeconds = largeSeconds % 10;
        
        if (largeSeconds < 10)
            timerData.text = "00:0" + largeSeconds.ToString(); 

        else if (minutes == 0)
            timerData.text = "00:" + largeSeconds.ToString();

        else
            timerData.text =  "0" + minutes.ToString() + ":" + largeSeconds.ToString();

    }

    public void ResetTimer()
    {
        timerData.text = "00:00";
    }

    public void ResetClock()
    {
        roundNum.text = "Round: 0";
        timerData.text = "00:00:";
    }
}
