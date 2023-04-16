using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

static class Constant
{
    public const string SPARE = "/";
    public const string STRIKE = "X";
    public const string MISS = "-";
}


public class BowlingUIBehaviour : MonoBehaviour
{
    public enum Player
    {
        First = 0,
        Second = 1
    }


    [Header("Bowling Data")]
    [SerializeField] private float maxSetSize;
    [SerializeField] private GameObject player1FrameHolder;
    [SerializeField] private GameObject player2FrameHolder;
    [SerializeField] private GameObject pinInformation;



    [Header("Bowling GammeObject Reference")]
    [SerializeField] private GameObject normalSetReference;
    [SerializeField] private GameObject lastSetReference;
    [SerializeField] private GameObject totalScoreReference;

    [Header("Bowling Tab Reference")]
    [SerializeField] private GameObject winnerTab;
    [SerializeField] private TextMeshProUGUI winnerText;

    //Private Reference
    private List<GameObject> Player1FrameList;
    private List<GameObject> Player2FrameList;
    private GameObject activePlayerFrame;

    // Reference to Bowling Variables
    public PinSetManager pinSetManager;
    [SerializeField] private BowlingScoreDetector bowlingScoreDetector;
    [SerializeField] private BowlingBallController bowlingBallController;
    [SerializeField] private PinSetSpawner pinSetSpawner;

    // Holds current player varaibles
    Player currentPlayer;
    public int currentFrame;
    public int currentAttempt;
    public string knockedPinsString;
    public int pinsHit;
    public int attempt1PinsHit;
    public float scoreDelay;
    public float switchTime;

    public List<int> P1CumulativeScore; // 0 - attempt 1, 1 - attempt 2, 2 - cumulative score
    public List<int> P2CumulativeScore; // 0 - attempt 1, 1 - attempt 2, 2 - cumulative score
    public int P1CurrentCumulativeIndex;
    public int P2CurrentCumulativeIndex;

    // Start is called before the first frame update
    void Start()
    {
        Player1FrameList = new List<GameObject>();
        Player2FrameList = new List<GameObject>();

        activePlayerFrame = player1FrameHolder;
        activePlayerFrame.SetActive(true);
        currentPlayer = Player.First;
        currentFrame = 0;
        currentAttempt = 1;
        pinsHit = 0;
        P1CurrentCumulativeIndex = 0;
        P2CurrentCumulativeIndex = 0;

        InitializeFrame();
        InitializeCumulativeScore();
        Debug.Log("Value 17: " + P1CumulativeScore[17]);
        

        // Spawn 1st Set of Pins
        bowlingBallController.ResetBallPosition();
        pinSetSpawner.SpawnPins();
    }

    

    void Update()
    {
        if (bowlingScoreDetector.BallPassed)
        {
            StartCoroutine(StartBowlingTurn());
        }

        //MissUpdater();
        SpareStrikeUpdater();

    }

    public void MissUpdater()
    {
        for(int i = 0; i < P1CumulativeScore.Count; i++)
        {
            if(P1CumulativeScore[i] == 12) // Meaning Miss
            {
                for(int j = i + 1; j < P1CumulativeScore.Count; j++)
                {
                    if(P1CumulativeScore[j] != 12 || P1CumulativeScore[j] != 11)
                    {
                        int newNumber = P1CumulativeScore[j];
                        P1CumulativeScore[i] = newNumber;
                        return;
                    }
                }
            }
        }
    }

