using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;


public class ArrowPhysics : MonoBehaviour
{
    private Rigidbody body;
    private Transform direction;
    private float power = 30;
    private SphereCollider collider;
    //Place instance here
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.AddForce(transform.forward * power, ForceMode.Impulse);
        collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDirection(Transform transform)
    {
        direction = transform;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            float distance = Mathf.Abs(Vector3.Distance(this.gameObject.transform.position, collision.gameObject.transform.position));
            //UI.Instance.AddScore(distance)
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            //UI.Instance.AddScore(-1)
            Destroy(this.gameObject);
        }
    }

}
