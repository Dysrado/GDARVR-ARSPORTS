using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ArcheryScoring : MonoBehaviour
{
    float p1 = 0.09868f; //inbetween inner and outer y   //Closest to middle
   float p2 = 0.1965f; //line of y and inner r
    float p3 = 0.29518f;
    float p4 = 0.39355f;
    float p5 = 0.4925f;
    
    [SerializeField] GameObject center;
    public static ArcheryScoring Instance;
    ArcheryUIBehaviour ui;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        ui = ArcheryUIBehaviour.Instance;
    }

    public void ReceiveArrowLoc(Vector3 arrow)
    {
        Vector2 c = new Vector2 (center.transform.position.x, center.transform.position.y);
        Vector2 a = new Vector2 (arrow.x, arrow.y);
        float distance = Mathf.Abs(Vector2.Distance(a,c));
       
        if (distance <= p5 && distance > p4)
        {
            //you get 6 points
            Debug.Log("White");
        }
        else if (distance <= p4 && distance > p3)
        {
            //you get 7 points
            Debug.Log("Black");
        }
        else if (distance <= p3 && distance > p2)
        {
            //you get 8 points
            Debug.Log("Blue");
        }
        else if (distance <= p2 && distance > p1)
        {
            //you get 9 points
            Debug.Log("Red");
        }
        else if (distance <= p1)
        {
            //you get 10 points
            Debug.Log("Yellow");
        }
        else if (arrow == new Vector3(1000, 1000, 1000))
        {
            //you get 0 points
            Debug.Log("Fail");
        }

        Debug.Log("Distance: " + distance);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