    public void SpareStrikeUpdater()
    { 
        // 11 - SPARE
        // 10 - STRIKE

        // Player 1 - Spares
        if(P1CumulativeScore[1] == 11) 
        {
            P1CumulativeScore[2] = P1CumulativeScore[0] + (10 - P1CumulativeScore[0]) + P1CumulativeScore[3];
            Player1FrameList[0].GetComponent<BSetData>().UpdateCumultativeScore(P1CumulativeScore[2].ToString());
        }
        if (P1CumulativeScore[4] == 11)
        {
            P1CumulativeScore[5] = P1CumulativeScore[2] + P1CumulativeScore[3] + (10 - P1CumulativeScore[3]) + P1CumulativeScore[6];
            Player1FrameList[1].GetComponent<BSetData>().UpdateCumultativeScore(P1CumulativeScore[5].ToString());
        }
        if (P1CumulativeScore[7] == 11)
        {
            P1CumulativeScore[8] = P1CumulativeScore[5] + P1CumulativeScore[2] + P1CumulativeScore[6] + (10 - P1CumulativeScore[6]) + P1CumulativeScore[9];
            Player1FrameList[2].GetComponent<BSetData>().UpdateCumultativeScore(P1CumulativeScore[8].ToString());
        }
        if (P1CumulativeScore[10] == 11)
        {
            P1CumulativeScore[11] = P1CumulativeScore[8] + P1CumulativeScore[5] + P1CumulativeScore[2] + P1CumulativeScore[9] + (10 - P1CumulativeScore[9]) + P1CumulativeScore[12];
            Player1FrameList[3].GetComponent<BSetData>().UpdateCumultativeScore(P1CumulativeScore[11].ToString());
        }
        if (P1CumulativeScore[13] == 11)
        {
            P1CumulativeScore[14] = P1CumulativeScore[11] + P1CumulativeScore[8] + P1CumulativeScore[5] + P1CumulativeScore[2] + P1CumulativeScore[12] + (10 - P1CumulativeScore[12]) + P1CumulativeScore[15];
            Player1FrameList[4].GetComponent<BSetData>().UpdateCumultativeScore(P1CumulativeScore[14].ToString());
        }

        // Player 1 - Strikes
        if (P1CumulativeScore[12] == 10)
        {
            List<int> next2Values = new List<int>();
            next2Values = CheckNext2ValueIndexesForStrike(P1CumulativeScore, 14);
            P1CumulativeScore[14] = P1CumulativeScore[11] + P1CumulativeScore[8] + P1CumulativeScore[5] + P1CumulativeScore[2] + P1CumulativeScore[12] + P1CumulativeScore[next2Values[0]] + P1CumulativeScore[next2Values[1]];
            Player1FrameList[4].GetComponent<BSetData>().UpdateCumultativeScore(P1CumulativeScore[14].ToString());
        }
        else if (P1CumulativeScore[9] == 10)
        {
            List<int> next2Values = new List<int>();
            next2Values = CheckNext2ValueIndexesForStrike(P1CumulativeScore, 11);
            P1CumulativeScore[11] = P1CumulativeScore[8] + P1CumulativeScore[5] + P1CumulativeScore[2] + P1CumulativeScore[9] + P1CumulativeScore[next2Values[0]] + P1CumulativeScore[next2Values[1]];
            Player1FrameList[3].GetComponent<BSetData>().UpdateCumultativeScore(P1CumulativeScore[11].ToString());
        }
        else if(P1CumulativeScore[6] == 10)
        {
            List<int> next2Values = new List<int>();
            next2Values = CheckNext2ValueIndexesForStrike(P1CumulativeScore, 8);
            P1CumulativeScore[8] = P1CumulativeScore[5] + P1CumulativeScore[2] + P1CumulativeScore[6] + P1CumulativeScore[next2Values[0]] + P1CumulativeScore[next2Values[1]];
            Player1FrameList[2].GetComponent<BSetData>().UpdateCumultativeScore(P1CumulativeScore[8].ToString());
        }
        else if(P1CumulativeScore[3] == 10)
        {
            List<int> next2Values = new List<int>();
            next2Values = CheckNext2ValueIndexesForStrike(P1CumulativeScore, 5);
            P1CumulativeScore[5] = P1CumulativeScore[2] + P1CumulativeScore[3] + P1CumulativeScore[next2Values[0]] + P1CumulativeScore[next2Values[1]];
            Player1FrameList[1].GetComponent<BSetData>().UpdateCumultativeScore(P1CumulativeScore[5].ToString());
        }
        else if(P1CumulativeScore[0] == 10)
        {
            List<int> next2Values = new List<int>();
            next2Values = CheckNext2ValueIndexesForStrike(P1CumulativeScore, 2);
            Debug.Log("Value 1: " + next2Values[0] + ", Value 2: " + next2Values[1]);
            P1CumulativeScore[2] = P1CumulativeScore[0] + P1CumulativeScore[next2Values[0]] + P1CumulativeScore[next2Values[1]];
            Player1FrameList[0].GetComponent<BSetData>().UpdateCumultativeScore(P1CumulativeScore[2].ToString());
        }
      


        // Player 2 - Spares
        if (P2CumulativeScore[1] == 11)
        {
            P2CumulativeScore[2] = P2CumulativeScore[0] + (10 - P2CumulativeScore[0]) + P2CumulativeScore[3];
            Player2FrameList[0].GetComponent<BSetData>().UpdateCumultativeScore(P2CumulativeScore[2].ToString());
        }
        if (P2CumulativeScore[4] == 11)
        {
            P2CumulativeScore[5] = P2CumulativeScore[2] + P2CumulativeScore[3] + (10 - P2CumulativeScore[3]) + P2CumulativeScore[6];
            Player2FrameList[1].GetComponent<BSetData>().UpdateCumultativeScore(P2CumulativeScore[5].ToString());
        }
        if (P2CumulativeScore[7] == 11)
        {
            P2CumulativeScore[8] = P2CumulativeScore[5] + P2CumulativeScore[2] + P2CumulativeScore[6] + (10 - P2CumulativeScore[6]) + P2CumulativeScore[9];
            Player2FrameList[2].GetComponent<BSetData>().UpdateCumultativeScore(P2CumulativeScore[8].ToString());
        }
        if (P2CumulativeScore[10] == 11)
        {
            P2CumulativeScore[11] = P2CumulativeScore[8] + P2CumulativeScore[5] + P2CumulativeScore[2] + P2CumulativeScore[9] + (10 - P2CumulativeScore[9]) + P2CumulativeScore[12];
            Player2FrameList[3].GetComponent<BSetData>().UpdateCumultativeScore(P2CumulativeScore[11].ToString());
        }
        if (P2CumulativeScore[13] == 11)
        {
            P2CumulativeScore[14] = P2CumulativeScore[11] + P2CumulativeScore[8] + P2CumulativeScore[5] + P2CumulativeScore[2] + P2CumulativeScore[12] + (10 - P2CumulativeScore[12]) + P2CumulativeScore[15];
            Player2FrameList[4].GetComponent<BSetData>().UpdateCumultativeScore(P2CumulativeScore[14].ToString());
        }

        // Player 2 - Strikes
        /*
        if (P2CumulativeScore[0] == 10)
        {
            P2CumulativeScore[2] = P2CumulativeScore[0] + CheckNext2ValuesForStrike(P2CumulativeScore, 2);
            Player2FrameList[0].GetComponent<BSetData>().UpdateCumultativeScore(P2CumulativeScore[2].ToString());
        }
        if (P2CumulativeScore[3] == 10)
        {
            P2CumulativeScore[5] = P2CumulativeScore[2] + P2CumulativeScore[3] + CheckNext2ValuesForStrike(P2CumulativeScore, 5);
            Player2FrameList[1].GetComponent<BSetData>().UpdateCumultativeScore(P2CumulativeScore[5].ToString());
        }
        if (P2CumulativeScore[6] == 10)
        {
            P2CumulativeScore[8] = P2CumulativeScore[5] + P2CumulativeScore[2] + P2CumulativeScore[6] + CheckNext2ValuesForStrike(P2CumulativeScore, 8);
            Player2FrameList[2].GetComponent<BSetData>().UpdateCumultativeScore(P2CumulativeScore[8].ToString());
        }
        if (P2CumulativeScore[9] == 10)
        {
            P2CumulativeScore[11] = P2CumulativeScore[8] + P2CumulativeScore[5] + P2CumulativeScore[2] + P2CumulativeScore[9] + CheckNext2ValuesForStrike(P2CumulativeScore, 11);
            Player2FrameList[3].GetComponent<BSetData>().UpdateCumultativeScore(P2CumulativeScore[11].ToString());
        }
        if (P2CumulativeScore[12] == 10)
        {
            P2CumulativeScore[14] = P2CumulativeScore[11] + P2CumulativeScore[8] + P2CumulativeScore[5] + P2CumulativeScore[2] + P2CumulativeScore[12] + CheckNext2ValuesForStrike(P2CumulativeScore, 14);
            Player2FrameList[4].GetComponent<BSetData>().UpdateCumultativeScore(P2CumulativeScore[14].ToString());
        }
        */
    }

