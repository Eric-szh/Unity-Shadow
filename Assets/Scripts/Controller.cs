using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    GameObject carema;
    GameObject player;

    bool shadowState = false;
    bool allowToPressB = true;

    private List<GameObject> FindGameObjectsWithLayer (int layer) {
        var goArray = FindObjectsOfType<GameObject>();
        var goList = new List<GameObject>();
        for (var i = 0; i < goArray.Length; i++) {
            if (goArray[i].layer == layer) {
                goList.Add(goArray[i]);
            }
        }

        if (goList.Count == 0) {
            return null;
        }
    
        return goList;
    }
    
    public void caremaCallBack() {
        allowToPressB = true;
        List<GameObject> objects = FindGameObjectsWithLayer(7);
        if(shadowState) {
            player.GetComponent<PlayerMoving>().SwitchShadow();
        } else {
            player.GetComponent<PlayerMoving>().SwitchNonShadow();
        }

        foreach (GameObject obj in objects) {
            if(shadowState) {
                obj.GetComponent<GenerateShadow>().GenShadow(false);
            } else {
                obj.GetComponent<GenerateShadow>().RemoveShadow();
            }
            
        }
        player.GetComponent<PlayerMoving>().UnlockMovement();
    }

    // Start is called before the first frame update
    void Start()
    {
        carema = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && allowToPressB) {
            player.GetComponent<PlayerMoving>().LockMovement();
            shadowState = !shadowState;
            allowToPressB = false;
            carema.GetComponent<CaremaSwitching>().ToogleCarema();
        }
    }
}
