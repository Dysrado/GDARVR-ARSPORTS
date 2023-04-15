using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ArcheryScoring : MonoBehaviour
{
    public static ArcheryScoring Instance;
    ArcheryShooting shooting;
    

    float p1 = 0.09868f; //inbetween inner and outer y   //Closest to middle
   float p2 = 0.1965f; //line of y and inner r
    float p3 = 0.29518f;
    float p4 = 0.39355f;
    float p5 = 0.4925f;
    
    [SerializeField] GameObject center;
    ArcheryUIBehaviour ui;
    [SerializeField] int[] playerScores;
    
    [SerializeField] int currentPlayer = 0;
    [SerializeField] int[] playerSets;
    [SerializeField] int sets = 0;

    [SerializeField] int maxArrowsShot = 5;
    [SerializeField] GameObject panel;
    WindpressureHandler windpressureHandler;
    private float startTicks = 210f;
    private float ticks = 0;

    int arrowsShot = 0;

    private bool SetStart = false;

    // Start is called before the first frame update
    void Awake()
    {
        windpressureHandler = WindpressureHandler.Instance;
        if (Instance == null)
        {
            Instance = this;
        }
       
        shooting = FindObjectOfType<ArcheryShooting>();
        currentPlayer = -1;
        panel.SetActive(true);
    }

    private void Start()
    {
        ui = ArcheryUIBehaviour.Instance;
    }


    public void StartTimer()
    {
        SetStart = true;
        ticks = startTicks;
        ui.ActivateTimer();
    }

    public float GetTicks()
    {
        return ticks;
    }
    public int GetWindPressure()
    {
       return windpressureHandler.GetDirection();
    }

    public int GetCurrentPlayer()
    {
        return currentPlayer;
    }

    



    public void ReceiveArrowLoc(Vector3 arrow)
    {
        Vector2 c = new Vector2 (center.transform.position.x, center.transform.position.y);
        Vector2 a = new Vector2 (arrow.x, arrow.y);
        float distance = Mathf.Abs(Vector2.Distance(a,c));

        int outputScore = 0;
        if (distance <= p5 && distance > p4)
        {
            //you get 2 points
            outputScore = 2;
            Debug.Log("White");
        }
        else if (distance <= p4 && distance > p3)
        {
            //you get 4 points
            outputScore = 4;

            Debug.Log("Black");
        }
        else if (distance <= p3 && distance > p2)
        {
            //you get 6 points
            outputScore = 6;
            Debug.Log("Blue");
        }
        else if (distance <= p2 && distance > p1)
        {
            //you get 8 points
            outputScore = 8;
            Debug.Log("Red");
        }
        else if (distance <= p1)
        {
            //you get 10 points
            outputScore = 10;
            Debug.Log("Yellow");
        }
        else if (arrow == new Vector3(1000, 1000, 1000))
        {
            //you get 0 points
            Debug.Log("Fail");
        }

        Debug.Log("Distance: " + distance);

     
        playerScores[currentPlayer] += outputScore;

        switch (currentPlayer)
        {
            case 0:
                ui.UpdatePlayerScore(ArcheryUIBehaviour.Player.First, playerScores[currentPlayer]);
                break;
            case 1:
                ui.UpdatePlayerScore(ArcheryUIBehaviour.Player.Second, playerScores[currentPlayer]);
                break;
        }        
    }


    public void IncrementShot()
    {
        arrowsShot++;
    }
    public void NextPlayer()
    {
        panel.SetActive(false);
        currentPlayer++;
        if (currentPlayer > 1)
        {
            CheckSet();
            currentPlayer = 0;
            playerScores[0] = 0;
            playerScores[1] = 0;
            ui.UpdatePlayerScore(ArcheryUIBehaviour.Player.First, playerScores[0]);
            ui.UpdatePlayerScore(ArcheryUIBehaviour.Player.Second, playerScores[1]);

        }
        arrowsShot = 0;
        StartTimer();
        
        
        //Delete all arrows just in case
        //reset timer
    }


    public int GetArrowsShot()
    {
        return arrowsShot;
    }
    // Update is called once per frame
    void Update()
    {
        if (arrowsShot >= maxArrowsShot || (ticks <= 0 && SetStart))
        {
            // =============================================================== Change player UI
            panel.SetActive(true);
            SetStart = false;
            ui.DisabledTimer();
            
        }

        if (SetStart == true)
        {
            ticks -= Time.deltaTime;
            //UpdateTimer UI
            ui.SetTime((int)ticks);
        }
    }

    public void CheckSet()
    {
        if (playerScores[0] > playerScores[1])
        {
            //Player 1 wins the set
            playerSets[0]++;
            if (playerSets[0] <= 2)
            {
            ui.UpdateSetScore(ArcheryUIBehaviour.Player.First, playerSets[0]);
            }
            if (playerSets[0] == 3)
            {
                ui.EndGame(ArcheryUIBehaviour.Player.First);
            }
            sets++;

        }

        else if (playerScores[0] < playerScores[1])
        {
            playerSets[1]++;
            if (playerSets[1] <= 2) { 
                ui.UpdateSetScore(ArcheryUIBehaviour.Player.Second, playerSets[1]);
            }
            sets++;
            if (playerSets[1] == 3)
            {
                ui.EndGame(ArcheryUIBehaviour.Player.Second);
            }

            //player 2 wins the set
        }


       
    }

}