    public List<int> CheckNext2ValueIndexesForStrike(List<int> PCumulativeScore, int startIndex)
    {
        List<int> next2Values = new List<int>();

        for (int i = startIndex + 1; i < PCumulativeScore.Count; i++)
        {
            if (PCumulativeScore[i] != 11 || PCumulativeScore[i] != 12)
            {
                next2Values.Add(i);
                for (int j = i + 1; j < PCumulativeScore.Count; j++)
                {
                    if (PCumulativeScore[j] == 11)
                    {
                        next2Values.Add(17);
                        return next2Values;
                    }
                    else if (PCumulativeScore[j] != 12)
                    {
                        next2Values.Add(j);
                        return next2Values;
                    }
                    else
                    {
                        
                    }
                }
            } 
        }
        return next2Values;
    }

    public void UpdateCumulativeScore(string knockedPinsString, Player player, int currentFrame)
    {
        List<GameObject> CopyFrameList = new List<GameObject>();
        switch (currentPlayer)
        {
            case Player.First:
                CopyFrameList = Player1FrameList;
                break;

            case Player.Second:
                CopyFrameList = Player2FrameList;
                break;

            default:
                Debug.LogError("Chosen Player Error for Frame Checking");
                break;
        }

        int cumulativeScore = CheckCumulativeScoreString(knockedPinsString);

        if(player == Player.First)
        {

            if(P1CurrentCumulativeIndex == 1)
            {
                P1CumulativeScore[P1CurrentCumulativeIndex] = cumulativeScore;
                P1CumulativeScore[P1CurrentCumulativeIndex + 1] = P1CumulativeScore[P1CurrentCumulativeIndex] + P1CumulativeScore[P1CurrentCumulativeIndex - 1];
                CopyFrameList[currentFrame].GetComponent<BSetData>().UpdateCumultativeScore(P1CumulativeScore[P1CurrentCumulativeIndex + 1].ToString());
                P1CurrentCumulativeIndex += 2;
            }
            else if(P1CurrentCumulativeIndex == 4)
            {
                P1CumulativeScore[P1CurrentCumulativeIndex] = cumulativeScore;
                P1CumulativeScore[P1CurrentCumulativeIndex + 1] = P1CumulativeScore[2] + P1CumulativeScore[P1CurrentCumulativeIndex] + P1CumulativeScore[P1CurrentCumulativeIndex - 1];
                CopyFrameList[currentFrame].GetComponent<BSetData>().UpdateCumultativeScore(P1CumulativeScore[P1CurrentCumulativeIndex + 1].ToString());
                P1CurrentCumulativeIndex += 2;
            }
            else if (P1CurrentCumulativeIndex == 7)
            {
                P1CumulativeScore[P1CurrentCumulativeIndex] = cumulativeScore;
                P1CumulativeScore[P1CurrentCumulativeIndex + 1] = P1CumulativeScore[2] + P1CumulativeScore[5] + P1CumulativeScore[P1CurrentCumulativeIndex] + P1CumulativeScore[P1CurrentCumulativeIndex - 1];
                CopyFrameList[currentFrame].GetComponent<BSetData>().UpdateCumultativeScore(P1CumulativeScore[P1CurrentCumulativeIndex + 1].ToString());
                P1CurrentCumulativeIndex += 2;
            }
            else if (P1CurrentCumulativeIndex == 10)
            {
                P1CumulativeScore[P1CurrentCumulativeIndex] = cumulativeScore;
                P1CumulativeScore[P1CurrentCumulativeIndex + 1] = P1CumulativeScore[2] + P1CumulativeScore[5] + P1CumulativeScore[8] + P1CumulativeScore[P1CurrentCumulativeIndex] + P1CumulativeScore[P1CurrentCumulativeIndex - 1];
                CopyFrameList[currentFrame].GetComponent<BSetData>().UpdateCumultativeScore(P1CumulativeScore[P1CurrentCumulativeIndex + 1].ToString());
                P1CurrentCumulativeIndex += 2;
            }
            else // essentially for 1st attempts
            {
                P1CumulativeScore[P1CurrentCumulativeIndex] = cumulativeScore;
                P1CurrentCumulativeIndex++;
            }

        }
        else if (player == Player.Second)
        {
            if (P2CurrentCumulativeIndex == 1)
            {
                P2CumulativeScore[P2CurrentCumulativeIndex] = cumulativeScore;
                P2CumulativeScore[P2CurrentCumulativeIndex + 1] = P2CumulativeScore[P2CurrentCumulativeIndex] + P2CumulativeScore[P2CurrentCumulativeIndex - 1];
                CopyFrameList[currentFrame].GetComponent<BSetData>().UpdateCumultativeScore(P2CumulativeScore[P2CurrentCumulativeIndex + 1].ToString());
                P2CurrentCumulativeIndex += 2;
            }   
            else if (P2CurrentCumulativeIndex == 4)
            {
                P2CumulativeScore[P2CurrentCumulativeIndex] = cumulativeScore;
                P2CumulativeScore[P2CurrentCumulativeIndex + 1] = P2CumulativeScore[2] + P2CumulativeScore[P2CurrentCumulativeIndex] + P2CumulativeScore[P2CurrentCumulativeIndex - 1];
                CopyFrameList[currentFrame].GetComponent<BSetData>().UpdateCumultativeScore(P2CumulativeScore[P2CurrentCumulativeIndex + 1].ToString());
                P2CurrentCumulativeIndex += 2;
            }
            else if (P2CurrentCumulativeIndex == 7)
            {
                P2CumulativeScore[P2CurrentCumulativeIndex] = cumulativeScore;
                P2CumulativeScore[P2CurrentCumulativeIndex + 1] = P2CumulativeScore[2] + P2CumulativeScore[5] + P2CumulativeScore[P2CurrentCumulativeIndex] + P2CumulativeScore[P2CurrentCumulativeIndex - 1];
                CopyFrameList[currentFrame].GetComponent<BSetData>().UpdateCumultativeScore(P2CumulativeScore[P2CurrentCumulativeIndex + 1].ToString());
                P2CurrentCumulativeIndex += 2;
            }
            else if (P2CurrentCumulativeIndex == 10)
            {
                P2CumulativeScore[P2CurrentCumulativeIndex] = cumulativeScore;
                P2CumulativeScore[P2CurrentCumulativeIndex + 1] = P2CumulativeScore[2] + P2CumulativeScore[5] + P1CumulativeScore[8] + P2CumulativeScore[P2CurrentCumulativeIndex] + P2CumulativeScore[P2CurrentCumulativeIndex - 1];
                CopyFrameList[currentFrame].GetComponent<BSetData>().UpdateCumultativeScore(P2CumulativeScore[P2CurrentCumulativeIndex + 1].ToString());
                P2CurrentCumulativeIndex += 2;
            }
            else
            {
                P2CumulativeScore[P2CurrentCumulativeIndex] = cumulativeScore;
                P2CurrentCumulativeIndex++;
            }
 
        }
    }

