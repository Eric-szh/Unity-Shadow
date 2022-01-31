using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaremaSwitching : MonoBehaviour
{
    public GameObject Player;
    public Vector3 offset;
    public GameObject Base;
    public float lerpDuration;
    public bool postive = false;
    
    private Vector3 center; 
    private bool followPlayer = true;
    public float centerCaremaOffset = 20f;
    private float timeElapsed = 20f;
    private Vector3 startValue;
    private Vector3 endValue;

    private Quaternion playerRo;
    private Quaternion centerRo;
    private Quaternion startRo;
    private Quaternion endRo;

    private bool callBackDone = true;


    Vector3 getPlayerCaremaPoint() {
        Vector3 playerPoint = Player.transform.position + offset;
        return new Vector3(playerPoint[0], playerPoint[1], playerPoint[2]);
    }



    void initTransform(Vector3 startPoint, Vector3 endPoint, Quaternion startRotation, Quaternion endRotation) {
        timeElapsed = 0;
        startValue = startPoint;
        endValue = endPoint;
        startRo = startRotation;
        endRo = endRotation;
    }

    public void ToogleCarema() {
        if (followPlayer) {
            followPlayer = false;
            initTransform(getPlayerCaremaPoint(), center, playerRo, centerRo);
        } else {
            followPlayer = true;
            initTransform(center, getPlayerCaremaPoint(), centerRo, playerRo);
        }
        callBackDone = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        offset = transform.position - Player.transform.position;
        var cenPos = Base.GetComponent<MeshRenderer>().bounds.center;
        center = new Vector3(cenPos[0], cenPos[1] + centerCaremaOffset, cenPos[2]);
        transform.position = Player.transform.position + offset;
        playerRo = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        if (!postive) {
            centerRo = new Quaternion(0.5f, 0.5f, -0.5f, 0.5f);
        } else {
            centerRo = new Quaternion(0.5f, -0.5f, 0.5f, 0.5f);
        }
        
        
        // Debug.Log(centerRo);
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer) {
            transform.position = Player.transform.position + offset;
        }

        
        if (timeElapsed < lerpDuration)
        {
            transform.position = Vector3.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            transform.rotation = Quaternion.Lerp(startRo, endRo, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
        } else if(!callBackDone) {
            callBackDone = true;
            GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<Controller>().caremaCallBack();
        }
    }
}
