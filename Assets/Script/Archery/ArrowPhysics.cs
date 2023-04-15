using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;


public class ArrowPhysics : MonoBehaviour
{
    private Rigidbody body;
    private Transform direction;
    
    
    [SerializeField] private float power = 30;
    [SerializeField] GameObject testpoint;
    ArcheryScoring score;
    WindpressureHandler wind;

    bool isHit = false;
    //Place instance here
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.AddForce(transform.up * power, ForceMode.Impulse);
        score = ArcheryScoring.Instance;
        wind = WindpressureHandler.Instance;
    }

    private void Update()
    {
        int direction = wind.GetDirection();
        float windPower = wind.GetWindPower();
        if (!isHit)
        {
            body.AddForce(-transform.right * direction * windPower, ForceMode.Force);
        }
    }

    // Update is called once per frame
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, transform.up * 10) ;
    }

    public void SetDirection(Transform transform)
    {
        direction = transform;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            
            Debug.Log("Target");
            //GameObject obj = Instantiate(testpoint, collision.contacts[0].point, transform.rotation);
            score.ReceiveArrowLoc(collision.contacts[0].point);
            body.useGravity = false;
            body.velocity = Vector3.zero;
            Collider collider = GetComponent<Collider>();
            collider.enabled = false;
            body.freezeRotation = true;

            //Destroy(this.gameObject);

            //UI.Instance.AddScore(distance)
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            
            score.ReceiveArrowLoc(new Vector3(1000,1000,1000));
            //UI.Instance.AddScore(-1)
            Debug.Log("Ground");

            Destroy(this.gameObject);
        }
        isHit = true;
    }

}
