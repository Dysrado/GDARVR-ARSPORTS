using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ArcheryGroundPlane : MonoBehaviour
{
    [SerializeField] bool isContentPlaced = false;
    [SerializeField] Transform CameraPosition;
    [SerializeField] GameObject Content;
    [SerializeField] GameObject btn;
    bool grab = false;
    bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (isContentPlaced)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;
                    Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(r, out hit))
                    {
                        if (hit.collider.gameObject.CompareTag("Target"))
                        {
                            grab = !grab;
                        }
                    }
                }
            }
        }
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

    public void AutomaticHitResult(HitTestResult hit)
    {
        if (canMove)
        {
            if (grab)
            {
                Content.transform.position = hit.Position;
                Content.transform.LookAt(new Vector3(-CameraPosition.position.x, 0, -CameraPosition.position.z));
            }
        }
        
    }

    public void LockTarget()
    {
        canMove = false;
        btn.SetActive(false);
    }
}
