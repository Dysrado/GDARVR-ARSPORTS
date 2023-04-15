using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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

    [SerializeField] int maxArrowsShot = 5;

    int arrowsShot = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        ui = ArcheryUIBehaviour.Instance;
        shooting = FindObjectOfType<ArcheryShooting>();
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

        arrowsShot++;
        playerScores[currentPlayer] += outputScore;

        if (arrowsShot >= maxArrowsShot)
        {
            // =============================================================== Change player UI
            currentPlayer++;
            if (currentPlayer > 1)
            {
                currentPlayer = 0;
            }
            arrowsShot = 0;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

}
