using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public float forceCof; 
    public float maxSpeed;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.A)) {
            rb.AddForce(-Vector3.right * forceCof);
        }
        if (Input.GetKey(KeyCode.D)) {
            rb.AddForce(Vector3.right * forceCof);
        }
        if (Input.GetKey(KeyCode.W)) {
            rb.AddForce(Vector3.forward * forceCof);
        }
        if (Input.GetKey(KeyCode.S)) {
            rb.AddForce(-Vector3.forward * forceCof);
        }

        rb.velocity = Vector3.ClampMagnitude(GetComponent<Rigidbody>().velocity, maxSpeed);
    }

}
