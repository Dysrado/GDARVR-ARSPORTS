using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class ArcheryShooting : MonoBehaviour
{
    [SerializeField] GameObject ArrowUI;
    [SerializeField] GameObject ArrowPrefab;
    [SerializeField] GameObject offset;
    [SerializeField] bool GameStart = false;
    [SerializeField] bool SetStart = false;
    [SerializeField] Transform parent;

    private int ShotsFired = 0;

    // Start is called before the first frame update
    void Start()
    {
        //SampleTesting
        GameStart = true;
        SetStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireArrow()
    {
        if (GameStart == true && SetStart == true)
        {
            GameObject arrow = Instantiate(ArrowPrefab, offset.transform.position, offset.transform.rotation, parent);
            ShotsFired++;
        }
    }
}
