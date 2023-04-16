using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExistenceChecker : MonoBehaviour
{
    //public static ExistenceChecker instance;

    //private void Awake()
    //{

    //}

    public bool isFound = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IsFound()
    {
        Time.timeScale = 1.0f;
    }

    public void IsLost()
    {
        Time.timeScale = 0.0f;
    }
    
}
