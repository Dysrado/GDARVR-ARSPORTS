using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pinSet;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        // Reset Pins
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject.Instantiate(pinSet);
        }
    }

    public void SpawnPins()
    {
        GameObject.Instantiate(pinSet);
    }

}
