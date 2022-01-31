using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public GameObject child;

    public float forceCof; 
    public float maxSpeed;

    public Vector3 offset;

    Rigidbody rb;
    bool canMove = true;
    bool isShadow = false;
    

    public void LockMovement() {
        canMove = false;
        child.GetComponent<TriangleFollowing>().LockMovement();
    }

    public void UnlockMovement() {
        canMove = true;
        child.GetComponent<TriangleFollowing>().UnlockMovement();
    }

    public void SwitchNonShadow() {
        isShadow = false;
        rb.useGravity = true;
        GetComponent<Collider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
        child.GetComponent<TriangleFollowing>().Deactivate();
    }

    public void SwitchShadow() {
        isShadow = true;
        rb.useGravity = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        child.GetComponent<TriangleFollowing>().Activate();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        offset = transform.position - child.transform.position;
    }

        

    void FixedUpdate() {
        if (canMove) {
            if (!isShadow) {
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
                // if (Input.GetKey(KeyCode.R)) {
                //     var spawn = GameObject.FindWithTag("Respawn");
                //     transform.position = spawn.transform.position;
                // }

                rb.velocity = Vector3.ClampMagnitude(GetComponent<Rigidbody>().velocity, maxSpeed);
            } else {
                transform.position = child.transform.position + offset;
            }
        }
    }

        

     

    

}
