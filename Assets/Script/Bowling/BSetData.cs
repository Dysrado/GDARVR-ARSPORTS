using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BSetData : MonoBehaviour
{
    [Header("Bowling Stats Data")]
    [SerializeField] private TextMeshProUGUI setCount;
    [SerializeField] private TextMeshProUGUI firstHitScore;
    [SerializeField] private TextMeshProUGUI secondHitScore;
    [SerializeField] private TextMeshProUGUI cumultativeTotal;


    //Setter Function
    public void DeclareSetCount(int count)
    {
        setCount.text = count.ToString();
    }

    public void UpdateFirstHit(string score)
    {
        firstHitScore.text = score;
    }

    public void UpdateSecondHit(string score)
    {
        secondHitScore.text = score;
    }

    public void UpdateCumultativeScore(string score)
    {
        cumultativeTotal.text = score;
    }


}
