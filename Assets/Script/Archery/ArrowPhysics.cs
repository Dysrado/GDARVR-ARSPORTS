using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;


public class ArrowPhysics : MonoBehaviour
{
    private Rigidbody body;
    private Transform direction;
    
    [SerializeField] private float power = 30;
    ArcheryScoring score;
    //Place instance here
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.AddForce(transform.up * power, ForceMode.Impulse);
        score = ArcheryScoring.Instance;
    }

    // Update is called once per frame
   

    public void SetDirection(Transform transform)
    {
        direction = transform;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            
            Debug.Log("Target");
            score.ReceiveArrowLoc(this.transform.position);
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
    }

}
