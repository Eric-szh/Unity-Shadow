using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleFollowing : MonoBehaviour
{
    public GameObject objectToFollow;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - objectToFollow.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = objectToFollow.transform.position + offset;
    }
}
