using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ArcheryGroundPlane : MonoBehaviour
{
    [SerializeField] bool isContentPlaced = false;
    [SerializeField] Transform CameraPosition;
    [SerializeField] GameObject Content;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PositionContentAtPlaneAnchor(HitTestResult hit)
    {
        if (!isContentPlaced)
        {
            GetComponent<ContentPositioningBehaviour>().PositionContentAtPlaneAnchor(hit);
            Content.transform.LookAt(new Vector3(-CameraPosition.position.x, 0, -CameraPosition.position.z));
            isContentPlaced = true;
        }

    }
}
