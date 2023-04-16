using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ArcheryPlayerData : MonoBehaviour
{
    [Header("Character GameObject Reference")]
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI characterScore;
    [SerializeField] private Image firstSetImage;
    [SerializeField] private Image secondSetImage;

    [Header("Specific Customization Reference")]
    [SerializeField] private Sprite defaultSetReference;
    [SerializeField] private Sprite wonSetReference;

 
    private Color defaultColor;

    private void Start()
    {
        defaultColor = playerName.color;
    }


   

    //Setter Attribute
    public void SetName(string name)
    {
        playerName.text = name;
    }

    public void SetScore(int score)
    {
        characterScore.text = score.ToString();
    }

    public void TickFirstSet()
    {
        firstSetImage.sprite = wonSetReference;
    }

    public void TickSecondSet()
    {
        secondSetImage.sprite = wonSetReference;
    }

    public void ResetStats()
    {
        characterScore.text = "0";
        firstSetImage.sprite = defaultSetReference;
        secondSetImage.sprite = defaultSetReference;

    }

    public void SetPlayerTurn()
    {
        playerName.color = Color.red;
    }

    public void RemovePlayerTurn()
    {
        playerName.color = defaultColor;
    }


    //Getter Value
    public string GetName()
    {
        return playerName.text;
    }


}
