using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingScoreManager : MonoBehaviour
{
    [SerializeField] private BowlingScoreDetector bowlingScoreDetector;
    public PinSetManager pinSetManager;
    public UIManager uIManager;

    // Start is called before the first frame update
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(bowlingScoreDetector.BallPassed)
        {
            StartCoroutine(CheckScore());
        }
        */
    }

    IEnumerator CheckScore()
    {
        bowlingScoreDetector.BallPassed = false;
        yield return new WaitForSeconds(5.0f);
        Debug.Log("Check Score Now!");
        int pins = pinSetManager.CheckKnockedOverPins();
        uIManager.SendKnockedPinsToUI();

    }
}
