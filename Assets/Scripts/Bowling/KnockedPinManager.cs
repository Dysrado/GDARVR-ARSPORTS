using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockedPinManager : MonoBehaviour
{
    [SerializeField] private GameObject pin;
    public bool isKnockedOver = false;
    public bool hit1stAttempt = false;
    public bool hitFloor = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Floor") && !isKnockedOver)
        {
            //Debug.Log("Knocked Over!");
            isKnockedOver = true;
            hitFloor = true;
        }
    }

    public bool CheckIfPinKnocked()
    {
        if ((pin.transform.rotation.eulerAngles.x <= 269 || pin.transform.rotation.eulerAngles.x >= 271) && !isKnockedOver && !hitFloor && !hit1stAttempt)
        {
            isKnockedOver = true;
        }

        return isKnockedOver;
    }
}
