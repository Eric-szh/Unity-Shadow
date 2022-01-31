using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitWin : MonoBehaviour
{
    bool win = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("you win");
        if (!win ) {
            
            Application.Quit();

            win = true;
        }
        
    }
}
