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


    }

    public void UpdateCumulativeScore(string knockedPinsString)
    {
        int cumulativeScore = CheckCumulativeScoreString(knockedPinsString);
    }

    public int CheckCumulativeScoreString(string knockedPinsString)
    {
        if(knockedPinsString == "0")
        {
            return 0;
        }
        else if (knockedPinsString == "1")
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
        else if (knockedPinsString == "7")
        {
            return 6;
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
            knockedPinsString = Constant.MISS;
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
        UpdateCumulativeScore(knockedPinsString);

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
        for(int i = 0; i < 16; i++)
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
