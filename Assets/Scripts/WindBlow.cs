using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBlow : MonoBehaviour
{

    public float windStr;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Shadow" && other.gameObject.GetComponent<TriangleFollowing>().isActive()) {
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * windStr);
            Debug.Log("wind moving");
        }
       
    }
}
