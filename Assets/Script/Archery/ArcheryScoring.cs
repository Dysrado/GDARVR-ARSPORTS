using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ArcheryScoring : MonoBehaviour
{
    [SerializeField] float p1;    //Closest to middle
    [SerializeField] float p2;
    [SerializeField] float p3;
    [SerializeField] float p4;
    [SerializeField] float p5;
    [SerializeField] float p6;
    [SerializeField] float p7;
    [SerializeField] float p8;
    [SerializeField] float p9;
    [SerializeField] float p10; //Farthest from Middle
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
        float distance = Mathf.Abs(Vector3.Distance(center.transform.position, arrow));
        if (distance <= p10 && distance > p9)
        {
            //you get 1 points
        }
        else if (distance <= p9 && distance > p8)
        {
            //you get 2 points
        }
        else if (distance <= p8 && distance > p7)
        {
            //you get 3points
        }
        else if (distance <= p7 && distance > p6)
        {
            //you get 4 points
        }
        else if (distance <= p6 && distance > p5)
        {
            //you get 5 points
        }
        else if (distance <= p5 && distance > p4)
        {
            //you get 6 points
        }
        else if (distance <= p4 && distance > p3)
        {
            //you get 7 points
        }
        else if (distance <= p3 && distance > p2)
        {
            //you get 8 points
        }
        else if (distance <= p2 && distance > p1)
        {
            //you get 9 points
        }
        else if (distance <= p1)
        {
            //you get 10 points
        }
        else if (arrow == new Vector3(1000,1000,1000))
        {
            //you get 0 points
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
