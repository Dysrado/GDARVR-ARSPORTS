using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalData : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalTxt;

    public void SetTotal(string total)
    {
        totalTxt.text = total;
    }

    
}
