using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI knockedOverPinsValue;
    public PinSetManager pinSetManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void SendKnockedPinsToUI()
    {
        string knockedPinValue;
        if(pinSetManager.knockedPins == 10)
        {
            knockedPinValue = "X";
        }
        else
        {
            knockedPinValue = pinSetManager.knockedPins.ToString();
        }
        knockedOverPinsValue.text = knockedPinValue;
    }

}
