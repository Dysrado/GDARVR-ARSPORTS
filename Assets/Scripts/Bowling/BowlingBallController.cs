using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBallController : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] private float bowlSpeed;
    [SerializeField] private float adjustSpeed;
    private Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        originalPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space Pressed!");
            Vector3 forwardforce = new Vector3(0, 0, -1 * bowlSpeed * Time.deltaTime);
            rb.AddForce(forwardforce, ForceMode.Impulse);
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 leftMove = new Vector3(adjustSpeed, 0, 0);
            gameObject.transform.Translate(leftMove);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 rightMove = new Vector3(-1 * adjustSpeed, 0, 0);
            gameObject.transform.Translate(rightMove);
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            gameObject.transform.position = originalPos;
        }
    }

    public void MoveBallLeft()
    {
        Vector3 leftMove = new Vector3(adjustSpeed, 0, 0);
        gameObject.transform.Translate(leftMove);
    }

    public void MoveBallRight()
    {
        Vector3 rightMove = new Vector3(-1 * adjustSpeed, 0, 0);
        gameObject.transform.Translate(rightMove);
    }

    public void LaunchBall()
    {
        Vector3 forwardforce = new Vector3(0, 0, -1 * bowlSpeed * Time.deltaTime);
        rb.AddForce(forwardforce, ForceMode.Impulse);
    }

    public void ResetBallPosition()
    {
        gameObject.transform.position = originalPos;
        gameObject.transform.rotation = Quaternion.identity;
        rb.position = originalPos;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
