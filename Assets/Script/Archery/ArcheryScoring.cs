using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ArcheryScoring : MonoBehaviour
{
    float p1 = 0.018f; //inbetween inner and outer y   //Closest to middle
   float p2 = 0.045f; //line of y and inner r
    float p3 = 0.060f;
    float p4 = 0.086f;
    float p5 = 0.125f;
    float p6 = 0.145f;
    float p7 = 0.160f;
    float p8 = 0.182f;
    float p9 = 0.217f;
    float p10 = 0.6f;//Farthest from Middle
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
        if (distance <= p10 && distance > p9)
        {
            //you get 1 points
            Debug.Log("Outer White");
        }
        else if (distance <= p9 && distance > p8)
        {
            //you get 2 points
            Debug.Log("Inner White");
        }
        else if (distance <= p8 && distance > p7)
        {
            Debug.Log("Outer Black");
            //you get 3points
        }
        else if (distance <= p7 && distance > p6)
        {
            //you get 4 points
            Debug.Log("Inner Black");

        }
        else if (distance <= p6 && distance > p5)
        {
            //you get 5 points
            Debug.Log("Outer Blue");

        }
        else if (distance <= p5 && distance > p4)
        {
            //you get 6 points
            Debug.Log("Inner Blue");
        }
        else if (distance <= p4 && distance > p3)
        {
            //you get 7 points
            Debug.Log("Outer Red");
        }
        else if (distance <= p3 && distance > p2)
        {
            //you get 8 points
            Debug.Log("Inner Red");
        }
        else if (distance <= p2 && distance > p1)
        {
            //you get 9 points
            Debug.Log("Outer Yellow");
        }
        else if (distance <= p1)
        {
            //you get 10 points
            Debug.Log("Inner Yellow");
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
