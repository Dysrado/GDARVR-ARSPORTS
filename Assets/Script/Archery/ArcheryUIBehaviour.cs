using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class ArcheryUIBehaviour : MonoBehaviour
{
    public enum Player
    {
        First = 0,
        Second = 1
    }
    public static ArcheryUIBehaviour Instance;


    [Header("ArcheryData")]
    [SerializeField] private ArcheryPlayerData player1Data;
    [SerializeField] private ArcheryPlayerData player2Data;
    [SerializeField] private ClockData clockInfo;

    [Header("External ReferenceTab")]
    [SerializeField] private GameObject winnerTab;
    [SerializeField] private TextMeshProUGUI winnerDescription;
    [SerializeField] private GameObject warningTab;
    [SerializeField] private GameObject debugTab;

    //PrivateData
    private GameObject activeTab = null;
    private bool isCountdownActive = false;
    protected int timeInSeconds = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    //Placeholder Function: If the develepor decide to customize the name.
    private void SetNames()
    {
        //Implement the logic to get the logic from player prefs function.
    }

    //One update per frame
    private void Update()
    {
        if (isCountdownActive)
            clockInfo.SetTimerData(timeInSeconds);
            
    }

    
    //Vuforia Detections
    public void NoImageTargetFound()
    {
        warningTab.SetActive(true);
        Time.timeScale = 0;
        //It should stop any pending activity
    }

    public void ImageTargetFound()
    {
        warningTab.SetActive(false);
        Time.timeScale = 1;
        //REsume activity
    }

    //Game Logic

    public void ActivateTimer()
    {
        isCountdownActive = true;
    }

    public void DisabledTimer()
    {
        isCountdownActive = false;
    }

    public void SetTime(int seconds)
    {
        timeInSeconds = seconds;
    }

    //Player Logic

    public void UpdatePlayerScore(Player currPlayer, int points)
    {
        if(currPlayer == Player.First)
        {
            player1Data.SetScore(points);
        }

        else
        {
            player2Data.SetScore(points);
        }
    }

    public void UpdateSetScore(Player playerSetWinner, int currSetWins)
    {
        if (playerSetWinner == Player.First)
        {
            switch (currSetWins)
            {
                case 1:
                    player1Data.TickFirstSet();
                    break;

                case 2:
                    player1Data.TickSecondSet();
                    break;

                default:

                    Debug.LogError("Number Don't Exist");
                    break;
            }
           
        }

        else
        {
            switch (currSetWins)
            {
                case 1:
                    player2Data.TickFirstSet();
                    break;

                case 2:
                    player2Data.TickSecondSet();
                    break;

                default:

                    Debug.LogError("Number Don't Exist");
                    break;
            }
        }
    }


    public void SwitchPlayer(Player activePlayer)
    {
        if (activePlayer == Player.First)
        {
            player1Data.RemovePlayerTurn();
            player2Data.SetPlayerTurn();
        }

        else
        {
            player1Data.SetPlayerTurn();
            player2Data.RemovePlayerTurn();
        }
    }


    // End Game
    public void SetReset()
    {
        player1Data.ResetStats();
        player2Data.ResetStats();
        clockInfo.ResetTimer();
    }

    public void EndGame(Player playerWinner)
    {
        winnerTab.SetActive(true);
        if (playerWinner == Player.First)
        {
            winnerDescription.text = player1Data.GetName() + "/n wins";
        }

        else
        {
            winnerDescription.text = player2Data.GetName() + "/n wins";
        }
    }


}
