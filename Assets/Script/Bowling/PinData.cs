using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinData : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> pinContainer;
    
    public void PinHit(int index)
    {
        pinContainer[index].text = "X";
    }

    public void ResetPinData()
    {
        for(int i = 0; i < 10; i++)
        {
            pinContainer[i].text = (i + 1).ToString();
        }
    }
}
