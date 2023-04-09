using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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




    // Start is called before the first frame update
     void Start()
    {
        Player1FrameList = new List<GameObject>();
        Player2FrameList = new List<GameObject>();

        activePlayerFrame = player1FrameHolder;
        activePlayerFrame.SetActive(true);
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
                    copyFrame.transform.SetParent(player1FrameHolder.transform);
                    Player1FrameList.Add(totalFrame);

                }

                //Normal Spawn of the Frame
                else
                {
                    GameObject copyFrame = GameObject.Instantiate(lastSetReference);
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
                    copyFrame.transform.SetParent(player1FrameHolder.transform);
                    copyFrame.GetComponent<BLastSetData>().DeclareSetCount(index + 1);
                    Player2FrameList.Add(copyFrame);

                    GameObject totalFrame = GameObject.Instantiate(totalScoreReference);
                    copyFrame.transform.SetParent(player1FrameHolder.transform);
                    Player2FrameList.Add(totalFrame);

                }

                //Normal Spawn of the Frame
                else
                {
                    GameObject copyFrame = GameObject.Instantiate(lastSetReference);
                    copyFrame.transform.SetParent(player1FrameHolder.transform);
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
