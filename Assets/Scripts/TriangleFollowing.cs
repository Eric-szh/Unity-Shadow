using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleFollowing : MonoBehaviour
{
    public GameObject objectToFollow;
    public Vector3 offset;
    private bool activated;
    private bool onGround;

    Rigidbody rb;
    bool canMove = true;
    bool spacePressed = false;
    Ray groundDetectionRay;

    public float forceCof; 
    public float maxSpeed;
    public float gravityStrength;
    public float jumpStrength;

    public void Activate(){
        activated = true;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }

    public void Deactivate(){
        activated = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    public void LockMovement() {
        canMove = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void UnlockMovement() {
        canMove = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public bool isActive() {
        return activated;
    }

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - objectToFollow.transform.position;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        GetComponent<MeshRenderer>().enabled = false;
    }

    void Update() {
        groundDetectionRay = new Ray(GetComponent<MeshRenderer>().bounds.center, -Vector3.right);
        RaycastHit hit;
        if (Physics.Raycast(groundDetectionRay, out hit, 0.5f, LayerMask.GetMask("ShadowGenerated")) && activated) {
            onGround = true;
        } else {
            onGround = false;
        }
        Debug.DrawRay(groundDetectionRay.origin, groundDetectionRay.direction * 0.5f, Color.red);
        if (Input.GetKeyDown(KeyCode.Space) && onGround) {
            spacePressed = true;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!activated) {
            transform.position = objectToFollow.transform.position + offset;
        } else {
            if (Input.GetKey(KeyCode.A) && canMove) {
                    rb.AddForce(Vector3.forward * forceCof);
                }
                if (Input.GetKey(KeyCode.D)) {
                    rb.AddForce(-Vector3.forward * forceCof);
                }
                if (spacePressed) {
                    rb.velocity= new Vector3(jumpStrength, 0, 0);
                    spacePressed = false;
                    Debug.Log("jump");
                }
                if (Input.GetKeyDown(KeyCode.P)) {
                    transform.position = transform.position + new Vector3(0.1f, 0f, 0f);
                }
                rb.AddForce(-Vector3.right * gravityStrength);

                var currVelo = GetComponent<Rigidbody>().velocity;
                rb.velocity = new Vector3(currVelo[0], currVelo[1], Mathf.Clamp(currVelo[2], -maxSpeed, maxSpeed));
        }
        
    }
}