    public int CheckCumulativeScoreString(string knockedPinsString)
    {
        if (knockedPinsString == "1")
        {
            return 1;
        }
        else if (knockedPinsString == "2")
        {
            return 2;
        }
        else if (knockedPinsString == "3")
        {
            return 3;
        }
        else if (knockedPinsString == "4")
        {
            return 4;
        }
        else if (knockedPinsString == "5")
        {
            return 5;
        }
        else if (knockedPinsString == "6")
        {
            return 6;
        }
        else if (knockedPinsString == "7")
        {
            return 7;
        }
        else if (knockedPinsString == "8")
        {
            return 8;
        }
        else if (knockedPinsString == "9")
        {
            return 9;
        }
        else if (knockedPinsString == Constant.STRIKE)
        {
            return 10;
        }
        else if (knockedPinsString == Constant.SPARE)
        {
            return 11;
        }
        else if (knockedPinsString == Constant.MISS)
        {
            return 12;
        }

        return 0;
    }

    IEnumerator StartBowlingTurn()
    {
        bowlingScoreDetector.BallPassed = false;
        yield return new WaitForSeconds(scoreDelay);
        Debug.Log("Check Score Now!");
        pinsHit = pinSetManager.CheckKnockedOverPins();

        // Determine String
        if(pinsHit == 0)
        {
            knockedPinsString = 0.ToString();
        }
        else if ((currentAttempt == 1 && pinsHit == 10) || (currentAttempt == 2 && currentFrame == maxSetSize - 1 && pinsHit == 10) || (currentAttempt == 3 && currentFrame == maxSetSize - 1 && pinsHit == 10))
        {
            knockedPinsString = Constant.STRIKE;
        }
        else if(currentAttempt == 2 && pinsHit == 10 - attempt1PinsHit)
        {
            knockedPinsString = Constant.SPARE;
        }
        else if(currentAttempt == 2)
        {
            knockedPinsString = pinsHit.ToString();
            attempt1PinsHit = pinsHit;
        }
        else // non strike in attempt 1
        { 
            knockedPinsString = pinsHit.ToString();
            attempt1PinsHit = pinsHit;
        }
        Debug.Log("Pins: " + pinsHit);

        // Update Score
        UpdateScore(currentPlayer, currentFrame, currentAttempt, knockedPinsString);
        if(P1CurrentCumulativeIndex != 2 || P1CurrentCumulativeIndex != 5 || P1CurrentCumulativeIndex != 8 || P1CurrentCumulativeIndex != 11 || P1CurrentCumulativeIndex != 14 || 
           P2CurrentCumulativeIndex != 2 || P2CurrentCumulativeIndex != 5 || P2CurrentCumulativeIndex != 8 || P2CurrentCumulativeIndex != 11 || P2CurrentCumulativeIndex != 14)
        {
            UpdateCumulativeScore(knockedPinsString, currentPlayer, currentFrame);
        }

        // Switch Player
        yield return new WaitForSeconds(switchTime);
        if(currentAttempt == 3)
        {
            if(currentPlayer == Player.First)
            {
                SwitchPlayer(currentPlayer);
                currentPlayer = Player.Second;
                currentAttempt = 1;
                attempt1PinsHit = 0;
                bowlingBallController.ResetBallPosition();
                pinSetManager.DestroyPinSet();
                pinSetSpawner.SpawnPins();
            }
            else if(currentPlayer == Player.Second)
            {
                Debug.Log("GAME END!");
            }
        }
        else if(currentAttempt == 2 && currentFrame == maxSetSize -1)
        {
            if(pinsHit == 10)
            {
                bowlingBallController.ResetBallPosition();
                pinSetManager.DestroyPinSet();
                pinSetSpawner.SpawnPins();
                currentAttempt = 3;
            }
            else
            {
                bowlingBallController.ResetBallPosition();
                pinSetManager.knockedPins = 0;
                pinSetManager.ResetSparePinsAttempt();
                currentAttempt = 3;
            }
        }
        else if(currentPlayer == Player.First && currentAttempt == 2)
        {
            SwitchPlayer(currentPlayer);
            currentPlayer = Player.Second;
            currentAttempt = 1;
            attempt1PinsHit = 0;
            bowlingBallController.ResetBallPosition();
            pinSetManager.DestroyPinSet();
            pinSetSpawner.SpawnPins();
        }
        else if (currentPlayer == Player.Second && currentAttempt == 2)
        {
            SwitchPlayer(currentPlayer);
            currentPlayer = Player.First;
            currentAttempt = 1;
            attempt1PinsHit = 0;
            bowlingBallController.ResetBallPosition();
            pinSetManager.DestroyPinSet();
            pinSetSpawner.SpawnPins();
            currentFrame++;
        }
        else if(currentPlayer == Player.First && currentAttempt == 1)
        {
            if (currentFrame == maxSetSize - 1)
            {
                if (pinsHit == 10)
                {
                    bowlingBallController.ResetBallPosition();
                    pinSetManager.DestroyPinSet();
                    pinSetSpawner.SpawnPins();
                    currentAttempt = 2;
                }
                else
                {
                    bowlingBallController.ResetBallPosition();
                    pinSetManager.knockedPins = 0;
                    pinSetManager.ResetSparePinsAttempt();
                    currentAttempt = 2;
                }
            }
            else
            {
                if (pinsHit == 10)
                {
                    // go immediately to second player
                    UpdateScore(currentPlayer, currentFrame, 2, Constant.MISS);
                    UpdateCumulativeScore(Constant.MISS, currentPlayer, currentFrame);
                    SwitchPlayer(currentPlayer);
                    currentPlayer = Player.Second;
                    currentAttempt = 1;
                    attempt1PinsHit = 0;
                    bowlingBallController.ResetBallPosition();
                    pinSetManager.DestroyPinSet();
                    pinSetSpawner.SpawnPins();
                }
                else
                {
                    // Reset
                    bowlingBallController.ResetBallPosition();
                    pinSetManager.knockedPins = 0;
                    pinSetManager.ResetSparePinsAttempt();
                    currentAttempt = 2;
                }
            }   
        }
        else if(currentPlayer == Player.Second && currentAttempt == 1)
        {
            if (currentFrame == maxSetSize - 1)
            {
                if (pinsHit == 10)
                {
                    bowlingBallController.ResetBallPosition();
                    pinSetManager.DestroyPinSet();
                    pinSetSpawner.SpawnPins();
                    currentAttempt = 2;
                }
                else
                {
                    bowlingBallController.ResetBallPosition();
                    pinSetManager.knockedPins = 0;
                    pinSetManager.ResetSparePinsAttempt();
                    currentAttempt = 2;
                }
            }
            else
            {
                if (pinsHit == 10)
                {
                    // go immediately to 1st player
                    UpdateScore(currentPlayer, currentFrame, 2, Constant.MISS);
                    UpdateCumulativeScore(Constant.MISS, currentPlayer, currentFrame);
                    SwitchPlayer(currentPlayer);
                    currentPlayer = Player.First;
                    currentAttempt = 1;
                    attempt1PinsHit = 0;
                    bowlingBallController.ResetBallPosition();
                    pinSetManager.DestroyPinSet();
                    pinSetSpawner.SpawnPins();
                    currentFrame++;
                }
                else
                {
                    bowlingBallController.ResetBallPosition();
                    pinSetManager.knockedPins = 0;
                    pinSetManager.ResetSparePinsAttempt();
                    currentAttempt = 2;
                }
            }
        }
    }

