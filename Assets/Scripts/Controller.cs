using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    GameObject carema;
    GameObject player;

    bool shadowState = false;
    bool allowToPressB = true;
    bool needRespwan = false;

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
        List<GameObject> objectShadow = FindGameObjectsWithLayer(8);
        List<GameObject> airObj = FindGameObjectsWithLayer(10);
        List<GameObject> glassObj = FindGameObjectsWithLayer(12);
        
        if(shadowState) {
            player.GetComponent<PlayerMoving>().SwitchShadow();
        } else {
            player.GetComponent<PlayerMoving>().SwitchNonShadow();
        }

        if (objects != null) {
            foreach (GameObject obj in objects) {
            if(shadowState) {
                obj.GetComponent<GenerateShadow>().GenShadow(false, false);
            } else {
                obj.GetComponent<GenerateShadow>().RemoveShadow();
            }
            
            }   
        }
        
        if(objectShadow != null) {
            foreach (GameObject obj in objectShadow) {
                if(shadowState) {
                    obj.GetComponent<GenerateShadow>().GenShadow(true, false);
                } else {
                    obj.GetComponent<GenerateShadow>().RemoveShadow();
            }

            }
        }

        if(airObj != null) {
            foreach (GameObject obj in airObj) {
                if(shadowState) {
                    obj.GetComponent<GenerateShadow>().GenShadow(false, true);
                } else {
                    obj.GetComponent<GenerateShadow>().RemoveShadow();
            }

            }
        }

        if(glassObj != null) {
            foreach (GameObject obj in glassObj) {
                if(shadowState) {
                    obj.GetComponent<MeshRenderer>().enabled = false;
                    obj.GetComponent<Collider>().enabled = false;
                } else {
                    obj.GetComponent<MeshRenderer>().enabled = true;
                    obj.GetComponent<Collider>().enabled = true;
            }

            }
        }

        if (needRespwan) {
            var spawn = GameObject.FindWithTag("Respawn");
            player.transform.position = spawn.GetComponent<MeshRenderer>().bounds.center;
            needRespwan = false;
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
        if (Input.GetKeyDown(KeyCode.R)) {
            needRespwan = true;
            if (shadowState) {
                carema.GetComponent<CaremaSwitching>().ToogleCarema();
                shadowState = !shadowState;
            }
            var spawn = GameObject.FindWithTag("Respawn");
            player.transform.position = spawn.transform.position;
        }
    }
}
