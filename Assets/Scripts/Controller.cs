using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    GameObject carema;


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
        Debug.Log("hi");
    }

    // Start is called before the first frame update
    void Start()
    {
        MyUtility.LogList(FindGameObjectsWithLayer(7));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) {
            // carema.tryToggleCam();
        }
    }
}