    public void InitializeCumulativeScore()
    {
        P1CumulativeScore = new List<int>();
        P2CumulativeScore = new List<int>();

        for (int i = 0; i < 18; i++)
        {
            P1CumulativeScore.Add(0);
            P2CumulativeScore.Add(0);
        }
    }

    private void InitializeFrame()
    {
        //Spawn the Frame per each player
        for(int i = 0; i < maxSetSize; i++)
        {
            SpawnFrame(Player.First, i);
        }

        for (int i = 0; i < maxSetSize; i++)
        {
            SpawnFrame(Player.Second, i);
        }
    }

    private void SpawnFrame(Player selectedPlayer, int index)
    {
        switch (selectedPlayer)
        {
            case Player.First:
                //End the Spawn of the Frame
                if (index == maxSetSize - 1)
                {
                    GameObject copyFrame = GameObject.Instantiate(lastSetReference);
                    copyFrame.transform.SetParent(player1FrameHolder.transform);
                    copyFrame.GetComponent<BLastSetData>().DeclareSetCount(index + 1);
                    Player1FrameList.Add(copyFrame);

                    GameObject totalFrame = GameObject.Instantiate(totalScoreReference);
                    totalFrame.transform.SetParent(player1FrameHolder.transform);
                    Player1FrameList.Add(totalFrame);

                    Debug.Log("Finished Building");

                }

                //Normal Spawn of the Frame
                else
                {
                    GameObject copyFrame = GameObject.Instantiate(normalSetReference);
                    copyFrame.transform.SetParent(player1FrameHolder.transform);
                    copyFrame.GetComponent<BSetData>().DeclareSetCount(index + 1);
                    Player1FrameList.Add(copyFrame);
                }
                break;

            case Player.Second:
                //End the Spawn of the Frame
                if (index == maxSetSize - 1)
                {
                    GameObject copyFrame = GameObject.Instantiate(lastSetReference);
                    copyFrame.transform.SetParent(player2FrameHolder.transform);
                    copyFrame.GetComponent<BLastSetData>().DeclareSetCount(index + 1);
                    Player2FrameList.Add(copyFrame);

                    GameObject totalFrame = GameObject.Instantiate(totalScoreReference);
                    totalFrame.transform.SetParent(player2FrameHolder.transform);
                    Player2FrameList.Add(totalFrame);

                }

                //Normal Spawn of the Frame
                else
                {
                    GameObject copyFrame = GameObject.Instantiate(normalSetReference);
                    copyFrame.transform.SetParent(player2FrameHolder.transform);
                    copyFrame.GetComponent<BSetData>().DeclareSetCount(index + 1);
                    Player2FrameList.Add(copyFrame);
                }
                break;

            default:
                Debug.LogError("Chosen Player Error");
                break;
        }
    }


