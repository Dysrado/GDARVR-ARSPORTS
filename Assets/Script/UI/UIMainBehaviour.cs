using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitch : MonoBehaviour
{
    public const string MAINMENU = "MainMenu";
    public const string ARCHERY = "Archery";
    public const string BOWLING = "Bowling";
}


public class UIMainBehaviour : MonoBehaviour
{
    enum ActiveGame
    {
        MainMenu,
        Archery,
        Bowling,
        None
    }


    [Header("Required Reference")]
    [SerializeField] private GameObject SelectionScreen;
    [SerializeField] private GameObject OnSelectPlayerSettings;
    [SerializeField] private ActiveGame TypeSection;


    //Required Attribute
    private ActiveGame SelectedGame = ActiveGame.None;
    private int playerCount = 2; //No of players competing with
    

    //Scene Transition Function = Internals Calls

    private void OnSelectedMainMenu()
    {
        SceneManager.LoadScene(SceneSwitch.MAINMENU);
    }

    private void OnSelectedBowling()
    {
        SceneManager.LoadScene(SceneSwitch.BOWLING);
    }

    private void OnSelectedArchery()
    {
        SceneManager.LoadScene(SceneSwitch.ARCHERY);
    }

    // UI Behaviour

    public void OnSelectSports()
    {
        SelectionScreen.SetActive(true);
    }

    public void OnDeselectSports()
    {
        SelectionScreen.SetActive(false);
    }


    public void OnSelectPlayer()
    {
        OnSelectPlayerSettings.SetActive(true);
    }

    public void OnDeselectPlayer()
    {
        OnSelectPlayerSettings.SetActive(false);
    }

    private void ConfirmGame()
    {
        switch (SelectedGame)
        {
            case ActiveGame.Archery:
                //call the function to store the final player count;
                OnSelectedArchery();
                break;

            case ActiveGame.Bowling:
                //call the function to store the final player count;
                OnSelectedBowling();
                break;

            default:
                Debug.LogWarning("None of the Option Present");
                break;
        }
    }


    //Button or Interaction Behaviour
        //For Sports Selection
        public void ChooseBowling()
        {
            SelectedGame = ActiveGame.Bowling;
        }

        public void ChooseArcher()
        {
            SelectedGame = ActiveGame.Archery;
        }

        //For Player Count

        public void OnPlayerSelectTwo()
        {
            playerCount = 2;
        }

        public void OnPlayerSelectThree()
        {
            playerCount = 3;
        }

        public void OnPlayerSelectFour()
        {
            playerCount = 4;
        }




}
