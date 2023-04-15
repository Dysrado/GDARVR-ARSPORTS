using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindpressureHandler : MonoBehaviour
{

    public static WindpressureHandler Instance;
    [SerializeField] int direction = 0;
    [SerializeField] float windPower = .5f;

    [SerializeField] float minTime = 1.0f;
    [SerializeField] float maxTime = 3.0f;
    float timer;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        timer = RandomTime();
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer <= 0.0f)
        {
            direction = Random.Range(-1,1);
            timer = RandomTime();
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    float RandomTime() { return Random.Range(minTime, maxTime); }

    public int GetDirection() { return direction; }
    public float GetWindPower() { return windPower; }
}