    //Player Update Logic
    public void UpdateScore(Player currentPlayer, int frameIndex, int attemptNum, string score)
    {
        List<GameObject> CopyFrameList = new List<GameObject>();
        switch (currentPlayer)
        {
            case Player.First:
                CopyFrameList = Player1FrameList;
                break;

            case Player.Second:
                CopyFrameList = Player2FrameList;
                break;

            default:
                Debug.LogError("Chosen Player Error for Frame Checking");
                break;
        }

        if (frameIndex + 1 == maxSetSize)
        {
            Debug.Log("In Third HIT");

            switch (attemptNum)
            {
                case 1:
                    CopyFrameList[frameIndex].GetComponent<BLastSetData>().UpdateFirstHit(score); // One of the possible cause of null reference errors
                    break;

                case 2:
                    CopyFrameList[frameIndex].GetComponent<BLastSetData>().UpdateSecondHit(score); // One of the possible cause of null reference errors
                    break;

                case 3:
                    CopyFrameList[frameIndex].GetComponent<BLastSetData>().UpdateThirdHit(score); // One of the possible cause of null reference errors
                    break;

                default:
                    Debug.LogError("Wrong Slot");
                    break;
            }
           
        }

        else if  (frameIndex == maxSetSize) // indicator of the value being total count
        {
            CopyFrameList[frameIndex].GetComponent<TotalData>().SetTotal(score);
        }

        else
        {
            switch (attemptNum)
            {
                case 1:
                    CopyFrameList[frameIndex].GetComponent<BSetData>().UpdateFirstHit(score); // One of the possible cause of null reference errors
                    break;

                case 2:
                    CopyFrameList[frameIndex].GetComponent<BSetData>().UpdateSecondHit(score); // One of the possible cause of null reference errors
                    break;

                default:
                    Debug.LogError("Wrong Slot");
                    break;
            }
        }

    }

    public void SwitchPlayer(Player activePlayer)
    {
        if(activePlayer == Player.First)
        {
            activePlayerFrame.SetActive(false);
            activePlayerFrame = player2FrameHolder;
            activePlayerFrame.SetActive(true);
        }

        else
        {
            activePlayerFrame.SetActive(false);
            activePlayerFrame = player1FrameHolder;
            activePlayerFrame.SetActive(true);
        }
    }


    //Pin Update Logic
    public void PinHit(int index)
    {
        pinInformation.GetComponent<PinData>().PinHit(index);
    }

    public void ResetPint()
    {
        pinInformation.GetComponent<PinData>().ResetPinData();
    }

    //End Game
    public void EndGame(Player winnerPlayer)
    {
        if(winnerPlayer == Player.First)
        {
            winnerText.text = "Player 1 Wins";
        }

        if (winnerPlayer == Player.Second)
        {
            winnerText.text = "Player 2 Wins";
        }
    }




}
