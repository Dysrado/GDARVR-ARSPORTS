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
    [SerializeField] private GameObject selectionScreen;
    [SerializeField] private GameObject selectPlayerSettings;
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private ActiveGame TypeSection;


    //Required Attribute
    private ActiveGame SelectedGame = ActiveGame.None;
    private int playerCount = 2; //No of players competing with
    private GameObject activeScreen;


    //Scene Transition Function = Internals Calls

    private void Start()
    {
        OnStartInitialization();
 
    }


    private void OnStartInitialization()
    {
        switch (TypeSection)
        {
            case ActiveGame.MainMenu:
                activeScreen = mainMenuScreen;
                break;

            //Add more cases for different Games;
        }
    }


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

    // Handling Sports Select

    public void OnQuit()
    {
        Application.Quit();
        Debug.Log("Exitting the Game");
    }


    public void OnSelectSports()
    {
        selectionScreen.SetActive(true);
        activeScreen.SetActive(false);
    }

    public void OnDeselectSports()
    {
        selectionScreen.SetActive(false);
        activeScreen.SetActive(true);

        Debug.Log("Click Back");
    }


    // Handling Choose Player
    public void OnSelectPlayer()
    {
        selectPlayerSettings.SetActive(true);
        selectionScreen.SetActive(false);
    }

    public void OnDeselectPlayer()
    {
        selectPlayerSettings.SetActive(false);
        selectionScreen.SetActive(true);
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
            Debug.Log("Choosen Sport: Bowling");
        }

        public void ChooseArcher()
        {
            SelectedGame = ActiveGame.Archery;
            Debug.Log("Choosen Sport: Archery");
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
